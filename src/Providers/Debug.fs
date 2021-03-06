﻿// --------------------------------------------------------------------------------------
// Helpers for writing type providers
// ----------------------------------------------------------------------------------------------

namespace ProviderImplementation

open System
open System.Collections.Generic
open System.Reflection
open System.Text
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Reflection
open ProviderImplementation.ProvidedTypes

module Debug = 

    /// Converts a sequence of strings to a single string separated with the delimiters
    let inline private separatedBy delimiter (items: string seq) = String.Join(delimiter, Array.ofSeq items)

    /// Simulates a real instance of TypeProviderConfig and then creates an instance of the last
    /// type provider added to a namespace by the type provider constructor
    let generate (resolutionFolder: string) (runtimeAssembly: string) typeProviderForNamespacesConstructor args =
        let cfg = new TypeProviderConfig(fun _ -> false)
        cfg.GetType().GetProperty("ResolutionFolder").GetSetMethod(nonPublic = true).Invoke(cfg, [| box resolutionFolder |]) |> ignore
        cfg.GetType().GetProperty("RuntimeAssembly").GetSetMethod(nonPublic = true).Invoke(cfg, [| box runtimeAssembly |]) |> ignore
        cfg.GetType().GetProperty("ReferencedAssemblies").GetSetMethod(nonPublic = true).Invoke(cfg, [| box ([||]: string[]) |]) |> ignore        

        let typeProviderForNamespaces = typeProviderForNamespacesConstructor cfg :> TypeProviderForNamespaces

        let providedTypeDefinition = typeProviderForNamespaces.Namespaces |> Seq.last |> snd |> Seq.last
            
        match args with
        | [||] -> providedTypeDefinition
        | args ->
            let typeName = providedTypeDefinition.Name + (args |> Seq.map (fun s -> ",\"" + (if s = null then "" else s.ToString()) + "\"") |> Seq.reduce (+))
            providedTypeDefinition.MakeParametricType(typeName, args)

    let private innerPrettyPrint signatureOnly (maxDepth: int option) exclude (t: ProvidedTypeDefinition) =        

        let ns = 
            [ t.Namespace
              "Microsoft.FSharp.Core"
              "Microsoft.FSharp.Core.Operators"
              "Microsoft.FSharp.Collections"
              "Microsoft.FSharp.Control"
              "Microsoft.FSharp.Text" ]
            |> Set.ofSeq

        let pending = new Queue<_>()
        let visited = new HashSet<_>()

        let add t =
            if not (exclude t) && visited.Add t then
                pending.Enqueue t

        let fullName (t: Type) =
            let fullName = t.FullName
            if fullName.StartsWith "FSI_" then
                fullName.Substring(fullName.IndexOf('.') + 1)
            else
                fullName

        let rec toString (t: Type) =

            if t = null then
                "<NULL>" // happens in the CSV and Freebase providers
            else

                let hasUnitOfMeasure = t.Name.Contains("[")

                let innerToString (t: Type) =
                    match t with
                    | t when t = typeof<bool> -> "bool"
                    | t when t = typeof<obj> -> "obj"
                    | t when t = typeof<int> -> "int"
                    | t when t = typeof<int64> -> "int64"
                    | t when t = typeof<float> -> "float"
                    | t when t = typeof<float32> -> "float32"
                    | t when t = typeof<decimal> -> "decimal"
                    | t when t = typeof<string> -> "string"
                    | t when t = typeof<Void> -> "()"
                    | t when t = typeof<unit> -> "()"
                    | t when t.IsArray -> (t.GetElementType() |> toString) + "[]"
                    | :? ProvidedTypeDefinition as t ->
                        add t
                        t.Name.Split([| ',' |]).[0]
                    | t when t.IsGenericType ->            
                        let args =                 
                            t.GetGenericArguments() 
                            |> Seq.map (if hasUnitOfMeasure then (fun t -> t.Name) else toString)
                            |> separatedBy ", "
                        let name, reverse = 
                            match t with
                            | t when hasUnitOfMeasure -> toString t.UnderlyingSystemType, false
                            | t when t.GetGenericTypeDefinition() = typeof<int seq>.GetGenericTypeDefinition() -> "seq", true
                            | t when t.GetGenericTypeDefinition() = typeof<int list>.GetGenericTypeDefinition() -> "list", true
                            | t when t.GetGenericTypeDefinition() = typeof<int option>.GetGenericTypeDefinition() -> "option", true
                            | t when t.GetGenericTypeDefinition() = typeof<int ref>.GetGenericTypeDefinition() -> "ref", true
                            | t when ns.Contains t.Namespace -> t.Name, false
                            | t -> fullName t, false
                        let name = name.Split([| '`' |]).[0]
                        if reverse then
                            args + " " + name 
                        else
                            name + "<" + args + ">"
                    | t when ns.Contains t.Namespace -> t.Name
                    | t when t.IsGenericParameter -> t.Name
                    | t -> fullName t

                let rec warnIfWrongAssembly (t:Type) =
                    match t with
                    | :? ProvidedTypeDefinition as t -> ""
                    | t when t.IsGenericType -> defaultArg (t.GetGenericArguments() |> Seq.map warnIfWrongAssembly |> Seq.tryFind (fun s -> s <> "")) ""
                    | t when t.IsArray -> warnIfWrongAssembly <| t.GetElementType()
                    | t -> if not t.IsGenericParameter && t.Assembly = Assembly.GetExecutingAssembly() then " [DESIGNTIME]" else ""

                if hasUnitOfMeasure || t.IsGenericParameter || t.DeclaringType = null then
                    innerToString t + (warnIfWrongAssembly t)
                else
                    (toString t.DeclaringType) + "+" + (innerToString t) + (warnIfWrongAssembly t)

        let toSignature (parameters: ParameterInfo[]) =
            if parameters.Length = 0 then
                "()"
            else
                parameters 
                |> Seq.map (fun p -> p.Name + ":" + (toString p.ParameterType))
                |> separatedBy " -> "

        let sb = StringBuilder ()
        let print (str: string) =
            sb.Append(str) |> ignore
        let println() =
            sb.AppendLine() |> ignore
                
        let printMember (memberInfo: MemberInfo) =        

            let print str =
                print "    "                
                print str
                println()

            let getMethodBody (m: ProvidedMethod) = 
                seq { if not m.IsStatic then yield m.DeclaringType.BaseType
                      for param in m.GetParameters() do yield param.ParameterType }
                |> Seq.map (fun typ -> Expr.Value(null, typ))
                |> Array.ofSeq
                |> m.GetInvokeCodeInternal false

            let getConstructorBody (c: ProvidedConstructor) = 
                seq { if not c.IsStatic then yield c.DeclaringType.BaseType
                      for param in c.GetParameters() do yield param.ParameterType }
                |> Seq.map (fun typ -> Expr.Value(null, typ))
                |> Array.ofSeq
                |> c.GetInvokeCodeInternal false

            match memberInfo with

            | :? ProvidedConstructor as cons -> 
                let body = 
                    if signatureOnly then ""
                    else cons |> getConstructorBody |> sprintf "\n%A\n"
                print <| "new : " + 
                         (toSignature <| cons.GetParameters()) + " -> " + 
                         (toString memberInfo.DeclaringType) + body

            | :? ProvidedLiteralField as field -> 
                let value = 
                    if signatureOnly then ""
                    else field.GetRawConstantValue() |> sprintf "\n%O\n" 
                print <| "val " + field.Name + ": " + 
                         (toString field.FieldType) + 
                         value
                         
            | :? ProvidedProperty as prop -> 
                let body = 
                    if signatureOnly then ""
                    else
                        let getter = 
                            if not prop.CanRead then ""
                            else getMethodBody (prop.GetMethod :?> ProvidedMethod) |> sprintf "\n%A\n"
                        let setter = 
                            if not prop.CanWrite then ""
                            else getMethodBody (prop.SetMethod :?> ProvidedMethod) |> sprintf "\n%A\n"
                        getter + setter
                print <| (if prop.IsStatic then "static " else "") + "member " + 
                         prop.Name + ": " + (toString prop.PropertyType) + 
                         " with " + (if prop.CanRead && prop.CanWrite then "get, set" else if prop.CanRead then "get" else "set")            

            | :? ProvidedMethod as m ->
                let body = 
                    if signatureOnly then ""
                    else m |> getMethodBody |> sprintf "\n%A\n"
                if m.Attributes &&& MethodAttributes.SpecialName <> MethodAttributes.SpecialName then
                    print <| (if m.IsStatic then "static " else "") + "member " + 
                    m.Name + ": " + (toSignature <| m.GetParameters()) + 
                    " -> " + (toString m.ReturnType) + body

            | :? ProvidedTypeDefinition as t -> add t

            | _ -> ()

        add t

        let currentDepth = ref 0

        let stop() =
            match maxDepth with
            | Some maxDepth -> !currentDepth > maxDepth
            | None -> false

        while pending.Count <> 0 && not (stop()) do
            let pendingForThisDepth = new Queue<_>(pending)
            pending.Clear()
            while pendingForThisDepth.Count <> 0 do
                let t = pendingForThisDepth.Dequeue()
                match t with
                | t when FSharpType.IsRecord t-> "record "
                | t when FSharpType.IsModule t -> "module "
                | t when t.IsValueType -> "struct "
                | t when t.IsClass && t.IsSealed && t.IsAbstract -> "static class "
                | t when t.IsClass && t.IsAbstract -> "abstract class "
                | t when t.IsClass -> "class "
                | t -> ""
                |> print
                print (toString t)
                if t.BaseType <> typeof<obj> then
                    print " : "
                    print (toString t.BaseType)
                println()
                t.GetMembers() |> Seq.iter printMember
                println()
            currentDepth := !currentDepth + 1
    
        sb.ToString()

    /// Returns a string representation of the signature (and optionally also the body) of all the
    /// types generated by the type provider
    let prettyPrint signatureOnly t = innerPrettyPrint signatureOnly None (fun _ -> false) t

    /// Returns a string representation of the signature (and optionally also the body) of all the
    /// types generated by the type provider up to a certain depth
    let prettyPrintWithMaxDepth signatureOnly maxDepth t = innerPrettyPrint signatureOnly (Some maxDepth) (fun _ -> false) t

    /// Returns a string representation of the signature (and optionally also the body) of all the
    /// types generated by the type provider up to a certain depth and excluding some types
    let prettyPrintWithMaxDepthAndExclusions signatureOnly maxDepth exclusions t = 
        let exclusions = Set.ofSeq exclusions
        innerPrettyPrint signatureOnly (Some maxDepth) (fun t -> exclusions.Contains t.Name) t

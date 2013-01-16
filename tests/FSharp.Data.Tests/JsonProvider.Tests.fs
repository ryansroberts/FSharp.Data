﻿// --------------------------------------------------------------------------------------
// Tests for the CSV parsing code
// --------------------------------------------------------------------------------------

namespace FSharp.Data.Tests

open NUnit.Framework
open System.IO
open FSharp.Data

module JsonNumericFields =

  type NumericFields = JsonProvider<""" [ {"a":12.3}, {"a":1.23, "b":1999.0} ] """, SampleList=true>

  [<Test>]
  let ``Decimal required field is read correctly`` () = 
    let prov = NumericFields.Parse(""" {"a":123} """)
    Assert.AreEqual(123M, prov.A)

  [<Test>]
  let ``Decimal optional field is read as None`` () = 
    let prov = NumericFields.Parse(""" {"a":123} """)
    Assert.AreEqual(None, prov.B)

  [<Test>]
  let ``Reading a required field that is null throws an exception`` () = 
    let prov = NumericFields.Parse(""" {"a":null, "b":123} """)
    Assert.Throws<System.Exception>(fun () -> prov.A |> ignore) |> ignore

  [<Test>]
  let ``Reading a required field that is missing throws an exception`` () = 
    let prov = NumericFields.Parse(""" {"b":123} """)
    Assert.Throws<System.Collections.Generic.KeyNotFoundException>(fun () -> prov.A |> ignore) |> ignore

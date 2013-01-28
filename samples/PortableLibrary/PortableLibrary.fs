module PortableLibrary

open FSharp.Data

type SimpleJson = JsonProvider<""" { "name":"John", "age":94 } """>
type StocksCsv = CsvProvider<"../docs/MSFT.csv">
type DetailedXml = XmlProvider<"""<author><name full="true">Karl Popper</name></author>""">
type WorldBankJson = JsonProvider<"../docs/WorldBank.json">
type AuthorsXml = XmlProvider<"../docs/Writers.xml">


let getJsonData() = seq {

    let simple = SimpleJson.Parse(""" { "name":"Tomas", "age":4 } """)
    yield simple.Age.ToString()
    yield simple.Name

    let doc = WorldBankJson.Parse("""[
    {
        "page": 1,
        "pages": 1,
        "per_page": "1000",
        "total": 53
    },
    [
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "2012"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "2011"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "35.1422970266502",
            "decimal": "1",
            "date": "2010"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "31.034880222506",
            "decimal": "1",
            "date": "2009"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "25.475163645463",
            "decimal": "1",
            "date": "2008"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "24.1933198328061",
            "decimal": "1",
            "date": "2007"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "23.7080545570765",
            "decimal": "1",
            "date": "2006"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "22.0334615295746",
            "decimal": "1",
            "date": "2005"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "20.1083787500358",
            "decimal": "1",
            "date": "2004"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "18.2677252058791",
            "decimal": "1",
            "date": "2003"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "15.4255646477354",
            "decimal": "1",
            "date": "2002"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "14.8744342075761",
            "decimal": "1",
            "date": "2001"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "13.2188686145055",
            "decimal": "1",
            "date": "2000"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "11.3566955774787",
            "decimal": "1",
            "date": "1999"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "10.1787800927734",
            "decimal": "1",
            "date": "1998"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "10.1535658732129",
            "decimal": "1",
            "date": "1997"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "10.5203014347956",
            "decimal": "1",
            "date": "1996"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "12.7078339884043",
            "decimal": "1",
            "date": "1995"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "14.7818078232509",
            "decimal": "1",
            "date": "1994"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": "16.6567773464055",
            "decimal": "1",
            "date": "1993"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1992"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1991"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1990"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1989"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1988"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1987"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1986"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1985"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1984"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1983"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1982"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1981"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1980"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1979"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1978"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1977"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1976"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1975"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1974"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1973"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1972"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1971"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1970"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1969"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1968"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1967"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1966"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1965"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1964"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1963"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1962"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1961"
        },
        {
            "indicator": {
                "id": "GC.DOD.TOTL.GD.ZS",
                "value": "Central government debt, total (% of GDP)"
            },
            "country": {
                "id": "CZ",
                "value": "Czech Republic"
            },
            "value": null,
            "decimal": "1",
            "date": "1960"
        }
    ]
]""") //worldBankJsonStream)

    let info = doc.Record
    yield sprintf "Showing page %d of %d. Total records %d" info.Page info.Pages info.Total

    for record in doc.Array do
      if record.Value <> null then
        yield sprintf "%d: %f" (int record.Date) (float record.Value)
}

let getXmlData() = seq {

    let info = DetailedXml.Parse("""<author><name full="false">Thomas Kuhn</name></author>""")
    yield sprintf "%s (full=%b)" info.Name.Value info.Name.Full

    let topic = AuthorsXml.Parse("""<authors topic="Philosophy of Science">
  <author name="Paul Feyerabend" born="1924" />
  <author name="Thomas Kuhn" />
</authors>""") //writerXmlStream)

    yield topic.Topic
    for author in topic.GetAuthors() do
      yield sprintf " - %s" author.Name 
      match author.Born with
      | Some born -> yield sprintf " (%d)" born
      | None -> ()
}

let getCsvData forSilverlight = seq {

    let msft = 
        if forSilverlight then
            let data = """Date,Open,High,Low,Close,Volume,Adj Close
2012-01-27,29.45,29.53,29.17,29.23,44187700,29.23
2012-01-26,29.61,29.70,29.40,29.50,49102800,29.50"""
            StocksCsv.Parse(data)
        else
            StocksCsv.Load("http://ichart.finance.yahoo.com/table.csv?s=MSFT")
    for row in msft.Data |> Seq.truncate 10 do
        yield sprintf "HLOC: (%A, %A, %A, %A)" row.High row.Low row.Open row.Close
}

let getWorldBankData() = seq {
    let wb = WorldBankData.GetDataContext()
    for c in wb.Countries |> Seq.truncate 10 do
        yield c.Name
        yield sprintf "GDP = %s" (c.Indicators.``GDP (current US$)`` |> Seq.map string |> String.concat ",")
}

let getData forSilverlight = seq {

    yield! getJsonData()
    
    if not forSilverlight then
      yield! getXmlData()

    yield! getCsvData forSilverlight

    //if not forSilverlight then
    //yield! getWorldBankData()
}

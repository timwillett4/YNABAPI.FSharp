open Expecto
open Hopac
open Logary.Configuration
open Logary.Adapters.Facade
open Logary.Targets

[<EntryPoint>]
let main args =
  let writeResults = TestResults.writeNUnitSummary ("YNABApi.FSharp.SerializationTests.xml", "YNABApi.FSharp.Tests.SerializationTests")
  let config = defaultConfig.appendSummaryHandler writeResults

  let logary =
    Config.create "YNABApi.Fsharp.Tests" "localhost"
    |> Config.targets [ LiterateConsole.create LiterateConsole.empty "console" ]
    |> Config.processing (Events.events |> Events.sink ["console";])
    |> Config.build
    |> run
  LogaryFacadeAdapter.initialise<Expecto.Logging.Logger> logary

  runTestsInAssembly config args

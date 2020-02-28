module YNABApi.FSharp.Tests.SerializationTests

open YNABApi.FSharp
open Expecto

open System.IO
open System.Reflection

let assembly = Assembly.GetExecutingAssembly();

let getSampleData resourceName = 
    let sampleDataStream = ("YNABApi.FSharp.Tests.sample_data." + resourceName) |> assembly.GetManifestResourceStream
    use jsonStream = new StreamReader(sampleDataStream)
    JsonData <| jsonStream.ReadToEnd()

[<Tests>]
let jsonDeserializationTests =
    
  testList "Json Deserialization Tests" [

    test "user.json deserializes successfully to UserResponse" {
        let userResponseResult = "user.json" |> getSampleData |> deserialize<UserResponse>
        Expect.isOk userResponseResult "Deserialize should return an okay result" }
  
    test "budgets.json deserializes successfully to BudgetSummaryResponse" {
        let budgetSummaryResponseResult = "budgets.json" |> getSampleData |> deserialize<BudgetSummaryResponse>
        Expect.isOk budgetSummaryResponseResult "Deserialize should return an okay result" }

    test "categories.json deserializes successfully to CategoryResponse" {
        let categoriesResponseResult = "categories.json" |> getSampleData |> deserialize<CategoriesResponse>
        Expect.isOk categoriesResponseResult "Deserialize should return an okay result" }

    test "transactions.json deserializes successfully to TransactionsResponse" {
        let transactionResponseResult = "transactions.json" |> getSampleData |> deserialize<TransactionsResponse>
        Expect.isOk transactionResponseResult "Deserialize should return an okay result" }
  ]
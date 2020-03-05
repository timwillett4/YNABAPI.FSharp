module YNABApi.FSharp.Tests.DataRequestTests

open YNABApi.FSharp
open YNABApi.FSharp.DataRequests
open Expecto

let accessToken = PersonalAccessToken "75714b4fed7a4997f8691917893a38c525721573660cb31d024fd80ce63f24f7";

[<Tests>]
let dataRequestTests =
    testList "Data Request Tests" [

    test "Request user data should return a valid user response" {
        let userResponseResult = requestUserInfo accessToken

        match userResponseResult with
        | Ok (statusCode, json) -> 
            Expect.equal statusCode (StatusCode 200) "Expected status code to be 200"
            Expect.isOk (json |> deserialize<UserResponse>) (sprintf "Expected returned json to be serializable: \n%A\n" json)
        | e -> Expect.isOk e "Expected no exception to be thrown"}

    test "Request budget summary should return a valid budget summary response" {
        let budgetSummaryResult = requestBudgetSummary accessToken

        match budgetSummaryResult with
        | Ok (statusCode, json) -> 
            Expect.equal statusCode (StatusCode 200) "Expected status code to be 200"
            Expect.isOk (json |> deserialize<BudgetSummaryResponse>) (sprintf "Expected returned json to be serializable: \n%A\n" json)
        | e -> Expect.isOk e "Expected no exception to be thrown"}

    test "Request categories for last used budget should return a valid categories response" {
        let categoriesResponse = requestCategories accessToken BudgetID.LastUsed None

        match categoriesResponse with
        | Ok (statusCode, json) -> 
            Expect.equal statusCode (StatusCode 200) "Expected status code to be 200"
            Expect.isOk (json |> deserialize<CategoriesResponse>) (sprintf "Expected returned json to be serializable: \n%A\n" json)
        | e -> Expect.isOk e "Expected no exception to be thrown"}

    test "Request transactions for last used budget should return a valid transactions response" {
        let transactionResult = requestTransactions accessToken BudgetID.LastUsed None None None

        match transactionResult with
        | Ok (statusCode, json) -> 
            Expect.equal statusCode (StatusCode 200) "Expected status code to be 200"
            Expect.isOk (json |> deserialize<TransactionsResponse>) (sprintf "Expected returned json to be serializable: \n%A\n" json)
        | e -> Expect.isOk e "Expected no exception to be thrown"}
    ]
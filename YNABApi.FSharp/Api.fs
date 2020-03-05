module YNABApi.FSharp.Api

open YNABApi.FSharp.DataRequests
open FSharp.Core.Extensions


type ApiError =
    | HttpError of ErrorResponse
    | Exn of exn

let private liftResult result =
    match result with
    | Ok x -> Ok x
    | Error exn -> Error (Exn exn)

let private getResponse<'a> statusCode json = result {
    match statusCode with 
    | StatusCode 200 -> 
        return! json |> deserialize<'a> |> liftResult
    | statusCode -> 
        let! errorResponse = json |> deserialize<ErrorResponse> |> liftResult
        return! errorResponse |> HttpError |> Error }

type Api(personalAccessToken) =
    
    member this.RequestUserInfo() = result { 
        let! statusCode,json = personalAccessToken |> requestUserInfo |> liftResult 
        return! (statusCode,json) ||> getResponse<UserResponse> }

    member this.RequestBudgetSummary() = result {
        let! statusCode,json = personalAccessToken |> requestBudgetSummary |> liftResult
        return! (statusCode,json) ||> getResponse<BudgetSummaryResponse> }

    member this.RequestCategories(?budgetID, ?serverKnowledge) = result {
        let budgetID = defaultArg budgetID LastUsed
        let! statusCode,json =  requestCategories personalAccessToken budgetID serverKnowledge |> liftResult
        return! (statusCode,json) ||> getResponse<CategoriesResponse> }

    member this.RequestTransactions(?budgetID, ?sinceDate, ?transactionType, ?serverKnowledge) = result {
        let budgetID = defaultArg budgetID LastUsed
        let! statusCode,json =  requestTransactions personalAccessToken budgetID sinceDate transactionType serverKnowledge |> liftResult
        return! (statusCode,json) ||> getResponse<TransactionsResponse> }


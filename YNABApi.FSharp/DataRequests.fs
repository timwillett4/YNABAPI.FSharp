module YNABApi.FSharp.DataRequests

open System
open System.IO
open Hopac
open HttpFs.Client
open NodaTime

let endpointRoot = "https://api.youneedabudget.com/v1"

type PersonalAccessToken = PersonalAccessToken of string
type StatusCode = StatusCode of int

type BudgetID = 
| BudgetID of Guid
| LastUsed
| Default

let budgetIDString budgetID =
   match budgetID with
   | BudgetID id -> id.ToString()
   | LastUsed -> "last-used"
   | Default -> "default"

let requestData endPoint (PersonalAccessToken accessToken) =
    try
        job {
            use! response = 
                Request.createUrl Get (Path.Combine(endpointRoot, endPoint))
                |> Request.setHeader (Authorization ("Bearer " + accessToken))
                |> getResponse

            let! responseBody = Response.readBodyAsString response
            // @TODO get headers for error?

            return StatusCode response.statusCode, JsonData responseBody       
        } |> run |> Ok
    with
    | ex -> Error ex

let requestUserInfo =
    "user" |> requestData

let requestBudgetSummary =
    "budgets" |> requestData 

let requestCategories token budgetID (serverKnowledge:int option) =
    let endPoint = Path.Combine("budgets", budgetID |> budgetIDString, "categories") 
    let endPoint = 
        match serverKnowledge with
        | Some serverKnowledge ->
            Path.Combine(endPoint, "?last_knowledge_of_server=" + serverKnowledge.ToString())
        | None -> endPoint

    (endPoint, token) ||> requestData

type TransactionType =
    | Uncategorized
    | Unapproved

let requestTransactions token budgetID (sinceDate:LocalDate option) transactionType (serverKnowledge:int option) =
    let endPoint = Path.Combine("budgets", budgetID |> budgetIDString, "transactions") 
    
    let endPoint = 
        match sinceDate with
        | Some sinceDate ->
            Path.Combine(endPoint, "?since_date=" + sinceDate.ToString())
        | None -> endPoint

    let endPoint = 
        match transactionType with
        | Some Uncategorized ->
            Path.Combine(endPoint, "?since_date=uncategorized")
        | Some Unapproved ->
            Path.Combine(endPoint, "?since_date=unapproved")
        | None -> endPoint

    let endPoint = 
        match serverKnowledge with
        | Some serverKnowledge ->
            Path.Combine(endPoint, "?last_knowledge_of_server=" + serverKnowledge.ToString())
        | None -> endPoint

    (endPoint,token) ||> requestData

[<AutoOpen>]
module YNABApi.FSharp.ResponseTypes

open System
open FSharp.Json

type JsonData = JsonData of string

let deserialize<'a> (JsonData json) =
    try
        Ok <| Json.deserialize<'a> json
    with
        | ex -> Error ex

type ErrorDetail = {
    [<JsonField("id")>]
    ID : string 
    [<JsonField("name")>]
    Name : string
    [<JsonField("detail")>]
    Detail : string
}

type ErrorResponse = {
    [<JsonField("error")>]
    Error : ErrorDetail 
}

type User = {
    [<JsonField("id")>]
    ID : Guid
}

type UserResponseData = {
    [<JsonField("user")>]
    User : User
}

type UserResponse = {
    [<JsonField("data")>]
    Data : UserResponseData
}

type DateFormat = {
    [<JsonField("format")>]
    Format : string
}

type CurrencyFormat = {
    [<JsonField("iso_code")>]
    IsoCode : string
    [<JsonField("example_format")>]
    ExampleFormat : string
    [<JsonField("decimal_digits")>]
    DecimalDigits : int
    [<JsonField("decimal_separator")>]
    DecimalSeparator : string
    [<JsonField("symbol_first")>]
    SymbolFirst : bool
    [<JsonField("group_separator")>]
    GroupSeparator : string
    [<JsonField("currency_symbol")>]
    CurrencySymbol : string
    [<JsonField("display_symbol")>]
    DisplaySymbol : bool
}

type BudgetSummary = {
    [<JsonField("id")>]
    ID : Guid
    [<JsonField("name")>]
    Name : string
    [<JsonField("last_modified_on")>]
    LastModifiedOn : DateTimeOffset
    [<JsonField("first_month")>]
    FirstMonth : DateTime
    [<JsonField("last_month")>]
    LastMonth : DateTime
    [<JsonField("date_format")>]
    DateFormat : DateFormat option
    [<JsonField("currency_format")>]
    CurrencyFormat : CurrencyFormat option
}

type BudgetSummaryResponseData = {
    [<JsonField("budgets")>]
    Budgets : BudgetSummary list
    [<JsonField("default_budget")>]
    DefaultBudget : BudgetSummary option
}

type BudgetSummaryResponse = {
    [<JsonField("data")>]
    Data : BudgetSummaryResponseData
}

type GoalType =
    | [<JsonUnionCase(Case="TB")>]TargetCategoryBalance
    | [<JsonUnionCase(Case="TBD")>]TargetCategoryBalanceByDate
    | [<JsonUnionCase(Case="MF")>]MontlyFunding
    | [<JsonUnionCase(Case="NEED")>]Need

type Category = {
    [<JsonField("id")>]
    ID : Guid
    [<JsonField("category_group_id")>]
    CategoryGroupID : Guid
    [<JsonField("name")>]
    Name : string
    [<JsonField("hidden")>]
    Hidden : bool
    [<JsonField("original_category_name")>]
    OriginalCategoryID : Guid option
    [<JsonField("note")>]
    Note : string option
    [<JsonField("budgeted")>]
    Budgeted : int64
    [<JsonField("activity")>]
    Activity : int64
    [<JsonField("balance")>]
    Balance : int64
    [<JsonField("goal_type")>]
    GoalType : GoalType option
    [<JsonField("goal_creation_month")>]
    GoalCreationMonth : System.DateTime option
    [<JsonField("goal_target")>]
    GoalTarget : int64 option
    [<JsonField("goal_target_month")>]
    GoalTargetMonth : System.DateTime option
    [<JsonField("goal_percentage_complete")>]
    GoalPercentageComplete : int32 option
    [<JsonField("deleted")>]
    Deleted : bool
}

type CategoryGroup = {
    [<JsonField("id")>]
    ID : Guid
    [<JsonField("name")>]
    Name : String
    [<JsonField("hidden")>]
    Hidden : bool
    [<JsonField("deleted")>]
    Deleted : bool
}

type CategoryGroupWithCategories = {
    [<JsonField("id")>]
    ID : Guid
    [<JsonField("name")>]
    Name : String
    [<JsonField("hidden")>]
    Hidden : bool
    [<JsonField("deleted")>]
    Deleted : bool
    [<JsonField("categories")>]
    Categories : Category list
}

type CategoriesResponseData = {
    [<JsonField("category_groups")>]
    CategoryGroups : CategoryGroupWithCategories list
    [<JsonField("server_knowledge")>]
    ServerKnowledge : int
}

type CategoriesResponse = {
    [<JsonField("data")>]
    Data : CategoriesResponseData
}

type CategoryResponseData = {
    [<JsonField("category")>]
    Category : Category 
}

type CategoryResponse = {
    [<JsonField("data")>]
    Data : CategoryResponseData
}

type Subtransaction = {
    [<JsonField("id")>]
    ID : string
    [<JsonField("transaction_id")>]
    TransactionID : string
    [<JsonField("amount")>]
    Amount : int64
    [<JsonField("memo")>]
    Memo : string option
    [<JsonField("payee_id")>]
    PayeeID : Guid option
    [<JsonField("category_id")>]
    CategoryID : Guid option
    [<JsonField("transfer_account_id")>]
    TransferAccountID : Guid option
    [<JsonField("deleted")>]
    Deleted : bool
}

type ClearedState = 
    | [<JsonUnionCase(Case="cleared")>]Cleared
    | [<JsonUnionCase(Case="uncleared")>]Uncleared
    | [<JsonUnionCase(Case="reconciled")>]Reconciled

type FlagColor = 
    | [<JsonUnionCase(Case="red")>]Red
    | [<JsonUnionCase(Case="orange")>]Orange
    | [<JsonUnionCase(Case="yellow")>]Yellow
    | [<JsonUnionCase(Case="green")>]Green
    | [<JsonUnionCase(Case="blue")>]Blue
    | [<JsonUnionCase(Case="purple")>]Purple

type TransactionDetail = {
    [<JsonField("id")>]
    ID : string option
    [<JsonField("date")>]
    Date : System.DateTime
    [<JsonField("amount")>]
    Amount : int64
    [<JsonField("memo")>]
    Memo : string option
    [<JsonField("cleared")>]
    Cleared : ClearedState
    [<JsonField("approved")>]
    Approved : bool
    [<JsonField("flag_color")>]
    FlagColor : FlagColor option
    [<JsonField("account_id")>]
    AccountID : Guid option
    [<JsonField("payee_id")>]
    PayeeID : Guid option
    [<JsonField("category_id")>]
    CategoryID : Guid option
    [<JsonField("transfer_acount_id")>]
    TransferAccountID : string option
    [<JsonField("matched_transaction_id")>]
    MatchedTransactionID : string option
    [<JsonField("import_id")>]
    ImportID : string option
    [<JsonField("deleted")>]
    Deleted : bool
    [<JsonField("account_name")>]
    AccountName : string
    [<JsonField("payee_name")>]
    PayeeName : string option
    [<JsonField("category_name")>]
    CategoryName : string option
    [<JsonField("subtransactions")>]
    Subtransactions : Subtransaction list
}

type TransactionResponseData = {
    [<JsonField("transactions")>]
    Transactions : TransactionDetail list
    [<JsonField("server_knowledge")>]
    ServerKnowledge : int
}

type TransactionsResponse = {
    [<JsonField("data")>]
    Data : TransactionResponseData
}
# Advanced Banking System

A small console-based banking demo application demonstrating basic banking operations and programming concepts in C#.

## Features
- Create bank accounts (Savings/Current)
- Deposit, withdraw and transfer money (with optional description/notification)
- Calculate simple and compound interest
- Display payment schedules and compound interest (recursive example)
- Demonstrate pass-by-value, pass-by-ref and out parameters
- Simple bank statistics and account display

## Files
- `Program.cs` — Console UI and program flow
- `BankAccount.cs` — Account model and operations
- `TransactionProcessor.cs` — Helper methods for transactions and examples
- `LoanCalculator.cs` — Interest and payment schedule utilities

## Requirements
- .NET SDK installed (use the `dotnet` CLI). The project contains a `.csproj` file — build/run with the CLI shown below.

## Build & Run
From the project directory (where `AdvancedBankingSystem.csproj` lives):

```bash
dotnet build
dotnet run --project AdvancedBankingSystem.csproj
```

## Usage
Run the app and choose menu options to create accounts, make deposits/withdrawals, transfer funds, and run the example utilities.

## Notes / TODOs
- `Program.cs` was recently updated to fix input-scope issues and improve type validation. You may still see analyzer suggestions:
  - Consider making `SelectAccount` return a nullable (`BankAccount?`) and update callers to avoid possible null-return warnings.
  - Refactor large methods (`Main`, `TransferMenu`) to reduce cognitive complexity.
  - Remove or integrate the unused `pause()` helper.

## Contributing
This repository is a learning/demo project — feel free to open issues or submit improvements.

## License
No license specified.

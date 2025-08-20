# Smartly Assignment - Payslip Calculator

## Project Structure

- **SmartlyAssignment.Core** - Core business logic and domain services
- **SmartlyAssignment.Tests** - Unit tests using xUnit

## Prerequisites

- .NET 8.0 SDK or later

## Building the Solution

Navigate to the `src` directory and run:

```bash
dotnet build
```

## Running Tests

To run all tests:

```bash
dotnet test
```

## Current Status

The project currently contains a stub `PayslipCalculator` service that returns `1` for testing purposes. Unit tests verify this basic functionality works correctly.

## Next Steps

1. Implement the actual payslip calculation logic
2. Add Employee and Payslip domain entities
3. Create comprehensive test coverage for tax calculations
4. Build API and frontend components
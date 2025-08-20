# Smartly Assignment - Payslip Calculator

## Project Structure

- **SmartlyAssignment.Core** - Core business logic and domain services
- **SmartlyAssignment.Tests** - Unit tests using xUnit

## Prerequisites

- .NET 8.0 SDK or later

## Building the Solution

Navigate to the `src` directory and run:

```bash
# Navigate to source directory
cd src

# Restore packages
dotnet restore

# Build solution
dotnet build

# Run tests
dotnet test
```

## Current Status

The project currently contains a stub `PayslipCalculator` service that returns `1` for testing purposes. Unit tests verify this basic functionality works correctly.

## 🔧 Assumptions

### Pay Period Handling
The pay period is treated as a **display-only field** that shows which month the payslip is for. Monthly payslip amounts are always calculated as `annual salary / 12` regardless of the number of days in the month. This aligns with standard salaried employment practices where employees receive consistent monthly compensation.

The system accepts a month name and year, then calculates the full calendar month date range for display (e.g., "01 March – 31 March"). This handles edge cases like February's varying days (28/29) automatically based on the specified year.

### Percentage Storage
Super rates are stored as `decimal` values representing fractions (e.g., 0.09 for 9%, 0.10 for 10%). This approach follows financial industry standards for precision and avoids floating-point arithmetic errors.

## Next Steps

1. Implement the actual payslip calculation logic
2. Add Employee and Payslip domain entities
3. Create comprehensive test coverage for tax calculations
4. Build API and frontend components
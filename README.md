# Smartly Assignment - Payslip Calculator

## Project Structure

- **SmartlyAssignment.Core** - Core business logic and domain services
- **SmartlyAssignment.Tests** - Unit tests using xUnit
- **SmartlyAssignment.Api** - ASP.NET Core Web API with frontend

## Getting Started

### Option 1: From Terminal

```bash
# Navigate to the API project directory
cd src/SmartlyAssignment.Api

# Build and run the API
dotnet run
```

### Option 2: Run Tests

```bash
# Navigate to the source directory
cd src

# Run all tests
dotnet test
```

### Option 3: From Visual Studio

1. Open the solution in Visual Studio
2. Right-click on `SmartlyAssignment.Api` project
3. Select "Set as Startup Project"
4. Press F5 or click the "Start Debugging" button

The API will start on `http://localhost:5140` in Development mode with the frontend interface accessible at the same URL.

## 🔧 Assumptions

### Pay Period Handling
The pay period is treated as a **display-only field** that shows which month the payslip is for. Monthly payslip amounts are always calculated as `annual salary / 12` regardless of the number of days in the month. This aligns with standard salaried employment practices where employees receive consistent monthly compensation.

The system accepts a month name and year, then calculates the full calendar month date range for display (e.g., "01 March – 31 March"). This handles edge cases like February's varying days (28/29) automatically based on the specified year.

### Percentage Storage
Super rates are stored as `decimal` values representing fractions (e.g., 0.09 for 9%, 0.10 for 10%). This approach follows financial industry standards for precision and avoids floating-point arithmetic errors.

## Frontend

A simple HTML/CSS/JavaScript interface is served from the API's `wwwroot` folder. Access it at `http://localhost:5140` when the API is running.
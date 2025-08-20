using Xunit;
using FluentAssertions;
using Moq;
using SmartlyAssignment.Core.Domain.Services;
using SmartlyAssignment.Core.Domain.Entities;
using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Tests.Domain.Services;

public class PayslipCalculatorTests
{
    private readonly PayslipCalculator _sut;
    private readonly Mock<ITaxBracketService> _mockTaxBracketService;

    public PayslipCalculatorTests()
    {
        _mockTaxBracketService = new Mock<ITaxBracketService>();
        
        var taxBrackets = new[]
        {
            new TaxBracket(0m, 14000m, 0.105m),
            new TaxBracket(14000m, 48000m, 0.175m),
            new TaxBracket(48000m, 70000m, 0.30m),
            new TaxBracket(70000m, 180000m, 0.33m),
            new TaxBracket(180000m, decimal.MaxValue, 0.39m)
        };

        _mockTaxBracketService
            .Setup(x => x.GetOrderedTaxBrackets())
            .Returns(taxBrackets);
            
        _sut = new PayslipCalculator(_mockTaxBracketService.Object);
    }

    private string BuildExpectedCsv(
        string employeeName,
        Month month,
        int year, 
        decimal grossIncome,
        decimal incomeTax,
        decimal netIncome,
        decimal super, 
        int decimalPlaces = 2)
    {
        var firstDayOfMonth = new DateTime(year, (int)month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        var payPeriod = $"{firstDayOfMonth:dd MMMM} – {lastDayOfMonth:dd MMMM}";
        
        return $"{employeeName},{payPeriod},{grossIncome.ToString($"F{decimalPlaces}")},{incomeTax.ToString($"F{decimalPlaces}")},{netIncome.ToString($"F{decimalPlaces}")},{super.ToString($"F{decimalPlaces}")}";
    }

    [Fact]
    public void Should_GenerateCorrectCsvOutput_WhenJohnSmithDataIsProvided()
    {
        const string firstName = "John";
        const string lastName = "Smith";
        const decimal annualSalary = 60050m;
        const decimal superRate = 0.09m;
        const Month month = Month.March;
        const int year = 2024;
        
        const decimal expectedGrossIncome = 5004.17m;
        const decimal expectedIncomeTax = 919.58m;
        const decimal expectedNetIncome = 4084.58m;
        const decimal expectedSuper = 450.38m;
        
        var expectedCsv = BuildExpectedCsv("John Smith", month, year, 
            expectedGrossIncome, expectedIncomeTax, expectedNetIncome, expectedSuper);

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        result.ToCsvFormat(2).Should().Be(expectedCsv);
    }

    [Fact]
    public void Should_GenerateCorrectCsvOutput_WhenAlexWongDataIsProvided()
    {
        const string firstName = "Alex";
        const string lastName = "Wong";
        const decimal annualSalary = 120000m;
        const decimal superRate = 0.10m;
        const Month month = Month.March;
        const int year = 2024;
        
        const decimal expectedGrossIncome = 10000.00m;
        const decimal expectedIncomeTax = 2543.33m;
        const decimal expectedNetIncome = 7456.67m;
        const decimal expectedSuper = 1000.00m;
        
        var expectedCsv = BuildExpectedCsv("Alex Wong", month, year, 
            expectedGrossIncome, expectedIncomeTax, expectedNetIncome, expectedSuper);

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        result.ToCsvFormat(2).Should().Be(expectedCsv);
    }

    [Fact]
    public void Should_GenerateCorrectCsvOutput_WhenTestUserDataIsProvided()
    {
        const string firstName = "Test";
        const string lastName = "User";
        const decimal annualSalary = 60000m;
        const decimal superRate = 0.09m;
        const Month month = Month.March;
        const int year = 2024;
        
        const decimal expectedGrossIncome = 5000.00m;
        const decimal expectedIncomeTax = 918.33m;
        const decimal expectedNetIncome = 4081.67m;
        const decimal expectedSuper = 450.00m;
        
        var expectedCsv = BuildExpectedCsv("Test User", month, year, 
            expectedGrossIncome, expectedIncomeTax, expectedNetIncome, expectedSuper);

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        result.ToCsvFormat(2).Should().Be(expectedCsv);
    }

    [Fact]
    public void Should_FormatCsvOutput_WithTwoDecimalPlaces()
    {
        const string firstName = "Test";
        const string lastName = "User";
        const decimal annualSalary = 60000m;
        const decimal superRate = 0.09m;
        const Month month = Month.March;
        const int year = 2024;

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        var csvOutput = result.ToCsvFormat(2);
        
        csvOutput.Should().Contain(".00");
        csvOutput.Should().Contain(".00");
    }
}


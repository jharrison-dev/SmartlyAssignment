using Xunit;
using FluentAssertions;
using SmartlyAssignment.Core.Domain.Entities;
using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Tests.Domain.Entities;

public class PayslipTests
{

    [Fact]
    public void Should_FormatToCsv_WithExpectedFormat()
    {
        const string employeeName = "John Smith";
        const Month month = Month.March;
        const int year = 2024;
        const decimal grossIncome = 5004.17m;
        const decimal incomeTax = 919.58m;
        const decimal netIncome = 4084.59m;
        const decimal super = 450.38m;
        const string expectedCsvFormat = "John Smith,01 March – 31 March,5004.17,919.58,4084.59,450.38";

        var payslip = new Payslip(employeeName, month, year, grossIncome, incomeTax, netIncome, super);

        var result = payslip.ToCsvFormat();

        result.Should().Be(expectedCsvFormat);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_ThrowArgumentException_WhenEmployeeNameIsInvalid(string? employeeName)
    {
        const Month month = Month.March;
        const int year = 2024;
        const decimal grossIncome = 5004.17m;
        const decimal incomeTax = 919.58m;
        const decimal netIncome = 4084.59m;
        const decimal super = 450.38m;
        const string expectedErrorMessage = "Employee name cannot be null or empty (Parameter 'employeeName')";

        var action = () => new Payslip(employeeName!, month, year, grossIncome, incomeTax, netIncome, super);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Should_ThrowArgumentException_WhenYearIsNotPositive(int invalidYear)
    {
        const string employeeName = "John Smith";
        const Month month = Month.March;
        const decimal grossIncome = 5004.17m;
        const decimal incomeTax = 919.58m;
        const decimal netIncome = 4084.59m;
        const decimal super = 450.38m;
        const string expectedErrorMessage = "Year must be positive (Parameter 'year')";

        var action = () => new Payslip(employeeName, month, invalidYear, grossIncome, incomeTax, netIncome, super);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(-1000)]
    public void Should_ThrowArgumentException_WhenGrossIncomeIsNegative(decimal negativeValue)
    {
        const string employeeName = "John Smith";
        const Month month = Month.March;
        const int year = 2024;
        const decimal incomeTax = 919.58m;
        const decimal netIncome = 4084.59m;
        const decimal super = 450.38m;
        const string expectedErrorMessage = "Gross income cannot be negative (Parameter 'grossIncome')";

        var action = () => new Payslip(employeeName, month, year, negativeValue, incomeTax, netIncome, super);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(-1000)]
    public void Should_ThrowArgumentException_WhenIncomeTaxIsNegative(decimal negativeValue)
    {
        const string employeeName = "John Smith";
        const Month month = Month.March;
        const int year = 2024;
        const decimal grossIncome = 5004.17m;
        const decimal netIncome = 4084.59m;
        const decimal super = 450.38m;
        const string expectedErrorMessage = "Income tax cannot be negative (Parameter 'incomeTax')";

        var action = () => new Payslip(employeeName, month, year, grossIncome, negativeValue, netIncome, super);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(-1000)]
    public void Should_ThrowArgumentException_WhenNetIncomeIsNegative(decimal negativeValue)
    {
        const string employeeName = "John Smith";
        const Month month = Month.March;
        const int year = 2024;
        const decimal grossIncome = 5004.17m;
        const decimal incomeTax = 919.58m;
        const decimal super = 450.38m;
        const string expectedErrorMessage = "Net income cannot be negative (Parameter 'netIncome')";

        var action = () => new Payslip(employeeName, month, year, grossIncome, incomeTax, negativeValue, super);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(-1000)]
    public void Should_ThrowArgumentException_WhenSuperIsNegative(decimal negativeValue)
    {
        const string employeeName = "John Smith";
        const Month month = Month.March;
        const int year = 2024;
        const decimal grossIncome = 5004.17m;
        const decimal incomeTax = 919.58m;
        const decimal netIncome = 4084.59m;
        const string expectedErrorMessage = "Super cannot be negative (Parameter 'super')";

        var action = () => new Payslip(employeeName, month, year, grossIncome, incomeTax, netIncome, negativeValue);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }
}

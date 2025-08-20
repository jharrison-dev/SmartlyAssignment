using Xunit;
using FluentAssertions;
using SmartlyAssignment.Core.Domain.Entities;
using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Tests.Domain.Entities;

public class PayslipTests
{
    [Fact]
    public void Should_CreatePayslip_WhenValidDataIsProvided()
    {
        const string expectedEmployeeName = "John Smith";
        const Month expectedMonth = Month.March;
        const int expectedYear = 2024;
        const decimal expectedGrossIncome = 5004.17m;
        const decimal expectedIncomeTax = 919.58m;
        const decimal expectedNetIncome = 4084.59m;
        const decimal expectedSuper = 450.38m;

        var payslip = new Payslip(expectedEmployeeName, expectedMonth, expectedYear, expectedGrossIncome, expectedIncomeTax, expectedNetIncome, expectedSuper);

        payslip.EmployeeName.Should().Be(expectedEmployeeName);
        payslip.Month.Should().Be(expectedMonth);
        payslip.Year.Should().Be(expectedYear);
        payslip.GrossIncome.Should().Be(expectedGrossIncome);
        payslip.IncomeTax.Should().Be(expectedIncomeTax);
        payslip.NetIncome.Should().Be(expectedNetIncome);
        payslip.Super.Should().Be(expectedSuper);
    }

    [Fact]
    public void Should_FormatToCsv_WhenToCsvFormatIsCalled()
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
    public void ShouldNot_CreatePayslip_WhenEmployeeNameIsInvalid(string employeeName)
    {
        const Month month = Month.March;
        const int year = 2024;
        const decimal grossIncome = 5004.17m;
        const decimal incomeTax = 919.58m;
        const decimal netIncome = 4084.59m;
        const decimal super = 450.38m;
        const string expectedErrorMessage = "Employee name cannot be null or empty (Parameter 'employeeName')";

        var action = () => new Payslip(employeeName, month, year, grossIncome, incomeTax, netIncome, super);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void ShouldNot_CreatePayslip_WhenYearIsNotPositive(int invalidYear)
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
    public void ShouldNot_CreatePayslip_WhenMonetaryValuesAreNegative(decimal negativeValue)
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
    [InlineData(Month.March, 2024, "01 March – 31 March")]
    [InlineData(Month.April, 2024, "01 April – 30 April")]
    [InlineData(Month.February, 2024, "01 February – 29 February")] // 2024 is a leap year
    [InlineData(Month.February, 2023, "01 February – 28 February")] // 2023 is not a leap year
    public void Should_FormatPayPeriod_WhenToCsvFormatIsCalled(Month month, int year, string expectedPayPeriod)
    {
        const string employeeName = "John Smith";
        const decimal grossIncome = 5004.17m;
        const decimal incomeTax = 919.58m;
        const decimal netIncome = 4084.59m;
        const decimal super = 450.38m;

        var payslip = new Payslip(employeeName, month, year, grossIncome, incomeTax, netIncome, super);

        var csvResult = payslip.ToCsvFormat();
        csvResult.Should().Contain(expectedPayPeriod);
    }
}

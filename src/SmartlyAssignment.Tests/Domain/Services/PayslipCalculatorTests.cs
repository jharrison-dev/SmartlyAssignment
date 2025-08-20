using Xunit;
using FluentAssertions;
using SmartlyAssignment.Core.Domain.Services;
using SmartlyAssignment.Core.Domain.Entities;
using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Tests.Domain.Services;

public class PayslipCalculatorTests
{
    private readonly PayslipCalculator _sut;

    public PayslipCalculatorTests()
    {
        _sut = new PayslipCalculator();
    }

    [Fact]
    public void Should_CalculatePayslip_WhenJohnSmithDataIsProvided()
    {
        const string firstName = "John";
        const string lastName = "Smith";
        const decimal annualSalary = 60050m;
        const decimal superRate = 0.09m;
        const Month month = Month.March;
        const int year = 2024;
        
        const string expectedEmployeeName = "John Smith";
        const decimal expectedGrossIncome = 5004.17m;
        const decimal expectedIncomeTax = 919.58m;
        const decimal expectedNetIncome = 4084.59m;
        const decimal expectedSuper = 450.38m;

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        result.EmployeeName.Should().Be(expectedEmployeeName);
        result.Month.Should().Be(month);
        result.Year.Should().Be(year);
        result.GrossIncome.Should().Be(expectedGrossIncome);
        result.IncomeTax.Should().Be(expectedIncomeTax);
        result.NetIncome.Should().Be(expectedNetIncome);
        result.Super.Should().Be(expectedSuper);
    }

    [Fact]
    public void Should_CalculatePayslip_WhenAlexWongDataIsProvided()
    {
        const string firstName = "Alex";
        const string lastName = "Wong";
        const decimal annualSalary = 120000m;
        const decimal superRate = 0.10m;
        const Month month = Month.March;
        const int year = 2024;
        
        const string expectedEmployeeName = "Alex Wong";
        const decimal expectedGrossIncome = 10000.00m;
        const decimal expectedIncomeTax = 2543.33m;
        const decimal expectedNetIncome = 7456.67m;
        const decimal expectedSuper = 1000.00m;

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        result.EmployeeName.Should().Be(expectedEmployeeName);
        result.Month.Should().Be(month);
        result.Year.Should().Be(year);
        result.GrossIncome.Should().Be(expectedGrossIncome);
        result.IncomeTax.Should().Be(expectedIncomeTax);
        result.NetIncome.Should().Be(expectedNetIncome);
        result.Super.Should().Be(expectedSuper);
    }

    [Fact]
    public void Should_CalculateGrossIncome_WhenAnnualSalaryIsProvided()
    {
        const string firstName = "Test";
        const string lastName = "User";
        const decimal annualSalary = 60000m;
        const decimal superRate = 0.09m;
        const Month month = Month.March;
        const int year = 2024;
        const decimal expectedGrossIncome = 5000.00m;

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        result.GrossIncome.Should().Be(expectedGrossIncome);
    }

    [Fact]
    public void Should_CalculateSuper_WhenGrossIncomeAndSuperRateAreProvided()
    {
        const string firstName = "Test";
        const string lastName = "User";
        const decimal annualSalary = 60000m;
        const decimal superRate = 0.09m;
        const Month month = Month.March;
        const int year = 2024;
        const decimal expectedSuper = 450.00m;

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        result.Super.Should().Be(expectedSuper);
    }

    [Fact]
    public void Should_CalculateNetIncome_WhenGrossIncomeAndIncomeTaxAreProvided()
    {
        const string firstName = "Test";
        const string lastName = "User";
        const decimal annualSalary = 60000m;
        const decimal superRate = 0.09m;
        const Month month = Month.March;
        const int year = 2024;

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        result.NetIncome.Should().Be(result.GrossIncome - result.IncomeTax);
    }

    [Fact]
    public void Should_RoundAllCalculations_WhenMonetaryValuesAreCalculated()
    {
        const string firstName = "Test";
        const string lastName = "User";
        const decimal annualSalary = 60000m;
        const decimal superRate = 0.09m;
        const Month month = Month.March;
        const int year = 2024;

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        var result = _sut.CalculatePayslip(employee, month, year);

        var grossIncomeString = result.GrossIncome.ToString("F2");
        var incomeTaxString = result.IncomeTax.ToString("F2");
        var netIncomeString = result.NetIncome.ToString("F2");
        var superString = result.Super.ToString("F2");

        result.GrossIncome.ToString().Should().Be(grossIncomeString);
        result.IncomeTax.ToString().Should().Be(incomeTaxString);
        result.NetIncome.ToString().Should().Be(netIncomeString);
        result.Super.ToString().Should().Be(superString);
    }
}

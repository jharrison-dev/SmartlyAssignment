using Xunit;
using FluentAssertions;
using SmartlyAssignment.Core.Domain.Entities;

namespace SmartlyAssignment.Tests.Domain.Entities;

public class EmployeeTests
{
    [Fact]
    public void Should_CreateEmployee_WhenValidDataIsProvided()
    {
        const string expectedFirstName = "John";
        const string expectedLastName = "Smith";
        const decimal expectedAnnualSalary = 60050m;
        const decimal expectedSuperRate = 0.09m;
        const string expectedFullName = "John Smith";

        var employee = new Employee(expectedFirstName, expectedLastName, expectedAnnualSalary, expectedSuperRate);

        employee.FirstName.Should().Be(expectedFirstName);
        employee.LastName.Should().Be(expectedLastName);
        employee.AnnualSalary.Should().Be(expectedAnnualSalary);
        employee.SuperRate.Should().Be(expectedSuperRate);
        employee.FullName.Should().Be(expectedFullName);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(0.25)]
    [InlineData(0.5)]
    public void Should_AcceptSuperRate_WhenRateIsWithinValidRange(decimal superRate)
    {
        const string firstName = "John";
        const string lastName = "Smith";
        const decimal annualSalary = 60050m;

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        employee.SuperRate.Should().Be(superRate);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(60050)]
    public void Should_AcceptAnnualSalary_WhenSalaryIsNonNegative(decimal annualSalary)
    {
        const string firstName = "John";
        const string lastName = "Smith";
        const decimal superRate = 0.09m;

        var employee = new Employee(firstName, lastName, annualSalary, superRate);

        employee.AnnualSalary.Should().Be(annualSalary);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ShouldNot_CreateEmployee_WhenFirstNameIsInvalid(string firstName)
    {
        const string lastName = "Smith";
        const decimal annualSalary = 60050m;
        const decimal superRate = 0.09m;
        const string expectedErrorMessage = "First name cannot be null or empty (Parameter 'firstName')";

        var action = () => new Employee(firstName, lastName, annualSalary, superRate);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ShouldNot_CreateEmployee_WhenLastNameIsInvalid(string lastName)
    {
        const string firstName = "John";
        const decimal annualSalary = 60050m;
        const decimal superRate = 0.09m;
        const string expectedErrorMessage = "Last name cannot be null or empty (Parameter 'lastName')";

        var action = () => new Employee(firstName, lastName, annualSalary, superRate);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(-1000)]
    public void ShouldNot_CreateEmployee_WhenAnnualSalaryIsNegative(decimal annualSalary)
    {
        const string firstName = "John";
        const string lastName = "Smith";
        const decimal superRate = 0.09m;
        const string expectedErrorMessage = "Annual salary must be non-negative (Parameter 'annualSalary')";

        var action = () => new Employee(firstName, lastName, annualSalary, superRate);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(0.51)]
    [InlineData(1.0)]
    public void ShouldNot_CreateEmployee_WhenSuperRateIsOutOfRange(decimal superRate)
    {
        const string firstName = "John";
        const string lastName = "Smith";
        const decimal annualSalary = 60050m;
        const string expectedErrorMessage = "Super rate must be between 0% and 50% (Parameter 'superRate')";

        var action = () => new Employee(firstName, lastName, annualSalary, superRate);
        
        action.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }
}

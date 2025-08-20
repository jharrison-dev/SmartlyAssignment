namespace SmartlyAssignment.Core.Domain.Entities;

public class Employee
{
    public string FirstName { get; }
    public string LastName { get; }
    public decimal AnnualSalary { get; }
    public decimal SuperRate { get; }

    public Employee(string firstName, string lastName, decimal annualSalary, decimal superRate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be null or empty", nameof(firstName));
        }
        
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));
        }
        
        if (annualSalary < 0)
        {
            throw new ArgumentException("Annual salary must be non-negative", nameof(annualSalary));
        }
        
        if (superRate < 0 || superRate > 0.5m)
        {
            throw new ArgumentException("Super rate must be between 0% and 50%", nameof(superRate));
        }

        FirstName = firstName;
        LastName = lastName;
        AnnualSalary = annualSalary;
        SuperRate = superRate;
    }

    public string FullName => $"{FirstName} {LastName}";
}

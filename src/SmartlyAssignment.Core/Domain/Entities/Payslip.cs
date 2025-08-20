using System.Globalization;
using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Core.Domain.Entities;

public class Payslip
{
    public string EmployeeName { get; }
    public Month Month { get; }
    public int Year { get; }
    public decimal GrossIncome { get; }
    public decimal IncomeTax { get; }
    public decimal NetIncome { get; }
    public decimal Super { get; }

    public Payslip(string employeeName, Month month, int year, decimal grossIncome, decimal incomeTax, decimal netIncome, decimal super)
    {
        if (string.IsNullOrWhiteSpace(employeeName))
        {
            throw new ArgumentException("Employee name cannot be null or empty", nameof(employeeName));
        }
        
        if (year <= 0)
        {
            throw new ArgumentException("Year must be positive", nameof(year));
        }
        
        if (grossIncome < 0)
        {
            throw new ArgumentException("Gross income cannot be negative", nameof(grossIncome));
        }
        
        if (incomeTax < 0)
        {
            throw new ArgumentException("Income tax cannot be negative", nameof(incomeTax));
        }
        
        if (netIncome < 0)
        {
            throw new ArgumentException("Net income cannot be negative", nameof(netIncome));
        }
        
        if (super < 0)
        {
            throw new ArgumentException("Super cannot be negative", nameof(super));
        }

        EmployeeName = employeeName;
        Month = month;
        Year = year;
        GrossIncome = grossIncome;
        IncomeTax = incomeTax;
        NetIncome = netIncome;
        Super = super;
    }

    private string CalculatePayPeriod()
    {
        var firstDayOfMonth = new DateTime(Year, (int)Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        return $"{firstDayOfMonth:dd MMMM} – {lastDayOfMonth:dd MMMM}";
    }

    public string ToCsvFormat()
    {
        return $"{EmployeeName},{CalculatePayPeriod()},{GrossIncome:F2},{IncomeTax:F2},{NetIncome:F2},{Super:F2}";
    }
}

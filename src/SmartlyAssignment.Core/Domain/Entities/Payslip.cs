using System.Globalization;
using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Core.Domain.Entities;

public class Payslip
{
    private readonly string _employeeName;
    private readonly Month _month;
    private readonly int _year;
    private readonly decimal _grossIncome;
    private readonly decimal _incomeTax;
    private readonly decimal _netIncome;
    private readonly decimal _super;

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

        _employeeName = employeeName;
        _month = month;
        _year = year;
        _grossIncome = grossIncome;
        _incomeTax = incomeTax;
        _netIncome = netIncome;
        _super = super;
    }

    private string CalculatePayPeriod()
    {
        var firstDayOfMonth = new DateTime(_year, (int)_month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        return $"{firstDayOfMonth:dd MMMM} – {lastDayOfMonth:dd MMMM}";
    }

    public string ToCsvFormat(int decimalPlaces = 2)
    {
        return $"{_employeeName},{CalculatePayPeriod()},{_grossIncome.ToString($"F{decimalPlaces}")},{_incomeTax.ToString($"F{decimalPlaces}")},{_netIncome.ToString($"F{decimalPlaces}")},{_super.ToString($"F{decimalPlaces}")}";
    }
}

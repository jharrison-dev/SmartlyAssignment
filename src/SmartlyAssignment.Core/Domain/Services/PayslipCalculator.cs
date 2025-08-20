using SmartlyAssignment.Core.Domain.Entities;
using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Core.Domain.Services;

public class PayslipCalculator
{
    private readonly ITaxBracketService _taxBracketService;

    public PayslipCalculator(ITaxBracketService taxBracketService)
    {
        _taxBracketService = taxBracketService ?? throw new ArgumentNullException(nameof(taxBracketService));
    }

    public Payslip CalculatePayslip(Employee employee, Month month, int year)
    {
        var grossMonthlyIncome = employee.AnnualSalary / 12m;
        
        var annualTaxedIncome = CalculateTaxedIncome(employee.AnnualSalary);
        var monthlyTaxedIncome = annualTaxedIncome / 12m;
        
        var netIncome = grossMonthlyIncome - monthlyTaxedIncome;
        
        var super = grossMonthlyIncome * employee.SuperRate;

        return new Payslip(
            employee.FullName,
            month,
            year,
            grossMonthlyIncome,
            monthlyTaxedIncome,
            netIncome,
            super
        );
    }

    private decimal CalculateTaxedIncome(decimal annualSalary)
    {
        var taxBrackets = _taxBracketService.GetOrderedTaxBrackets();
        var totalTax = 0m;
        var remainingUntaxedIncome = annualSalary;
        
        foreach (var bracket in taxBrackets)
        {
            if (remainingUntaxedIncome <= 0)
            {
                break;
            }
                
            var taxableInThisBracket = Math.Min(
                remainingUntaxedIncome, 
                bracket.UpperBound - bracket.LowerBound
            );
            
            totalTax += taxableInThisBracket * bracket.Rate;
            
            remainingUntaxedIncome -= taxableInThisBracket;
        }
        
        return totalTax;
    }
}

using SmartlyAssignment.Core.Domain.Entities;
using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Core.Domain.Services;

public class PayslipCalculator
{
    public Payslip CalculatePayslip(Employee employee, Month month, int year)
    {
        // TODO: Implement actual calculation logic
        // For now, return a stub to make tests fail as expected
        return new Payslip(
            employee.FullName,
            month,
            year,
            0, // Gross income
            0, // Income tax
            0, // Net income
            0  // Super
        );
    }
}

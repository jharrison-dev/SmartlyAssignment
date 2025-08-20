using SmartlyAssignment.Core.Domain.Entities;
using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Core.Domain.Services;

public interface IPayslipCalculator
{
    Payslip CalculatePayslip(Employee employee, Month month, int year);
}

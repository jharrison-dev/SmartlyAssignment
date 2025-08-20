using SmartlyAssignment.Core.Domain.Enums;

namespace SmartlyAssignment.Api.Requests;

public class PayslipRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public decimal AnnualSalary { get; set; }
    public decimal SuperRate { get; set; }
    public Month Month { get; set; }
}

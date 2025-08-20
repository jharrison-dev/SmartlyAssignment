using Microsoft.AspNetCore.Mvc;
using SmartlyAssignment.Core.Domain.Services;
using SmartlyAssignment.Core.Domain.Entities;
using SmartlyAssignment.Core.Domain.Enums;
using SmartlyAssignment.Api.Requests;
using SmartlyAssignment.Api.Responses;

namespace SmartlyAssignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PayslipController : ControllerBase
{
    private readonly IPayslipCalculator _payslipCalculator;
    private readonly ITaxBracketService _taxBracketService;

    public PayslipController(IPayslipCalculator payslipCalculator, ITaxBracketService taxBracketService)
    {
        _payslipCalculator = payslipCalculator ?? throw new ArgumentNullException(nameof(payslipCalculator));
        _taxBracketService = taxBracketService ?? throw new ArgumentNullException(nameof(taxBracketService));
    }

    [HttpPost("calculate")]
    public ActionResult<PayslipResponse> CalculatePayslip([FromBody] PayslipRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee(request.FirstName, request.LastName, request.AnnualSalary, request.SuperRate);
            var payslip = _payslipCalculator.CalculatePayslip(employee, request.Month, DateTime.UtcNow.Year);

            var response = new PayslipResponse
            {
                CsvOutput = payslip.ToCsvFormat()
            };

            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "An unexpected error occurred while calculating the payslip." });
        }
    }
}

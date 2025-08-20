using Xunit;
using SmartlyAssignment.Core.Domain.Services;

namespace SmartlyAssignment.Tests.Domain.Services;

public class PayslipCalculatorTests
{
    private readonly PayslipCalculator _sut;

    public PayslipCalculatorTests()
    {
        _sut = new PayslipCalculator();
    }

    [Fact]
    public void Should_ReturnOne_When_CalculatePayslipIsCalled()
    {
        var result = _sut.CalculatePayslip();

        // Assert
        Assert.Equal(1, result);
    }
}

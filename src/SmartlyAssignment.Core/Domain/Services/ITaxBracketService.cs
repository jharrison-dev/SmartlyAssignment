using SmartlyAssignment.Core.Domain.Entities;

namespace SmartlyAssignment.Core.Domain.Services;

public interface ITaxBracketService
{
    IEnumerable<TaxBracket> GetOrderedTaxBrackets();
}

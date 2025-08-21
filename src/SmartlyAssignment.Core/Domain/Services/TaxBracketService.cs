using SmartlyAssignment.Core.Domain.Entities;
using SmartlyAssignment.Core.Domain.Configuration;
using Microsoft.Extensions.Options;

namespace SmartlyAssignment.Core.Domain.Services;

public class TaxBracketService : ITaxBracketService
{
    private readonly TaxBracketConfiguration _configuration;

    public TaxBracketService(IOptions<TaxBracketConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public IEnumerable<TaxBracket> GetOrderedTaxBrackets()
    {
        var brackets = _configuration.Brackets
            .Select(b => new TaxBracket(b.LowerBound, b.UpperBound, b.Rate))
            .OrderBy(b => b.LowerBound)
            .ToList();

        ValidateContinuousBounds(brackets);
        return brackets;
    }

    private void ValidateContinuousBounds(IList<TaxBracket> orderedBrackets)
    {
        if (!orderedBrackets.Any())
        {
            throw new InvalidOperationException("Tax brackets collection cannot be empty");
        }
        
        var previous = orderedBrackets.First();
        foreach (var current in orderedBrackets.Skip(1))
        {
            if (previous.UpperBound != current.LowerBound)
            {
                throw new InvalidOperationException($"Gap detected between tax brackets: {previous.UpperBound} and {current.LowerBound}");
            }
            previous = current;
        }
    }
}

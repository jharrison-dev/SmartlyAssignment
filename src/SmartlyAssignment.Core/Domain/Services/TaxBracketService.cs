using SmartlyAssignment.Core.Domain.Entities;

namespace SmartlyAssignment.Core.Domain.Services;

public class TaxBracketService : ITaxBracketService
{
    public IEnumerable<TaxBracket> GetOrderedTaxBrackets()
    {
        var brackets = new[]
        {
            new TaxBracket(0m, 14000m, 0.105m),
            new TaxBracket(14000m, 48000m, 0.175m),
            new TaxBracket(48000m, 70000m, 0.30m),
            new TaxBracket(70000m, 180000m, 0.33m),
            new TaxBracket(180000m, decimal.MaxValue, 0.39m)
        };

        var orderedBrackets = brackets.OrderBy(b => b.LowerBound).ToList();
        ValidateContinuousBounds(orderedBrackets);
        return orderedBrackets;
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

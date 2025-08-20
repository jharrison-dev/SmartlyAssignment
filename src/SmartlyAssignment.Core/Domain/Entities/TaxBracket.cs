namespace SmartlyAssignment.Core.Domain.Entities;

public class TaxBracket
{
    public decimal LowerBound { get; }
    public decimal UpperBound { get; }
    public decimal Rate { get; }

    public TaxBracket(decimal lowerBound, decimal upperBound, decimal rate)
    {
        if (lowerBound < 0)
        {
            throw new ArgumentException("Lower bound cannot be negative", nameof(lowerBound));
        }
        
        if (upperBound <= lowerBound)
        {
            throw new ArgumentException("Upper bound must be greater than lower bound", nameof(upperBound));
        }
        
        if (rate < 0 || rate > 1)
        {
            throw new ArgumentException("Rate must be between 0 and 1", nameof(rate));
        }

        LowerBound = lowerBound;
        UpperBound = upperBound;
        Rate = rate;
    }

    public decimal GetTaxableAmount(decimal income)
    {
        if (income <= LowerBound)
        {
            return 0m;
        }
            
        var taxableInThisBracket = Math.Min(income - LowerBound, UpperBound - LowerBound);
        return taxableInThisBracket;
    }
}

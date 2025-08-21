namespace SmartlyAssignment.Core.Domain.Configuration;

public class TaxBracketConfiguration
{
    public const string SectionName = "TaxBrackets";
    
    public List<TaxBracketSettings> Brackets { get; set; } = new();
}

public class TaxBracketSettings
{
    public decimal LowerBound { get; set; }
    public decimal UpperBound { get; set; }
    public decimal Rate { get; set; }
}

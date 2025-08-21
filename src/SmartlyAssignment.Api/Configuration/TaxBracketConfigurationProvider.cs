using SmartlyAssignment.Core.Domain.Configuration;
using Microsoft.Extensions.Options;

namespace SmartlyAssignment.Api.Configuration;

public class TaxBracketConfigurationProvider : ITaxBracketConfiguration
{
    private readonly TaxBracketConfiguration _configuration;

    public TaxBracketConfigurationProvider(IOptions<TaxBracketConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public List<TaxBracketSettings> Brackets => _configuration.Brackets;
}

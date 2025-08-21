using SmartlyAssignment.Core.Domain.Services;
using SmartlyAssignment.Core.Domain.Configuration;
using SmartlyAssignment.Api.Configuration;

// Force Development environment and port 5140 for this dev project
Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "http://localhost:5140");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Configure tax bracket settings
builder.Services.Configure<TaxBracketConfiguration>(
    builder.Configuration.GetSection(TaxBracketConfiguration.SectionName));

// Register configuration provider
builder.Services.AddScoped<ITaxBracketConfiguration, TaxBracketConfigurationProvider>();

// Register our domain services
builder.Services.AddScoped<ITaxBracketService, TaxBracketService>();
builder.Services.AddScoped<IPayslipCalculator, PayslipCalculator>();

// Add CORS for frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection(); // Disabled for development
app.UseDefaultFiles(); // Add default file serving (index.html)
app.UseStaticFiles(); // Enable static file serving
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();

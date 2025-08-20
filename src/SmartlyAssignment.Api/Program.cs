using SmartlyAssignment.Core.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

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

using Microsoft.EntityFrameworkCore;
using HelloApi.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Versioning;
using HelloApi.Repository;
using Microsoft.AspNetCore.HttpLogging;
using FluentValidation;
using System.Globalization;
using HelloApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Override default Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// HTTP Logging options
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath | HttpLoggingFields.ResponseStatusCode;
});

// Retrieve database connection password from Azure Key Vault secret
var client = new SecretClient(new Uri("https://mykeyvalue2023.vault.azure.net/"), new AzureCliCredential());
KeyVaultSecret secretDbConnStr = await client.GetSecretAsync("MyDatabaseConnectionString");
string dbConnStr = secretDbConnStr.Value;

// Setup DB connection
var connStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("MyDatabase"));
connStringBuilder.ConnectionString = dbConnStr;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(connStringBuilder.ConnectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Register Validation
builder.Services.AddScoped<IValidator<TodoItem>, TodoItemValidator>();

// Add JWT authentication
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

// Add versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

// Add Azure Application Insights
KeyVaultSecret secretAppInsightsConnStr = await client.GetSecretAsync("HelloApiApplicationInsightsConnString");
string appInsightsConnStr = secretAppInsightsConnStr.Value;
builder.Services.AddApplicationInsightsTelemetry(opt => opt.ConnectionString = appInsightsConnStr);

var app = builder.Build();

// Enable HTTP logging
app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware
app.UseRequestCulture();

app.UseAuthorization();

app.MapControllers();

app.Run(async (context) =>
{
    await context.Response.WriteAsync(
        $"CurrentCulture.DisplayName: {CultureInfo.CurrentCulture.DisplayName}");
});

app.Run();




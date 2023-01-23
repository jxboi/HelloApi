using Microsoft.EntityFrameworkCore;
using HelloApi.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Data.SqlClient;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Override default Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Retrieve database connection password from Azure Key Vault secret
var client = new SecretClient(new Uri("https://mykeyvalue2023.vault.azure.net/"), new DefaultAzureCredential());
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

// Add Azure Application Insights
KeyVaultSecret secretAppInsightsConnStr = await client.GetSecretAsync("HelloApiApplicationInsightsConnString");
string appInsightsConnStr = secretAppInsightsConnStr.Value;
builder.Services.AddApplicationInsightsTelemetry(opt => opt.ConnectionString = appInsightsConnStr);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


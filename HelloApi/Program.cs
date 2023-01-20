using Microsoft.EntityFrameworkCore;
using HelloApi.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Retrieve database connection password from Azure Key Vault secret
var client = new SecretClient(new Uri("https://mykeyvalue2023.vault.azure.net/"), new DefaultAzureCredential());
KeyVaultSecret secretUserId = await client.GetSecretAsync("MyDatabaseDbUserId");
string dbUserId = secretUserId.Value;
KeyVaultSecret secretPw = await client.GetSecretAsync("MyDatabaseDbPw");
string dbPassword = secretPw.Value;

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

// Setup DB connection
var connStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("MyDatabase"));
connStringBuilder.DataSource = "tcp:server2023a.database.windows.net,1433";
connStringBuilder.InitialCatalog = "MyDatabase";
connStringBuilder.UserID = dbUserId;
connStringBuilder.Password = dbPassword;

builder.Services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(connStringBuilder.ConnectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


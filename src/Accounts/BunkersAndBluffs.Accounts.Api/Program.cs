using BunkersAndBluffs.Accounts.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

builder.Services.AddDbContext<AccountsDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("Accounts")
            ?? throw new InvalidOperationException("Connection string "
            + "'Accounts' not found.");

    options.UseSqlServer(connectionString,
            x => x.MigrationsHistoryTable("__EFMigrationsHistory", "accounts"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program 
{ 

}



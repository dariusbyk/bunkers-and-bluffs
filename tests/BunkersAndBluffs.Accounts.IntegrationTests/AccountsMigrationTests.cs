using BunkersAndBluffs.Accounts.Infrastructure;
using BunkersAndBluffs.Accounts.Infrastructure.Migrations;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BunkersAndBluffs.Accounts.IntegrationTests
{
    public class AccountsMigrationTests : IClassFixture<SqlServerFixture>
    {
        SqlServerFixture fixture;

        public AccountsMigrationTests(SqlServerFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task InitialMigration_CreatesUsersTable()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(fixture.ConnectionString);

            builder.InitialCatalog = "AccountsMigrationTests";
            string connectionString = builder.ConnectionString;

            using WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>();
            Dictionary<string, string?> connections = new Dictionary<string, string?>();
            connections.Add("ConnectionStrings:Accounts", connectionString);

            var testFactory = factory.WithWebHostBuilder(hostBuilder =>
            {
                hostBuilder.ConfigureAppConfiguration((_, config) =>
                {
                    config.AddInMemoryCollection(connections);
                });
            });

            using var scope = testFactory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AccountsDbContext>();

            await dbContext.Database.MigrateAsync();
            Assert.False(await dbContext.Users.AnyAsync());

            var appliedMigrations = await dbContext.Database.GetAppliedMigrationsAsync();
            Assert.Contains("20261109180804_InitialAccounts", appliedMigrations);
        }
    }
}

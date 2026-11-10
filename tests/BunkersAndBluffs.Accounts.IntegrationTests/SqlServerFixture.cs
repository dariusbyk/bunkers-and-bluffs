using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.MsSql;

namespace BunkersAndBluffs.Accounts.IntegrationTests
{
    public class SqlServerFixture : IAsyncLifetime
    {
        private readonly MsSqlContainer _container =
            new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
                .Build();

        public string ConnectionString 
        { 
            get => _container.GetConnectionString(); 
        }


        public async Task DisposeAsync()
        {
            await _container.DisposeAsync();
        }
        public async Task InitializeAsync()
        {
            await _container.StartAsync();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BunkersAndBluffs.Accounts.Infrastructure
{
    public class AccountsDbContext : IdentityUserContext<IdentityUser>
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("accounts");
            builder.Entity<IdentityUser>(b =>
            {
                b.ToTable("Users");
                b.HasIndex(с => с.NormalizedEmail).IsUnique();
            });
        }
    }
}

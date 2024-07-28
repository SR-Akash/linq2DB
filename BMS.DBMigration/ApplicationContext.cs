using System;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
 
using BMS.Helper.Enums;

namespace BMS.DBMigration
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
             : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = @"Data Source=10.209.99.144;Initial Catalog=LMS;User ID=smeapp;Password=sds#dt454sesa0wdnp@1vpo#98;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;";
            if (string.IsNullOrEmpty(connectionString))
                throw new System.Exception("DB Server connection string configuration required");

            if (!options.IsConfigured)
            {
                options
                    .UseNpgsql(connectionString, x => x.MigrationsAssembly("BMS.DBMigration"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

           
            builder.HasDefaultSchema("security");

            base.OnModelCreating(builder);

            //builder.UseOpenIddict<ApplicationClient, ApplicationAuthorization, ApplicationScope, ApplicationToken, long>();
        }
    }
}

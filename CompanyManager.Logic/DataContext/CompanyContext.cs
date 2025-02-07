﻿using CompanyManager.Logic.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Logic.DataContext
{
    internal class CompanyContext : DbContext, IContext
    {
        #region fields
        private static string DatabaseType = "Sqlite";
        private static string ConnectionString = "data source=CompanyManager.db";
        #endregion fields

        static CompanyContext()
        {
            var appSettings = Modules.AppSettings.Instance;

            DatabaseType = appSettings["Database:Type"] ?? DatabaseType;
            ConnectionString = appSettings[$"ConnectionStrings:{DatabaseType}ConnectionString"] ?? ConnectionString;
        }
        public DbSet<Entities.Company> CompanySet { get; set; }
        public DbSet<Entities.Customer> CustomerSet { get; set; }
        public DbSet<Entities.Employee> EmployeeSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (DatabaseType == "Sqlite")
            {
                optionsBuilder.UseSqlite(ConnectionString);
            }
            else if (DatabaseType == "SqlServer")
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}

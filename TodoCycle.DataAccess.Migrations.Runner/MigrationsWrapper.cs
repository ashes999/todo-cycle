using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using FluentMigrator;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Text;

namespace TodoCycle.DataAccess.Migrations.Runner
{
    public class MigrationsWrapper
    {
        private StringBuilder output = new StringBuilder();
        private Assembly migrationsAssembly;
        private ConnectionStringSettings connectionString;

        public MigrationsWrapper(Assembly migrationsAssembly, ConnectionStringSettings connectionString)
        {
            this.migrationsAssembly = migrationsAssembly;
            this.connectionString = connectionString;
        }

        private MigrationRunner GetMigrator()
        {
            var announcer = new TextWriterAnnouncer(s => output.Append(s));

            var migrationContext = new RunnerContext(announcer);
            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2008ProcessorFactory();
            var processor = factory.Create(connectionString.ConnectionString, announcer, options);
            var runner = new MigrationRunner(migrationsAssembly, migrationContext, processor);

            return runner;
        }

        public void MigrateToLatestVersion()
        {
            var runner = GetMigrator();
            runner.MigrateUp();
        }
        
        public string Output {  get { return this.output.ToString(); } }

        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }

            public string ProviderSwitches { get; }

            public int Timeout { get; set; }
        }
    }
}

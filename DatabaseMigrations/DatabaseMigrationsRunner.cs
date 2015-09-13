using System;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace DatabaseMigrations
{
    public class DatabaseMigrationRunner
    {
        public static bool MigrateDatabase(string databaseType, string connectionString)
        {
            try {
                RunnerContext runnerContext = new RunnerContext(new TextWriterAnnouncer(Console.Out)) {
                    Database = databaseType,
                    Connection = connectionString,
                    Targets = new[] { typeof(DatabaseMigrationRunner).Assembly.Location },
                };

                new TaskExecutor(runnerContext).Execute();
            } catch {
                return false;
            }

            return true;
        }
    }
}

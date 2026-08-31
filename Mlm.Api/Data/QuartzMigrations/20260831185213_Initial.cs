using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mlm.Api.Data.QuartzMigrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(ReadQuartzSql());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DROP TABLE IF EXISTS qrtz_simple_triggers CASCADE;
                DROP TABLE IF EXISTS qrtz_simprop_triggers CASCADE;
                DROP TABLE IF EXISTS qrtz_cron_triggers CASCADE;
                DROP TABLE IF EXISTS qrtz_blob_triggers CASCADE;
                DROP TABLE IF EXISTS qrtz_triggers CASCADE;
                DROP TABLE IF EXISTS qrtz_fired_triggers CASCADE;
                DROP TABLE IF EXISTS qrtz_paused_trigger_grps CASCADE;
                DROP TABLE IF EXISTS qrtz_calendars CASCADE;
                DROP TABLE IF EXISTS qrtz_scheduler_state CASCADE;
                DROP TABLE IF EXISTS qrtz_locks CASCADE;
                DROP TABLE IF EXISTS qrtz_job_details CASCADE;
                """);
        }

        private static string ReadQuartzSql()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Data", "Scripts", "0002_quartz_postgres.sql");
            if (!File.Exists(path))
            {
                path = Path.GetFullPath(Path.Combine("Data", "Scripts", "0002_quartz_postgres.sql"));
            }

            return File.ReadAllText(path);
        }
    }
}

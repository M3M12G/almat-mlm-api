using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mlm.Api.Data.Migrations;

/// <inheritdoc />
public partial class ReplaceTickerQWithQuartz : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CronTickerOccurrences",
            schema: "ticker");

        migrationBuilder.DropTable(
            name: "TimeTickers",
            schema: "ticker");

        migrationBuilder.DropTable(
            name: "CronTickers",
            schema: "ticker");

        migrationBuilder.Sql("DROP SCHEMA IF EXISTS ticker CASCADE;");

        migrationBuilder.Sql(ReadQuartzSql());
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            DROP TABLE IF EXISTS qrtz_fired_triggers;
            DROP TABLE IF EXISTS qrtz_paused_trigger_grps;
            DROP TABLE IF EXISTS qrtz_scheduler_state;
            DROP TABLE IF EXISTS qrtz_locks;
            DROP TABLE IF EXISTS qrtz_simprop_triggers;
            DROP TABLE IF EXISTS qrtz_simple_triggers;
            DROP TABLE IF EXISTS qrtz_cron_triggers;
            DROP TABLE IF EXISTS qrtz_blob_triggers;
            DROP TABLE IF EXISTS qrtz_triggers;
            DROP TABLE IF EXISTS qrtz_job_details;
            DROP TABLE IF EXISTS qrtz_calendars;
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

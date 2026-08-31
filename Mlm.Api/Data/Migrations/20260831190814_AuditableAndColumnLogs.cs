using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mlm.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditableAndColumnLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "metadata",
                table: "audit_log");

            migrationBuilder.RenameColumn(
                name: "entity_id",
                table: "audit_log",
                newName: "operation_id");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "withdrawal_requests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "withdrawal_requests",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "withdrawal_requests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "withdrawal_requests",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "ranks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "ranks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "ranks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "ranks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "rank_achievements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "rank_achievements",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "rank_achievements",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "rank_achievements",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "purchases",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "purchases",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "purchases",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "pool_periods",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "pool_periods",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "pool_periods",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "pool_distributions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "pool_distributions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "pool_distributions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "pool_distributions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "packages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "packages",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "packages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "packages",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "bonus_transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "bonus_transactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "bonus_transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "bonus_rules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "bonus_rules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "bonus_rules",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "bonus_rules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "auth_sessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "auth_sessions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "auth_sessions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "auth_sessions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "entity_type",
                table: "audit_log",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "action",
                table: "audit_log",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "entity_key",
                table: "audit_log",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "new_value",
                table: "audit_log",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "old_value",
                table: "audit_log",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "property_name",
                table: "audit_log",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "accounting_entries",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "updated_at",
                table: "accounting_entries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "accounting_entries",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_created_at",
                table: "audit_log",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_entity_type_entity_key",
                table: "audit_log",
                columns: new[] { "entity_type", "entity_key" });

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_operation_id",
                table: "audit_log",
                column: "operation_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_audit_log_created_at",
                table: "audit_log");

            migrationBuilder.DropIndex(
                name: "IX_audit_log_entity_type_entity_key",
                table: "audit_log");

            migrationBuilder.DropIndex(
                name: "IX_audit_log_operation_id",
                table: "audit_log");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "withdrawal_requests");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "withdrawal_requests");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "withdrawal_requests");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "withdrawal_requests");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "ranks");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "ranks");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "ranks");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "ranks");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "rank_achievements");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "rank_achievements");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "rank_achievements");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "rank_achievements");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "purchases");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "purchases");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "purchases");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "pool_periods");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "pool_periods");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "pool_periods");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "pool_distributions");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "pool_distributions");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "pool_distributions");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "pool_distributions");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "packages");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "packages");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "packages");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "packages");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "bonus_transactions");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "bonus_transactions");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "bonus_transactions");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "bonus_rules");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "bonus_rules");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "bonus_rules");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "bonus_rules");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "auth_sessions");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "auth_sessions");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "auth_sessions");

            migrationBuilder.DropColumn(
                name: "entity_key",
                table: "audit_log");

            migrationBuilder.DropColumn(
                name: "new_value",
                table: "audit_log");

            migrationBuilder.DropColumn(
                name: "old_value",
                table: "audit_log");

            migrationBuilder.DropColumn(
                name: "property_name",
                table: "audit_log");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "accounting_entries");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "accounting_entries");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "accounting_entries");

            migrationBuilder.RenameColumn(
                name: "operation_id",
                table: "audit_log",
                newName: "entity_id");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "auth_sessions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<string>(
                name: "entity_type",
                table: "audit_log",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "action",
                table: "audit_log",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(16)",
                oldMaxLength: 16);

            migrationBuilder.AddColumn<string>(
                name: "metadata",
                table: "audit_log",
                type: "jsonb",
                nullable: true);
        }
    }
}

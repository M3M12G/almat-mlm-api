using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mlm.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class GuidIdsOnAuditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_users_ranks_rank_id", table: "users");
            migrationBuilder.DropForeignKey(name: "FK_rank_achievements_ranks_rank_id", table: "rank_achievements");
            migrationBuilder.DropForeignKey(name: "FK_purchases_packages_package_id", table: "purchases");
            migrationBuilder.DropForeignKey(name: "FK_pool_distributions_pool_periods_period_id", table: "pool_distributions");
            migrationBuilder.DropForeignKey(name: "FK_pool_distributions_ranks_rank_id", table: "pool_distributions");

            migrationBuilder.DropIndex(name: "IX_users_rank_id", table: "users");
            migrationBuilder.DropIndex(name: "IX_rank_achievements_rank_id", table: "rank_achievements");
            migrationBuilder.DropIndex(name: "IX_purchases_package_id", table: "purchases");
            migrationBuilder.DropIndex(name: "IX_pool_distributions_period_id", table: "pool_distributions");
            migrationBuilder.DropIndex(name: "IX_pool_distributions_rank_id", table: "pool_distributions");

            migrationBuilder.DropColumn(name: "rank_id", table: "users");
            migrationBuilder.DropColumn(name: "rank_id", table: "rank_achievements");
            migrationBuilder.DropColumn(name: "package_id", table: "purchases");
            migrationBuilder.DropColumn(name: "period_id", table: "pool_distributions");
            migrationBuilder.DropColumn(name: "rank_id", table: "pool_distributions");

            migrationBuilder.DropPrimaryKey(name: "PK_ranks", table: "ranks");
            migrationBuilder.DropColumn(name: "id", table: "ranks");
            migrationBuilder.AddColumn<Guid>(name: "id", table: "ranks", type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()");
            migrationBuilder.AddPrimaryKey(name: "PK_ranks", table: "ranks", column: "id");

            migrationBuilder.DropPrimaryKey(name: "PK_packages", table: "packages");
            migrationBuilder.DropColumn(name: "id", table: "packages");
            migrationBuilder.AddColumn<Guid>(name: "id", table: "packages", type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()");
            migrationBuilder.AddPrimaryKey(name: "PK_packages", table: "packages", column: "id");

            migrationBuilder.DropPrimaryKey(name: "PK_bonus_rules", table: "bonus_rules");
            migrationBuilder.DropColumn(name: "id", table: "bonus_rules");
            migrationBuilder.AddColumn<Guid>(name: "id", table: "bonus_rules", type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()");
            migrationBuilder.AddPrimaryKey(name: "PK_bonus_rules", table: "bonus_rules", column: "id");

            migrationBuilder.DropPrimaryKey(name: "PK_pool_periods", table: "pool_periods");
            migrationBuilder.DropColumn(name: "id", table: "pool_periods");
            migrationBuilder.AddColumn<Guid>(name: "id", table: "pool_periods", type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()");
            migrationBuilder.AddPrimaryKey(name: "PK_pool_periods", table: "pool_periods", column: "id");

            migrationBuilder.AddColumn<Guid>(name: "rank_id", table: "users", type: "uuid", nullable: true);
            migrationBuilder.AddColumn<Guid>(name: "rank_id", table: "rank_achievements", type: "uuid", nullable: false);
            migrationBuilder.AddColumn<Guid>(name: "package_id", table: "purchases", type: "uuid", nullable: true);
            migrationBuilder.AddColumn<Guid>(name: "period_id", table: "pool_distributions", type: "uuid", nullable: false);
            migrationBuilder.AddColumn<Guid>(name: "rank_id", table: "pool_distributions", type: "uuid", nullable: false);

            migrationBuilder.CreateIndex(name: "IX_users_rank_id", table: "users", column: "rank_id");
            migrationBuilder.CreateIndex(name: "IX_rank_achievements_rank_id", table: "rank_achievements", column: "rank_id");
            migrationBuilder.CreateIndex(name: "IX_purchases_package_id", table: "purchases", column: "package_id");
            migrationBuilder.CreateIndex(name: "IX_pool_distributions_period_id", table: "pool_distributions", column: "period_id");
            migrationBuilder.CreateIndex(name: "IX_pool_distributions_rank_id", table: "pool_distributions", column: "rank_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_ranks_rank_id",
                table: "users",
                column: "rank_id",
                principalTable: "ranks",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
            migrationBuilder.AddForeignKey(
                name: "FK_rank_achievements_ranks_rank_id",
                table: "rank_achievements",
                column: "rank_id",
                principalTable: "ranks",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_purchases_packages_package_id",
                table: "purchases",
                column: "package_id",
                principalTable: "packages",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
            migrationBuilder.AddForeignKey(
                name: "FK_pool_distributions_pool_periods_period_id",
                table: "pool_distributions",
                column: "period_id",
                principalTable: "pool_periods",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_pool_distributions_ranks_rank_id",
                table: "pool_distributions",
                column: "rank_id",
                principalTable: "ranks",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "rank_id",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "ranks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "rank_id",
                table: "rank_achievements",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "package_id",
                table: "purchases",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "pool_periods",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "rank_id",
                table: "pool_distributions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "period_id",
                table: "pool_distributions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "packages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "bonus_rules",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}

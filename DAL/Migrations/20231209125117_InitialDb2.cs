using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkShifts",
                table: "WorkShifts");

            migrationBuilder.DropColumn(
                name: "WorkShiftID",
                table: "WorkShifts");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "WorkShifts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "WorkShifts");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "WorkShifts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "WorkShifts");

            migrationBuilder.AddColumn<int>(
                name: "WorkShiftID",
                table: "WorkShifts",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "WorkShifts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "WorkShifts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkShifts",
                table: "WorkShifts",
                column: "WorkShiftID");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContributionSystem.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contribution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CalculationMethod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contribution", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonthInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContributionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthInfo_Contribution_ContributionId",
                        column: x => x.ContributionId,
                        principalTable: "Contribution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthInfo_ContributionId",
                table: "MonthInfo",
                column: "ContributionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthInfo");

            migrationBuilder.DropTable(
                name: "Contribution");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.Migrations;

/// <inheritdoc />
public partial class RenameIncomeToMonthlyIncome2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
   name: "Income",
   table: "UserProfiles",
   newName: "MonthlyIncome");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
   name: "MonthlyIncome",
   table: "UserProfiles",
   newName: "Income");
    }
}

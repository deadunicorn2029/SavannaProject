using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savanna.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SequrityQuestion",
                table: "User",
                newName: "SecurityQuestion");

            migrationBuilder.RenameColumn(
                name: "AnswerToSeqcurityQuestion",
                table: "User",
                newName: "AnswerToSecurityQuestion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityQuestion",
                table: "User",
                newName: "SequrityQuestion");

            migrationBuilder.RenameColumn(
                name: "AnswerToSecurityQuestion",
                table: "User",
                newName: "AnswerToSeqcurityQuestion");
        }
    }
}

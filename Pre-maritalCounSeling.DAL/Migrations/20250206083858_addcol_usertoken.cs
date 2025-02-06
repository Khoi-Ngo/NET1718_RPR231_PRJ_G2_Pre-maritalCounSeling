using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pre_maritalCounSeling.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addcol_usertoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "question_option",
                newName: "Question_Option");

            migrationBuilder.RenameIndex(
                name: "IX_question_option_question_id",
                table: "Question_Option",
                newName: "IX_Question_Option_question_id");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddCheckConstraint(
                name: "Max_value_avg_rating",
                table: "Service",
                sql: "avg_rating <= 5");

            migrationBuilder.AddCheckConstraint(
                name: "Min_value_avg_rating",
                table: "Service",
                sql: "avg_rating >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "Min_value_commission_rate",
                table: "Service",
                sql: "commission_rate >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RescheduledCount_Max",
                table: "Schedule",
                sql: "rescheduled_count <= 2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Max_value_avg_rating",
                table: "Service");

            migrationBuilder.DropCheckConstraint(
                name: "Min_value_avg_rating",
                table: "Service");

            migrationBuilder.DropCheckConstraint(
                name: "Min_value_commission_rate",
                table: "Service");

            migrationBuilder.DropCheckConstraint(
                name: "CK_RescheduledCount_Max",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "User");

            migrationBuilder.RenameTable(
                name: "Question_Option",
                newName: "question_option");

            migrationBuilder.RenameIndex(
                name: "IX_Question_Option_question_id",
                table: "question_option",
                newName: "IX_question_option_question_id");
        }
    }
}

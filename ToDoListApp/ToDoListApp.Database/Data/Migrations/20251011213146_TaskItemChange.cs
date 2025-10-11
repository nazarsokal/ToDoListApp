using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListApp.Database.Data.Migrations
{
    public partial class TaskItemChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "TaskItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "TaskItems");
        }
    }
}

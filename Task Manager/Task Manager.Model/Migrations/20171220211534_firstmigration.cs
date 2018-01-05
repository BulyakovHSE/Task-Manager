using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TaskManager.Model.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AllPointsCount = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DonePoints = table.Column<int>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PointName = table.Column<string>(nullable: true),
                    TaskType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificacaoAPI.Context.Migrations
{
    public partial class Script0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReadDate",
                table: "notifications",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadDate",
                table: "notifications");
        }
    }
}

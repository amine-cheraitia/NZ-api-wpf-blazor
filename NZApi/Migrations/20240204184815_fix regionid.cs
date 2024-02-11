using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZApi.Migrations
{
    /// <inheritdoc />
    public partial class fixregionid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegtionId",
                table: "Walks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RegtionId",
                table: "Walks",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}

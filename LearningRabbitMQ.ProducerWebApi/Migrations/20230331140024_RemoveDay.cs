using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningRabbitMQ.ProducerWebApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Forecasts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Day",
                table: "Forecasts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

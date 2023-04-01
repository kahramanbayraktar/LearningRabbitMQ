using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningRabbitMQ.ProducerWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddForecastValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Day",
                table: "Forecasts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Temperature",
                table: "Forecasts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Time",
                table: "Forecasts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Forecasts");
        }
    }
}

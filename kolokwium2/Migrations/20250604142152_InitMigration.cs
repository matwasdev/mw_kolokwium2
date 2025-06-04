﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace kolokwium2.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    TemperatureCelsius = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "Washing_Machine",
                columns: table => new
                {
                    WashingMachineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxWeight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Washing_Machine", x => x.WashingMachineId);
                });

            migrationBuilder.CreateTable(
                name: "Available_Program",
                columns: table => new
                {
                    AvailableProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WashingMachineId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Available_Program", x => x.AvailableProgramId);
                    table.ForeignKey(
                        name: "FK_Available_Program_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Available_Program_Washing_Machine_WashingMachineId",
                        column: x => x.WashingMachineId,
                        principalTable: "Washing_Machine",
                        principalColumn: "WashingMachineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_History",
                columns: table => new
                {
                    AvailableProgramId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_History", x => new { x.AvailableProgramId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_Purchase_History_Available_Program_AvailableProgramId",
                        column: x => x.AvailableProgramId,
                        principalTable: "Available_Program",
                        principalColumn: "AvailableProgramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchase_History_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "1111" },
                    { 2, "John2", "Doe2", "2222" },
                    { 3, "John3", "Doe3", "3333" }
                });

            migrationBuilder.InsertData(
                table: "Program",
                columns: new[] { "ProgramId", "DurationMinutes", "Name", "TemperatureCelsius" },
                values: new object[,]
                {
                    { 1, 10, "Program1", 100 },
                    { 2, 20, "Program2", 200 },
                    { 3, 30, "Program3", 300 }
                });

            migrationBuilder.InsertData(
                table: "Washing_Machine",
                columns: new[] { "WashingMachineId", "MaxWeight", "SerialNumber" },
                values: new object[,]
                {
                    { 1, 10.5m, "111" },
                    { 2, 20.5m, "222" },
                    { 3, 30.5m, "333" }
                });

            migrationBuilder.InsertData(
                table: "Available_Program",
                columns: new[] { "AvailableProgramId", "Price", "ProgramId", "WashingMachineId" },
                values: new object[,]
                {
                    { 1, 100.5m, 1, 1 },
                    { 2, 100.25m, 2, 1 },
                    { 3, 300.88m, 2, 2 },
                    { 4, 400.2m, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Purchase_History",
                columns: new[] { "AvailableProgramId", "CustomerId", "PurchaseDate", "Rating" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 2, 2, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 3, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Available_Program_ProgramId",
                table: "Available_Program",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Available_Program_WashingMachineId",
                table: "Available_Program",
                column: "WashingMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_History_CustomerId",
                table: "Purchase_History",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchase_History");

            migrationBuilder.DropTable(
                name: "Available_Program");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "Washing_Machine");
        }
    }
}

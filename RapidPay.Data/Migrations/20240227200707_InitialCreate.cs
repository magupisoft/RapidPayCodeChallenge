using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RapidPay.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardHolderSince = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentFees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentFees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ExpirationMonth = table.Column<int>(type: "int", nullable: false),
                    ExpirationtYear = table.Column<int>(type: "int", nullable: false),
                    CVC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "CardHolderSince", "CardId", "Email", "FirstName", "LastName", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("d720d095-a5b9-448e-8604-e4a7205fbab1"), null, new DateTime(2024, 2, 27, 20, 7, 6, 698, DateTimeKind.Utc).AddTicks(7575), null, "demo@payrapid.io", "Demo", "Demo", "R@pidPay!", null },
                    { new Guid("e703be3d-2f45-4fdb-b50b-5e683b85fec2"), null, new DateTime(2024, 2, 27, 20, 7, 6, 698, DateTimeKind.Utc).AddTicks(7570), null, "admin@payrapid.io", "SysAdmin", "RapidPay", "Password1!", null }
                });

            migrationBuilder.InsertData(
                table: "PaymentFees",
                columns: new[] { "Id", "Fee" },
                values: new object[] { new Guid("15900a44-154c-49f4-aa1f-231412c5a032"), 0.758225147965469m });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "AccountId", "Balance", "CVC", "ExpirationMonth", "ExpirationtYear", "Number" },
                values: new object[] { new Guid("063a2192-9111-4021-bcee-ad064d5d99c7"), new Guid("d720d095-a5b9-448e-8604-e4a7205fbab1"), 10000m, "481", 12, 2026, "422856726227993" });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AccountId",
                table: "Cards",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CardId",
                table: "Transactions",
                column: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentFees");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}

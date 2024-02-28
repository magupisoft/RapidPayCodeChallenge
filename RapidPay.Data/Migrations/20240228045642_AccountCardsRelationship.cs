using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RapidPay.Data.Migrations
{
    /// <inheritdoc />
    public partial class AccountCardsRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Accounts_AccountId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_AccountId",
                table: "Cards");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("e703be3d-2f45-4fdb-b50b-5e683b85fec2"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: new Guid("063a2192-9111-4021-bcee-ad064d5d99c7"));

            migrationBuilder.DeleteData(
                table: "PaymentFees",
                keyColumn: "Id",
                keyValue: new Guid("15900a44-154c-49f4-aa1f-231412c5a032"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("d720d095-a5b9-448e-8604-e4a7205fbab1"));

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Accounts");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "CardHolderSince", "Email", "FirstName", "LastName", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("00dce1ae-f2ff-42bb-bec7-2ee0986ead52"), null, new DateTime(2024, 2, 28, 4, 56, 41, 706, DateTimeKind.Utc).AddTicks(8124), "admin@payrapid.io", "SysAdmin", "RapidPay", "Password1!", null },
                    { new Guid("fbaed2b1-59bb-4d9b-ab43-117a3431b6d1"), null, new DateTime(2024, 2, 28, 4, 56, 41, 706, DateTimeKind.Utc).AddTicks(8127), "demo@payrapid.io", "Demo", "Demo", "R@pidPay!", null }
                });

            migrationBuilder.InsertData(
                table: "PaymentFees",
                columns: new[] { "Id", "Fee" },
                values: new object[] { new Guid("7a2f9875-ac32-4de6-9b12-2c43838dff22"), 1.1170301749916m });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "AccountId", "Balance", "CVC", "ExpirationMonth", "ExpirationtYear", "Number" },
                values: new object[] { new Guid("e656663d-73b9-4631-93b9-df6d55855464"), new Guid("fbaed2b1-59bb-4d9b-ab43-117a3431b6d1"), 10000m, "481", 12, 2026, "422856726227993" });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AccountId",
                table: "Cards",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Accounts_AccountId",
                table: "Cards",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Accounts_AccountId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_AccountId",
                table: "Cards");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00dce1ae-f2ff-42bb-bec7-2ee0986ead52"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: new Guid("e656663d-73b9-4631-93b9-df6d55855464"));

            migrationBuilder.DeleteData(
                table: "PaymentFees",
                keyColumn: "Id",
                keyValue: new Guid("7a2f9875-ac32-4de6-9b12-2c43838dff22"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("fbaed2b1-59bb-4d9b-ab43-117a3431b6d1"));

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

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
                values: new object[] { new Guid("063a2192-9111-4021-bcee-ad064d5d99c7"), new Guid("d720d095-a5b9-448e-8604-e4a7205fbab1"), 10000m, "481", 12, 2026, "4228567262279934" });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AccountId",
                table: "Cards",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Accounts_AccountId",
                table: "Cards",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}

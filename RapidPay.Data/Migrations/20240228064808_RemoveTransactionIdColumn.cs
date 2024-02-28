using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RapidPay.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTransactionIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Transactions");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "CardHolderSince", "Email", "FirstName", "LastName", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("199c7969-4134-43c8-8b64-7c26081b3580"), null, new DateTime(2024, 2, 28, 6, 48, 7, 787, DateTimeKind.Utc).AddTicks(9591), "demo@payrapid.io", "Demo", "Demo", "R@pidPay!", null },
                    { new Guid("8f0b22cd-5613-401a-a089-ccedb8bcaaa6"), null, new DateTime(2024, 2, 28, 6, 48, 7, 787, DateTimeKind.Utc).AddTicks(9588), "admin@payrapid.io", "SysAdmin", "RapidPay", "Password1!", null }
                });

            migrationBuilder.InsertData(
                table: "PaymentFees",
                columns: new[] { "Id", "Fee" },
                values: new object[] { new Guid("6bc94915-e30b-4c7f-87d8-1eae54c45bc3"), 1.74993107363113m });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "AccountId", "Balance", "CVC", "ExpirationMonth", "ExpirationtYear", "Number" },
                values: new object[] { new Guid("d74bfc29-8800-4457-82a8-18b44699afa4"), new Guid("199c7969-4134-43c8-8b64-7c26081b3580"), 10000m, "481", 12, 2026, "422856726227993" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f0b22cd-5613-401a-a089-ccedb8bcaaa6"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: new Guid("d74bfc29-8800-4457-82a8-18b44699afa4"));

            migrationBuilder.DeleteData(
                table: "PaymentFees",
                keyColumn: "Id",
                keyValue: new Guid("6bc94915-e30b-4c7f-87d8-1eae54c45bc3"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("199c7969-4134-43c8-8b64-7c26081b3580"));

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
        }
    }
}

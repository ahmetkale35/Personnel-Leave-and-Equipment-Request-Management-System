using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelUygulaması.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRequests_EquipmentItemId",
                table: "EquipmentRequests",
                column: "EquipmentItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentRequests_EquipmentItems_EquipmentItemId",
                table: "EquipmentRequests",
                column: "EquipmentItemId",
                principalTable: "EquipmentItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentRequests_EquipmentItems_EquipmentItemId",
                table: "EquipmentRequests");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentRequests_EquipmentItemId",
                table: "EquipmentRequests");
        }
    }
}

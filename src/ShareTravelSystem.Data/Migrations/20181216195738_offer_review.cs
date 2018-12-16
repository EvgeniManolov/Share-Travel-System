using Microsoft.EntityFrameworkCore.Migrations;

namespace ShareTravelSystem.Data.Migrations
{
    public partial class offer_review : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OfferId",
                table: "Reviews",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Offers_OfferId",
                table: "Reviews",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Offers_OfferId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_OfferId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Reviews");
        }
    }
}

namespace ShareTravelSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class correction_Review : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);
        }
    }
}

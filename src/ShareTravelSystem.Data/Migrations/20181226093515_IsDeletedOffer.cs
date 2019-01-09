﻿namespace ShareTravelSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class IsDeletedOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Towns",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Towns");
        }
    }
}
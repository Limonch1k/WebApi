using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class adderroretable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Punkt_Name",
                table: "PunktAccessRight");

            migrationBuilder.AddColumn<int>(
                name: "Punkt_Id",
                table: "PunktAccessRight",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ErroreDataPresentation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Srok = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateWrite = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    param = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErroreDataPresentation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErroreDataPresentation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErroreDataPresentation_UserId",
                table: "ErroreDataPresentation",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErroreDataPresentation");

            migrationBuilder.DropColumn(
                name: "Punkt_Id",
                table: "PunktAccessRight");

            migrationBuilder.AddColumn<string>(
                name: "Punkt_Name",
                table: "PunktAccessRight",
                type: "text",
                nullable: true);
        }
    }
}

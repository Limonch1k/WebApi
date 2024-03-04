using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class RenameErroreTable_as_NoDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErroreDataPresentation");

            migrationBuilder.CreateTable(
                name: "NullDataTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Srok = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PunktId = table.Column<int>(type: "integer", nullable: true),
                    param = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DateWrite = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NullDataTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NullDataTables_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NullDataTables_UserId",
                table: "NullDataTables",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NullDataTables");

            migrationBuilder.CreateTable(
                name: "ErroreDataPresentation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DateWrite = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PunktId = table.Column<int>(type: "integer", nullable: true),
                    Srok = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    param = table.Column<string>(type: "text", nullable: true)
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
    }
}

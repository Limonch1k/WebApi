using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class renamesometableккку : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessRights_Users_UserId",
                table: "AccessRights");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamAccessRights_Users_UserId",
                table: "ParamAccessRights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamAccessRights",
                table: "ParamAccessRights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessRights",
                table: "AccessRights");

            migrationBuilder.RenameTable(
                name: "ParamAccessRights",
                newName: "ParamAccessRight");

            migrationBuilder.RenameTable(
                name: "AccessRights",
                newName: "PageAccessRight");

            migrationBuilder.RenameIndex(
                name: "IX_ParamAccessRights_UserId",
                table: "ParamAccessRight",
                newName: "IX_ParamAccessRight_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessRights_UserId",
                table: "PageAccessRight",
                newName: "IX_PageAccessRight_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamAccessRight",
                table: "ParamAccessRight",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageAccessRight",
                table: "PageAccessRight",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageAccessRight_Users_UserId",
                table: "PageAccessRight",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamAccessRight_Users_UserId",
                table: "ParamAccessRight",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageAccessRight_Users_UserId",
                table: "PageAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamAccessRight_Users_UserId",
                table: "ParamAccessRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamAccessRight",
                table: "ParamAccessRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageAccessRight",
                table: "PageAccessRight");

            migrationBuilder.RenameTable(
                name: "ParamAccessRight",
                newName: "ParamAccessRights");

            migrationBuilder.RenameTable(
                name: "PageAccessRight",
                newName: "AccessRights");

            migrationBuilder.RenameIndex(
                name: "IX_ParamAccessRight_UserId",
                table: "ParamAccessRights",
                newName: "IX_ParamAccessRights_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PageAccessRight_UserId",
                table: "AccessRights",
                newName: "IX_AccessRights_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamAccessRights",
                table: "ParamAccessRights",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessRights",
                table: "AccessRights",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRights_Users_UserId",
                table: "AccessRights",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamAccessRights_Users_UserId",
                table: "ParamAccessRights",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

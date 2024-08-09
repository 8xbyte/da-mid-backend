using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaMid.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClassModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "group_id",
                table: "classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_classes_group_id",
                table: "classes",
                column: "group_id");

            migrationBuilder.AddForeignKey(
                name: "FK_classes_groups_group_id",
                table: "classes",
                column: "group_id",
                principalTable: "groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classes_groups_group_id",
                table: "classes");

            migrationBuilder.DropIndex(
                name: "IX_classes_group_id",
                table: "classes");

            migrationBuilder.DropColumn(
                name: "group_id",
                table: "classes");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sozluk.Infrastructure.Persistence.Migrations
{
    public partial class Added_DeleteBehaveiour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_entrycommentfavorite_entrycomment_EntryCommentId",
                schema: "dbo",
                table: "entrycommentfavorite");

            migrationBuilder.DropForeignKey(
                name: "FK_entrycommentfavorite_user_CreatedById",
                schema: "dbo",
                table: "entrycommentfavorite");

            migrationBuilder.AddForeignKey(
                name: "FK_entrycommentfavorite_entrycomment_EntryCommentId",
                schema: "dbo",
                table: "entrycommentfavorite",
                column: "EntryCommentId",
                principalSchema: "dbo",
                principalTable: "entrycomment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_entrycommentfavorite_user_CreatedById",
                schema: "dbo",
                table: "entrycommentfavorite",
                column: "CreatedById",
                principalSchema: "dbo",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_entrycommentfavorite_entrycomment_EntryCommentId",
                schema: "dbo",
                table: "entrycommentfavorite");

            migrationBuilder.DropForeignKey(
                name: "FK_entrycommentfavorite_user_CreatedById",
                schema: "dbo",
                table: "entrycommentfavorite");

            migrationBuilder.AddForeignKey(
                name: "FK_entrycommentfavorite_entrycomment_EntryCommentId",
                schema: "dbo",
                table: "entrycommentfavorite",
                column: "EntryCommentId",
                principalSchema: "dbo",
                principalTable: "entrycomment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entrycommentfavorite_user_CreatedById",
                schema: "dbo",
                table: "entrycommentfavorite",
                column: "CreatedById",
                principalSchema: "dbo",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

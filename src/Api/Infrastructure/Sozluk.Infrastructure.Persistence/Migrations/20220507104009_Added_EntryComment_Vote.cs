using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sozluk.Infrastructure.Persistence.Migrations
{
    public partial class Added_EntryComment_Vote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntryCommentFavorite_entrycomment_EntryCommentId",
                table: "EntryCommentFavorite");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryCommentFavorite_user_CreatedUserId",
                table: "EntryCommentFavorite");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryCommentVotes_entrycomment_EntryCommentId",
                table: "EntryCommentVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntryCommentFavorite",
                table: "EntryCommentFavorite");

            migrationBuilder.DropIndex(
                name: "IX_EntryCommentFavorite_CreatedUserId",
                table: "EntryCommentFavorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntryCommentVotes",
                table: "EntryCommentVotes");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "EntryCommentFavorite");

            migrationBuilder.RenameTable(
                name: "EntryCommentFavorite",
                newName: "entrycommentfavorite",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "EntryCommentVotes",
                newName: "entrycommentvote",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_EntryCommentFavorite_EntryCommentId",
                schema: "dbo",
                table: "entrycommentfavorite",
                newName: "IX_entrycommentfavorite_EntryCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_EntryCommentVotes_EntryCommentId",
                schema: "dbo",
                table: "entrycommentvote",
                newName: "IX_entrycommentvote_EntryCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_entrycommentfavorite",
                schema: "dbo",
                table: "entrycommentfavorite",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_entrycommentvote",
                schema: "dbo",
                table: "entrycommentvote",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_entrycommentfavorite_CreatedById",
                schema: "dbo",
                table: "entrycommentfavorite",
                column: "CreatedById");

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

            migrationBuilder.AddForeignKey(
                name: "FK_entrycommentvote_entrycomment_EntryCommentId",
                schema: "dbo",
                table: "entrycommentvote",
                column: "EntryCommentId",
                principalSchema: "dbo",
                principalTable: "entrycomment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_entrycommentvote_entrycomment_EntryCommentId",
                schema: "dbo",
                table: "entrycommentvote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_entrycommentfavorite",
                schema: "dbo",
                table: "entrycommentfavorite");

            migrationBuilder.DropIndex(
                name: "IX_entrycommentfavorite_CreatedById",
                schema: "dbo",
                table: "entrycommentfavorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_entrycommentvote",
                schema: "dbo",
                table: "entrycommentvote");

            migrationBuilder.RenameTable(
                name: "entrycommentfavorite",
                schema: "dbo",
                newName: "EntryCommentFavorite");

            migrationBuilder.RenameTable(
                name: "entrycommentvote",
                schema: "dbo",
                newName: "EntryCommentVotes");

            migrationBuilder.RenameIndex(
                name: "IX_entrycommentfavorite_EntryCommentId",
                table: "EntryCommentFavorite",
                newName: "IX_EntryCommentFavorite_EntryCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_entrycommentvote_EntryCommentId",
                table: "EntryCommentVotes",
                newName: "IX_EntryCommentVotes_EntryCommentId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "EntryCommentFavorite",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntryCommentFavorite",
                table: "EntryCommentFavorite",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntryCommentVotes",
                table: "EntryCommentVotes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentFavorite_CreatedUserId",
                table: "EntryCommentFavorite",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntryCommentFavorite_entrycomment_EntryCommentId",
                table: "EntryCommentFavorite",
                column: "EntryCommentId",
                principalSchema: "dbo",
                principalTable: "entrycomment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntryCommentFavorite_user_CreatedUserId",
                table: "EntryCommentFavorite",
                column: "CreatedUserId",
                principalSchema: "dbo",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntryCommentVotes_entrycomment_EntryCommentId",
                table: "EntryCommentVotes",
                column: "EntryCommentId",
                principalSchema: "dbo",
                principalTable: "entrycomment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

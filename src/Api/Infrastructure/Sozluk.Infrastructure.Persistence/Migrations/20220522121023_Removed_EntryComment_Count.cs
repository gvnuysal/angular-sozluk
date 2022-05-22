using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sozluk.Infrastructure.Persistence.Migrations
{
    public partial class Removed_EntryComment_Count : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_entryvote_entry_EntryId1",
                schema: "dbo",
                table: "entryvote");

            migrationBuilder.DropColumn(
                name: "EntryCommentCount",
                schema: "dbo",
                table: "entrycommentfavorite");

            migrationBuilder.AlterColumn<Guid>(
                name: "EntryId1",
                schema: "dbo",
                table: "entryvote",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_entryvote_entry_EntryId1",
                schema: "dbo",
                table: "entryvote",
                column: "EntryId1",
                principalSchema: "dbo",
                principalTable: "entry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_entryvote_entry_EntryId1",
                schema: "dbo",
                table: "entryvote");

            migrationBuilder.AlterColumn<Guid>(
                name: "EntryId1",
                schema: "dbo",
                table: "entryvote",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntryCommentCount",
                schema: "dbo",
                table: "entrycommentfavorite",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_entryvote_entry_EntryId1",
                schema: "dbo",
                table: "entryvote",
                column: "EntryId1",
                principalSchema: "dbo",
                principalTable: "entry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

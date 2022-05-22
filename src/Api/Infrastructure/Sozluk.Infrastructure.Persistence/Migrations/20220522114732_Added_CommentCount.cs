using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sozluk.Infrastructure.Persistence.Migrations
{
    public partial class Added_CommentCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntryCommentCount",
                schema: "dbo",
                table: "entrycommentfavorite",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryCommentCount",
                schema: "dbo",
                table: "entrycommentfavorite");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreakOut_Movie.Migrations
{
    public partial class editFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_AspNetUsers_UserId",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Movies_Movie_Id",
                table: "Rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rates",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Rates");

            migrationBuilder.RenameColumn(
                name: "Movie_Id",
                table: "Rates",
                newName: "MovieId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Rates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rates",
                table: "Rates",
                columns: new[] { "MovieId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_AspNetUsers_UserId",
                table: "Rates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Movies_MovieId",
                table: "Rates",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_AspNetUsers_UserId",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Movies_MovieId",
                table: "Rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rates",
                table: "Rates");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Rates",
                newName: "Movie_Id");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Rates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Rates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rates",
                table: "Rates",
                columns: new[] { "Movie_Id", "User_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_AspNetUsers_UserId",
                table: "Rates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Movies_Movie_Id",
                table: "Rates",
                column: "Movie_Id",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

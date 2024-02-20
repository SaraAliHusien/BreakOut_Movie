using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreakOut_Movie.Migrations
{
    public partial class addRolesToAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles]([UserId] ,[RoleId]) SELECT '5d3a5c05-c8af-4fbf-87a7-12d64d5f9443' , Id FROM [dbo].[AspNetRoles] ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles] WHERE UserId ='5d3a5c05-c8af-4fbf-87a7-12d64d5f9443'");
        }
    }
}

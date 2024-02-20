using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreakOut_Movie.Migrations
{
    public partial class addrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6c107258-66ac-45bc-9e15-07c93961d1c7', N'Admin', N'ADMIN', N'07981354-92bb-4bb6-a24e-831db1381127')");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'f07f3bf1-5f18-48b6-a3fd-558aea48b2da', N'MainAdmin', N'MAINADMIN', N'7c734443-ad7f-455f-8f38-7686263bd9b6')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetRoles] WHERE Id='6c107258-66ac-45bc-9e15-07c93961d1c7'");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetRoles] WHERE Id='f07f3bf1-5f18-48b6-a3fd-558aea48b2da'");

        }
    }
}

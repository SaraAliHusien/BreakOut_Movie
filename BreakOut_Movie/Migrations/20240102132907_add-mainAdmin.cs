using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreakOut_Movie.Migrations
{
    public partial class addmainAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5d3a5c05-c8af-4fbf-87a7-12d64d5f9443', N'MainAdmin@BreakOut.Store', N'MAINADMIN@BREAKOUT.STORE', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEOc+bzdgayHAatDoJmTtkvAMHkA2eeiBB+B4l41k+UgfEyuFoxHpjeVgV9XdJJsaUA==', N'6YQZ4G55TY6DTXEJAHXU4D5SEQCNVAMW', N'e02ee042-19f6-4042-b1ca-838ba709db21', NULL, 0, 0, NULL, 1, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id='5d3a5c05-c8af-4fbf-87a7-12d64d5f9443' ");
        }
    }
}

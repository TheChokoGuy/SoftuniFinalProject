

#nullable disable

namespace Project.Data.Migrations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Project.Data.Models;
    using System.Text;
    public partial class AddAdminAccount : Migration
    {
        const string ADMIN_USER_GUID = "3d5d0982-31bc-4b38-963a-4c99e9fc9aca";
        const string ADMIN_ROLE_GUID = "f58b57e0-9a3e-43db-87bb-f434c1b46ddf";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            var hasher = new PasswordHasher<User>();

            var passwordHash = hasher.HashPassword(null, "hehcn9@8cnaXe");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp)");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{ADMIN_USER_GUID}'");
            sb.AppendLine(",'Admin'");
            sb.AppendLine(",'ADMIN'");
            sb.AppendLine(",'newprogrammer64@gmail.com'");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(",'NEWPROGRAMMER64@GMAIL.COM'");
            sb.AppendLine($", '{passwordHash}'");
            sb.AppendLine(", ''");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());


            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}','Admin','ADMIN')");

            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}','{ADMIN_ROLE_GUID}')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{ADMIN_USER_GUID}' AND RoleId = '{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{ADMIN_USER_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{ADMIN_ROLE_GUID}'");
        }
    }
}

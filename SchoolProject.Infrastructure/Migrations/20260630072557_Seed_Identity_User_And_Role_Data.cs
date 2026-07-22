using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Identity_User_And_Role_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "a97a4913-dfc9-41b6-beba-aa2ba780c52d", "Admin", "ADMIN" },
                    { 2, "0731cd45-e921-4851-842d-7eaaa0ba7b40", "Teacher", "TEACHER" },
                    { 3, "2fd85377-76f7-489d-a723-e7656161415f", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NameAr", "NameEn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "b1a89a8d-8b32-4946-bae5-334c2e11fdd2", "Egypt", "admin@yahoo.com", true, false, null, "المستخدم المسؤول", "Admin User", "ADMIN@YAHOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEPEeEauhgQ1f/Kj6xJnhtrTrcQL5kvtLLOnz+LZLW0EFn64MoT7kLPSVGvfofF1A0w==", "01001234567", null, true, "0c2f2bc3-6e4d-4e94-aecb-32bae418d7c2", false, "admin" },
                    { 2, 0, "a7c17b1a-33a4-48e5-9fd7-765421a99234", "Egypt", "teacher1@yahoo.com", true, false, null, "المعلم الأول", "Teacher One", "TEACHER1@YAHOO.COM", "TEACHER1", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01101234567", null, true, "b719ecff-c01e-4641-a622-4150c4a3c274", false, "teacher1" },
                    { 3, 0, "c9081a84-6af9-4469-ad9c-9ba72a9fca1d", "Egypt", "teacher2@yahoo.com", true, false, null, "المعلم الثاني", "Teacher Two", "TEACHER2@YAHOO.COM", "TEACHER2", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01201234567", null, true, "a053d27a-715b-4af4-a5bc-660278dfd9ff", false, "teacher2" },
                    { 4, 0, "d910958e-d9b5-4ab8-bd90-6ba0645dcc13", "Egypt", "student1@yahoo.com", true, false, null, "الطالب الأول", "Student One", "STUDENT1@YAHOO.COM", "STUDENT1", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01001234568", null, true, "50729c87-1113-4c48-bbe4-80f88425eb01", false, "student1" },
                    { 5, 0, "13c8aa62-fafb-4710-9341-7850c3f82868", "Egypt", "student2@yahoo.com", true, false, null, "الطالب الثاني", "Student Two", "STUDENT2@YAHOO.COM", "STUDENT2", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01101234568", null, true, "a3ae62a1-4c23-480c-8049-f90c2334fad8", false, "student2" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Period = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashedCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetCodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    TokenHash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentSubjects",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentSubjects", x => new { x.DepartmentId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_DepartmentSubjects_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Instructors_Instructors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorSubjects",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSubjects", x => new { x.InstructorId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_InstructorSubjects_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjects",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjects", x => new { x.StudentId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "a97a4913-dfc9-41b6-beba-aa2ba780c52d", "Admin", "ADMIN" },
                    { 2, "0731cd45-e921-4851-842d-7eaaa0ba7b40", "Instructor", "INSTRUCTOR" },
                    { 3, "2fd85377-76f7-489d-a723-e7656161415f", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImagePath", "LockoutEnabled", "LockoutEnd", "NameAr", "NameEn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { 1, 0, "Cairo, Egypt", "b1a89a8d-8b32-4946-bae5-334c2e11fdd2", "admin@yahoo.com", true, null, false, null, "المستخدم المسؤول", "Admin User", "ADMIN@YAHOO.COM", "ADMIN", "AQAAAAIAAYagAAAAEPEeEauhgQ1f/Kj6xJnhtrTrcQL5kvtLLOnz+LZLW0EFn64MoT7kLPSVGvfofF1A0w==", "01001234567", true, "0c2f2bc3-6e4d-4e94-aecb-32bae418d7c2", false, "admin", "Admin" },
                    { 2, 0, "Cairo, Egypt", "a7c17b1a-33a4-48e5-9fd7-765421a99234", "teacher1@yahoo.com", true, null, false, null, "د. علي منصور", "Dr. Ali Mansour", "TEACHER1@YAHOO.COM", "TEACHER1", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01101234567", true, "b719ecff-c01e-4641-a622-4150c4a3c274", false, "teacher1", "Instructor" },
                    { 3, 0, "Cairo, Egypt", "c9081a84-6af9-4469-ad9c-9ba72a9fca1d", "teacher2@yahoo.com", true, null, false, null, "د. سارة يوسف", "Dr. Sarah Youssef", "TEACHER2@YAHOO.COM", "TEACHER2", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01201234567", true, "a053d27a-715b-4af4-a5bc-660278dfd9ff", false, "teacher2", "Instructor" },
                    { 4, 0, "Cairo, Egypt", "d910958e-d9b5-4ab8-bd90-6ba0645dcc13", "student1@yahoo.com", true, null, false, null, "أحمد حسن", "Ahmed Hassan", "STUDENT1@YAHOO.COM", "STUDENT1", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01001234568", true, "50729c87-1113-4c48-bbe4-80f88425eb01", false, "student1", "Student" },
                    { 5, 0, "Giza, Egypt", "13c8aa62-fafb-4710-9341-7850c3f82868", "student2@yahoo.com", true, null, false, null, "فاطمة محمد", "Fatima Mohamed", "STUDENT2@YAHOO.COM", "STUDENT2", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01101234568", true, "a3ae62a1-4c23-480c-8049-f90c2334fad8", false, "student2", "Student" },
                    { 6, 0, "Alexandria, Egypt", "c1234567-1234-1234-1234-123456789012", "student3@yahoo.com", true, null, false, null, "عمر علي", "Omar Ali", "STUDENT3@YAHOO.COM", "STUDENT3", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01201234570", true, "c1234567-1234-1234-1234-123456789012", false, "student3", "Student" },
                    { 7, 0, "Cairo, Egypt", "c2234567-1234-1234-1234-123456789012", "student4@yahoo.com", true, null, false, null, "ليلى إبراهيم", "Layla Ibrahim", "STUDENT4@YAHOO.COM", "STUDENT4", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01001234571", true, "c2234567-1234-1234-1234-123456789012", false, "student4", "Student" },
                    { 8, 0, "Helwan, Egypt", "c3234567-1234-1234-1234-123456789012", "student5@yahoo.com", true, null, false, null, "مصطفى كريم", "Mustafa Karim", "STUDENT5@YAHOO.COM", "STUDENT5", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01101234571", true, "c3234567-1234-1234-1234-123456789012", false, "student5", "Student" },
                    { 9, 0, "New Cairo, Egypt", "c4234567-1234-1234-1234-123456789012", "student6@yahoo.com", true, null, false, null, "أميرة حسن", "Amira Hassan", "STUDENT6@YAHOO.COM", "STUDENT6", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01201234571", true, "c4234567-1234-1234-1234-123456789012", false, "student6", "Student" },
                    { 10, 0, "Cairo, Egypt", "c5234567-1234-1234-1234-123456789012", "student7@yahoo.com", true, null, false, null, "خالد أحمد", "Khaled Ahmed", "STUDENT7@YAHOO.COM", "STUDENT7", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01001234572", true, "c5234567-1234-1234-1234-123456789012", false, "student7", "Student" },
                    { 11, 0, "Giza, Egypt", "c6234567-1234-1234-1234-123456789012", "student8@yahoo.com", true, null, false, null, "نور صالح", "Noor Saleh", "STUDENT8@YAHOO.COM", "STUDENT8", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01101234572", true, "c6234567-1234-1234-1234-123456789012", false, "student8", "Student" },
                    { 12, 0, "Cairo, Egypt", "c7234567-1234-1234-1234-123456789012", "student9@yahoo.com", true, null, false, null, "يوسف نصر", "Youssef Nasr", "STUDENT9@YAHOO.COM", "STUDENT9", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01201234572", true, "c7234567-1234-1234-1234-123456789012", false, "student9", "Student" },
                    { 13, 0, "Helwan, Egypt", "c8234567-1234-1234-1234-123456789012", "student10@yahoo.com", true, null, false, null, "دينا خليل", "Dina Khalil", "STUDENT10@YAHOO.COM", "STUDENT10", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01001234573", true, "c8234567-1234-1234-1234-123456789012", false, "student10", "Student" },
                    { 14, 0, "Cairo, Egypt", "c9234567-1234-1234-1234-123456789012", "teacher3@yahoo.com", true, null, false, null, "د. كريم عادل", "Dr. Karim Adel", "TEACHER3@YAHOO.COM", "TEACHER3", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01101234573", true, "c9234567-1234-1234-1234-123456789012", false, "teacher3", "Instructor" },
                    { 15, 0, "Cairo, Egypt", "ca234567-1234-1234-1234-123456789012", "teacher4@yahoo.com", true, null, false, null, "د. منى سمير", "Dr. Mona Samir", "TEACHER4@YAHOO.COM", "TEACHER4", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01201234573", true, "ca234567-1234-1234-1234-123456789012", false, "teacher4", "Instructor" },
                    { 16, 0, "Cairo, Egypt", "cb234567-1234-1234-1234-123456789012", "teacher5@yahoo.com", true, null, false, null, "د. طارق نجيب", "Dr. Tarek Naguib", "TEACHER5@YAHOO.COM", "TEACHER5", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01001234574", true, "cb234567-1234-1234-1234-123456789012", false, "teacher5", "Instructor" },
                    { 17, 0, "Cairo, Egypt", "cc234567-1234-1234-1234-123456789012", "teacher6@yahoo.com", true, null, false, null, "د. نور فتحي", "Dr. Nour Fathy", "TEACHER6@YAHOO.COM", "TEACHER6", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01101234574", true, "cc234567-1234-1234-1234-123456789012", false, "teacher6", "Instructor" },
                    { 18, 0, "Cairo, Egypt", "cd234567-1234-1234-1234-123456789012", "teacher7@yahoo.com", true, null, false, null, "د. حنان عباس", "Dr. Hanan Abbas", "TEACHER7@YAHOO.COM", "TEACHER7", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01201234574", true, "cd234567-1234-1234-1234-123456789012", false, "teacher7", "Instructor" },
                    { 19, 0, "Cairo, Egypt", "ce234567-1234-1234-1234-123456789012", "teacher8@yahoo.com", true, null, false, null, "د. يارا حامد", "Dr. Yara Hamed", "TEACHER8@YAHOO.COM", "TEACHER8", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01001234575", true, "ce234567-1234-1234-1234-123456789012", false, "teacher8", "Instructor" },
                    { 20, 0, "Cairo, Egypt", "cf234567-1234-1234-1234-123456789012", "teacher9@yahoo.com", true, null, false, null, "د. رانيا فاضل", "Dr. Rania Fadel", "TEACHER9@YAHOO.COM", "TEACHER9", "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==", "01101234575", true, "cf234567-1234-1234-1234-123456789012", false, "teacher9", "Instructor" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "ManagerId", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, null, "علوم الحاسوب", "Computer Science" },
                    { 2, null, "الرياضيات", "Mathematics" },
                    { 3, null, "الفيزياء", "Physics" },
                    { 4, null, "الكيمياء", "Chemistry" },
                    { 5, null, "الأحياء", "Biology" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "NameAr", "NameEn", "Period" },
                values: new object[,]
                {
                    { 1, "هياكل البيانات", "Data Structures", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "الخوارزميات", "Algorithms", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "تطوير الويب", "Web Development", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "حسبان 1", "Calculus I", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "الجبر الخطي", "Linear Algebra", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "الميكانيكا", "Mechanics", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "الكهرومغناطيسية", "Electromagnetism", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "الكيمياء العضوية", "Organic Chemistry", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "علم الوراثة", "Genetics", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 3, 13 },
                    { 2, 14 },
                    { 2, 15 },
                    { 2, 16 },
                    { 2, 17 },
                    { 2, 18 },
                    { 2, 19 },
                    { 2, 20 }
                });

            migrationBuilder.InsertData(
                table: "DepartmentSubjects",
                columns: new[] { "DepartmentId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 4, 8 },
                    { 5, 9 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "DepartmentId", "SupervisorId" },
                values: new object[,]
                {
                    { 2, 1, null },
                    { 14, 2, null },
                    { 16, 3, null },
                    { 18, 4, null },
                    { 20, 5, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DepartmentId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 3 },
                    { 9, 3 },
                    { 10, 4 },
                    { 11, 4 },
                    { 12, 5 },
                    { 13, 5 }
                });

            migrationBuilder.InsertData(
                table: "InstructorSubjects",
                columns: new[] { "InstructorId", "SubjectId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 14, 4 },
                    { 14, 5 },
                    { 16, 6 },
                    { 16, 7 },
                    { 18, 8 },
                    { 20, 9 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "DepartmentId", "SupervisorId" },
                values: new object[,]
                {
                    { 3, 1, 2 },
                    { 15, 2, 14 },
                    { 17, 3, 16 },
                    { 19, 4, 18 }
                });

            migrationBuilder.InsertData(
                table: "StudentSubjects",
                columns: new[] { "StudentId", "SubjectId", "Grade" },
                values: new object[,]
                {
                    { 4, 1, 85.5m },
                    { 4, 2, 90.0m },
                    { 4, 3, 78.0m },
                    { 5, 1, 92.5m },
                    { 5, 3, 88.0m },
                    { 6, 4, 65.0m },
                    { 6, 5, 72.5m },
                    { 7, 4, 81.0m },
                    { 7, 5, 89.5m },
                    { 8, 6, 95.0m },
                    { 8, 7, 87.0m },
                    { 9, 6, 74.5m },
                    { 9, 7, 80.0m },
                    { 10, 8, 83.0m },
                    { 11, 8, 91.5m },
                    { 12, 9, 77.0m },
                    { 13, 9, 86.5m }
                });

            migrationBuilder.InsertData(
                table: "InstructorSubjects",
                columns: new[] { "InstructorId", "SubjectId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 3, 3 },
                    { 15, 4 },
                    { 17, 7 },
                    { 19, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubjects_SubjectId",
                table: "DepartmentSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DepartmentId",
                table: "Instructors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubjects_SubjectId",
                table: "InstructorSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetCodes_UserId_HashedCode",
                table: "PasswordResetCodes",
                columns: new[] { "UserId", "HashedCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetCodes_UserId_IsRevoked",
                table: "PasswordResetCodes",
                columns: new[] { "UserId", "IsRevoked" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_FamilyId",
                table: "RefreshTokens",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_TokenHash",
                table: "RefreshTokens",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_ManagerId",
                table: "Departments",
                column: "ManagerId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_AspNetUsers_Id",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_ManagerId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DepartmentSubjects");

            migrationBuilder.DropTable(
                name: "InstructorSubjects");

            migrationBuilder.DropTable(
                name: "PasswordResetCodes");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "StudentSubjects");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}

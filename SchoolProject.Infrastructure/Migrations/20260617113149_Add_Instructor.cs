using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Instructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subjects",
                newName: "NameEn");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Subjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Grade",
                table: "StudentSubjects",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
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

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                column: "ManagerId",
                value: null);

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "DepartmentId", "NameAr", "NameEn", "SupervisorId" },
                values: new object[,]
                {
                    { 1, 1, "د. علي منصور", "Dr. Ali Mansour", null },
                    { 3, 2, "د. كريم عادل", "Dr. Karim Adel", null },
                    { 5, 3, "د. طارق نجيب", "Dr. Tarek Naguib", null },
                    { 7, 4, "د. حنان عباس", "Dr. Hanan Abbas", null },
                    { 9, 5, "د. رانيا فاضل", "Dr. Rania Fadel", null }
                });

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 1, 1 },
                column: "Grade",
                value: 85.5m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 1, 2 },
                column: "Grade",
                value: 90.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 1, 3 },
                column: "Grade",
                value: 78.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 2, 1 },
                column: "Grade",
                value: 92.5m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 2, 3 },
                column: "Grade",
                value: 88.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 3, 4 },
                column: "Grade",
                value: 65.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 3, 5 },
                column: "Grade",
                value: 72.5m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 4, 4 },
                column: "Grade",
                value: 81.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 4, 5 },
                column: "Grade",
                value: 89.5m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 5, 6 },
                column: "Grade",
                value: 95.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 5, 7 },
                column: "Grade",
                value: 87.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 6, 6 },
                column: "Grade",
                value: 74.5m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 6, 7 },
                column: "Grade",
                value: 80.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 7, 8 },
                column: "Grade",
                value: 83.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 8, 8 },
                column: "Grade",
                value: 91.5m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 9, 9 },
                column: "Grade",
                value: 77.0m);

            migrationBuilder.UpdateData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 10, 9 },
                column: "Grade",
                value: 86.5m);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameAr",
                value: "هياكل البيانات");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameAr",
                value: "الخوارزميات");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameAr",
                value: "تطوير الويب");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4,
                column: "NameAr",
                value: "حسبان 1");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5,
                column: "NameAr",
                value: "الجبر الخطي");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6,
                column: "NameAr",
                value: "الميكانيكا");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7,
                column: "NameAr",
                value: "الكهرومغناطيسية");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8,
                column: "NameAr",
                value: "الكيمياء العضوية");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 9,
                column: "NameAr",
                value: "علم الوراثة");

            migrationBuilder.InsertData(
                table: "InstructorSubjects",
                columns: new[] { "InstructorId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 5, 6 },
                    { 5, 7 },
                    { 7, 8 },
                    { 9, 9 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "DepartmentId", "NameAr", "NameEn", "SupervisorId" },
                values: new object[,]
                {
                    { 2, 1, "د. سارة يوسف", "Dr. Sarah Youssef", 1 },
                    { 4, 2, "د. منى سمير", "Dr. Mona Samir", 3 },
                    { 6, 3, "د. نور فتحي", "Dr. Nour Fathy", 5 },
                    { 8, 4, "د. يارا حامد", "Dr. Yara Hamed", 7 }
                });

            migrationBuilder.InsertData(
                table: "InstructorSubjects",
                columns: new[] { "InstructorId", "SubjectId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 3 },
                    { 4, 4 },
                    { 6, 7 },
                    { 8, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

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
                name: "FK_Departments_Instructors_ManagerId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "InstructorSubjects");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Subjects",
                newName: "Name");
        }
    }
}

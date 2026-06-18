using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_M_TO_M_Composite_Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentId",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSubjects_DepartmentId",
                table: "DepartmentSubjects");

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 17);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DepartmentSubjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                columns: new[] { "StudentId", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects",
                columns: new[] { "DepartmentId", "SubjectId" });

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
                table: "StudentSubjects",
                columns: new[] { "StudentId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 4, 4 },
                    { 4, 5 },
                    { 5, 6 },
                    { 5, 7 },
                    { 6, 6 },
                    { 6, 7 },
                    { 7, 8 },
                    { 8, 8 },
                    { 9, 9 },
                    { 10, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects");

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DepartmentId", "SubjectId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DepartmentId", "SubjectId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DepartmentId", "SubjectId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DepartmentId", "SubjectId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DepartmentId", "SubjectId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DepartmentId", "SubjectId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DepartmentId", "SubjectId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DepartmentId", "SubjectId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumns: new[] { "DepartmentId", "SubjectId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 7, 8 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 9, 9 });

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumns: new[] { "StudentId", "SubjectId" },
                keyValues: new object[] { 10, 9 });

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DepartmentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects",
                column: "Id");

            migrationBuilder.InsertData(
                table: "DepartmentSubjects",
                columns: new[] { "Id", "DepartmentId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 2, 4 },
                    { 5, 2, 5 },
                    { 6, 3, 6 },
                    { 7, 3, 7 },
                    { 8, 4, 8 },
                    { 9, 5, 9 }
                });

            migrationBuilder.InsertData(
                table: "StudentSubjects",
                columns: new[] { "Id", "StudentId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 2, 1 },
                    { 5, 2, 3 },
                    { 6, 3, 4 },
                    { 7, 3, 5 },
                    { 8, 4, 4 },
                    { 9, 4, 5 },
                    { 10, 5, 6 },
                    { 11, 5, 7 },
                    { 12, 6, 6 },
                    { 13, 6, 7 },
                    { 14, 7, 8 },
                    { 15, 8, 8 },
                    { 16, 9, 9 },
                    { 17, 10, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentId",
                table: "StudentSubjects",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubjects_DepartmentId",
                table: "DepartmentSubjects",
                column: "DepartmentId");
        }
    }
}

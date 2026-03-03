using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeededRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Mathematics" },
                    { 3, "Physics" },
                    { 4, "Chemistry" },
                    { 5, "Biology" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name", "Period" },
                values: new object[,]
                {
                    { 1, "Data Structures", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Algorithms", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Web Development", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Calculus I", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Linear Algebra", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Mechanics", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Electromagnetism", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Organic Chemistry", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Genetics", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

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
                table: "Students",
                columns: new[] { "Id", "Address", "DepartmentId", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Cairo, Egypt", 1, "Ahmed Hassan", "01001234567" },
                    { 2, "Giza, Egypt", 1, "Fatima Mohamed", "01101234567" },
                    { 3, "Alexandria, Egypt", 2, "Omar Ali", "01201234567" },
                    { 4, "Cairo, Egypt", 2, "Layla Ibrahim", "01001234568" },
                    { 5, "Helwan, Egypt", 3, "Mustafa Karim", "01101234568" },
                    { 6, "New Cairo, Egypt", 3, "Amira Hassan", "01201234568" },
                    { 7, "Cairo, Egypt", 4, "Khaled Ahmed", "01001234569" },
                    { 8, "Giza, Egypt", 4, "Noor Saleh", "01101234569" },
                    { 9, "Cairo, Egypt", 5, "Youssef Nasr", "01201234569" },
                    { 10, "Helwan, Egypt", 5, "Dina Khalil", "01001234570" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DepartmentSubjects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "StudentSubjects",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

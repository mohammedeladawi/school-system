using Microsoft.AspNetCore.Identity;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Domain.Enums;

namespace SchoolProject.Infrastructure.Seeder;

public static class SeedData
{
    public static readonly Department[] Departments = new[]
    {
        new Department { Id = 1, NameEn = "Computer Science", NameAr = "علوم الحاسوب" },
        new Department { Id = 2, NameEn = "Mathematics", NameAr = "الرياضيات" },
        new Department { Id = 3, NameEn = "Physics", NameAr = "الفيزياء" },
        new Department { Id = 4, NameEn = "Chemistry", NameAr = "الكيمياء" },
        new Department { Id = 5, NameEn = "Biology", NameAr = "الأحياء" }
    };

    public static readonly Subject[] Subjects = new[]
    {
        new Subject { Id = 1, NameEn = "Data Structures", NameAr = "هياكل البيانات", Period = new DateTime(2026, 03, 03) },
        new Subject { Id = 2, NameEn = "Algorithms", NameAr = "الخوارزميات", Period = new DateTime(2026, 03, 03) },
        new Subject { Id = 3, NameEn = "Web Development", NameAr = "تطوير الويب", Period = new DateTime(2026, 03, 03) },
        new Subject { Id = 4, NameEn = "Calculus I", NameAr = "حسبان 1", Period = new DateTime(2026, 03, 03) },
        new Subject { Id = 5, NameEn = "Linear Algebra", NameAr = "الجبر الخطي", Period = new DateTime(2026, 03, 03) },
        new Subject { Id = 6, NameEn = "Mechanics", NameAr = "الميكانيكا", Period = new DateTime(2026, 03, 03) },
        new Subject { Id = 7, NameEn = "Electromagnetism", NameAr = "الكهرومغناطيسية", Period = new DateTime(2026, 03, 03) },
        new Subject { Id = 8, NameEn = "Organic Chemistry", NameAr = "الكيمياء العضوية", Period = new DateTime(2026, 03, 03) },
        new Subject { Id = 9, NameEn = "Genetics", NameAr = "علم الوراثة", Period = new DateTime(2026, 03, 03) }
    };

    public static readonly StudentSubject[] StudentSubjects = new[]
    {
        // Ahmed Hassan (Student 4) - Subjects 1, 2, 3
        new StudentSubject { StudentId = 4, SubjectId = 1, Grade = 85.5m },
        new StudentSubject { StudentId = 4, SubjectId = 2, Grade = 90.0m },
        new StudentSubject { StudentId = 4, SubjectId = 3, Grade = 78.0m },
        // Fatima Mohamed (Student 5) - Subjects 1, 3
        new StudentSubject { StudentId = 5, SubjectId = 1, Grade = 92.5m },
        new StudentSubject { StudentId = 5, SubjectId = 3, Grade = 88.0m },
        // Omar Ali (Student 6) - Subjects 4, 5
        new StudentSubject { StudentId = 6, SubjectId = 4, Grade = 65.0m },
        new StudentSubject { StudentId = 6, SubjectId = 5, Grade = 72.5m },
        // Layla Ibrahim (Student 7) - Subjects 4, 5
        new StudentSubject { StudentId = 7, SubjectId = 4, Grade = 81.0m },
        new StudentSubject { StudentId = 7, SubjectId = 5, Grade = 89.5m },
        // Mustafa Karim (Student 8) - Subjects 6, 7
        new StudentSubject { StudentId = 8, SubjectId = 6, Grade = 95.0m },
        new StudentSubject { StudentId = 8, SubjectId = 7, Grade = 87.0m },
        // Amira Hassan (Student 9) - Subjects 6, 7
        new StudentSubject { StudentId = 9, SubjectId = 6, Grade = 74.5m },
        new StudentSubject { StudentId = 9, SubjectId = 7, Grade = 80.0m },
        // Khaled Ahmed (Student 10) - Subject 8
        new StudentSubject { StudentId = 10, SubjectId = 8, Grade = 83.0m },
        // Noor Saleh (Student 11) - Subject 8
        new StudentSubject { StudentId = 11, SubjectId = 8, Grade = 91.5m },
        // Youssef Nasr (Student 12) - Subject 9
        new StudentSubject { StudentId = 12, SubjectId = 9, Grade = 77.0m },
        // Dina Khalil (Student 13) - Subject 9
        new StudentSubject { StudentId = 13, SubjectId = 9, Grade = 86.5m }
    };

    public static readonly DepartmentSubject[] DepartmentSubjects = new[]
    {
        // Computer Science department (1) - Subjects 1, 2, 3
        new DepartmentSubject { DepartmentId = 1, SubjectId = 1 },
        new DepartmentSubject { DepartmentId = 1, SubjectId = 2 },
        new DepartmentSubject { DepartmentId = 1, SubjectId = 3 },
        // Mathematics department (2) - Subjects 4, 5
        new DepartmentSubject { DepartmentId = 2, SubjectId = 4 },
        new DepartmentSubject { DepartmentId = 2, SubjectId = 5 },
        // Physics department (3) - Subjects 6, 7
        new DepartmentSubject { DepartmentId = 3, SubjectId = 6 },
        new DepartmentSubject { DepartmentId = 3, SubjectId = 7 },
        // Chemistry department (4) - Subject 8
        new DepartmentSubject { DepartmentId = 4, SubjectId = 8 },
        // Biology department (5) - Subject 9
        new DepartmentSubject { DepartmentId = 5, SubjectId = 9 }

    };

    public static readonly InstructorSubject[] InstructorSubjects = new[]
    {
        new InstructorSubject { InstructorId = 2, SubjectId = 1 },
        new InstructorSubject { InstructorId = 2, SubjectId = 2 },
        new InstructorSubject { InstructorId = 2, SubjectId = 3 },
        new InstructorSubject { InstructorId = 3, SubjectId = 1 },
        new InstructorSubject { InstructorId = 3, SubjectId = 3 },
        new InstructorSubject { InstructorId = 14, SubjectId = 4 },
        new InstructorSubject { InstructorId = 14, SubjectId = 5 },
        new InstructorSubject { InstructorId = 15, SubjectId = 4 },
        new InstructorSubject { InstructorId = 16, SubjectId = 6 },
        new InstructorSubject { InstructorId = 16, SubjectId = 7 },
        new InstructorSubject { InstructorId = 17, SubjectId = 7 },
        new InstructorSubject { InstructorId = 18, SubjectId = 8 },
        new InstructorSubject { InstructorId = 19, SubjectId = 8 },
        new InstructorSubject { InstructorId = 20, SubjectId = 9 }
    };

    public static readonly ApplicationUser[] ApplicationUsers = new[]
    {
      new ApplicationUser
      {
            Id = 1,
            UserName = "admin",
            NameEn = "Admin User",
            NameAr = "المستخدم المسؤول",
            NormalizedUserName = "ADMIN",
            Email = "admin@yahoo.com",
            NormalizedEmail = "ADMIN@YAHOO.COM",
            EmailConfirmed = true,
            // Raw Password: 1122Mm@
            PasswordHash = "AQAAAAIAAYagAAAAEPEeEauhgQ1f/Kj6xJnhtrTrcQL5kvtLLOnz+LZLW0EFn64MoT7kLPSVGvfofF1A0w==",
            PhoneNumber = "01001234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "0c2f2bc3-6e4d-4e94-aecb-32bae418d7c2",
            ConcurrencyStamp = "b1a89a8d-8b32-4946-bae5-334c2e11fdd2",
            Address = "Cairo, Egypt",
            UserType = UserType.Admin
      }
    };

    public static readonly Instructor[] Instructors = new[]
    {
      new Instructor
      {
            Id = 2,
            UserName = "teacher1",
            NameEn = "Dr. Ali Mansour",
            NameAr = "د. علي منصور",
            NormalizedUserName = "TEACHER1",
            Email = "teacher1@yahoo.com",
            NormalizedEmail = "TEACHER1@YAHOO.COM",
            EmailConfirmed = true,
            // Raw Password: Password@123
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01101234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "b719ecff-c01e-4641-a622-4150c4a3c274",
            ConcurrencyStamp = "a7c17b1a-33a4-48e5-9fd7-765421a99234",
            Address = "Cairo, Egypt",
            UserType = UserType.Instructor,
            DepartmentId = 1,
            SupervisorId = null
      },
      new Instructor
      {
            Id = 3,
            UserName = "teacher2",
            NameEn = "Dr. Sarah Youssef",
            NameAr = "د. سارة يوسف",
            NormalizedUserName = "TEACHER2",
            Email = "teacher2@yahoo.com",
            NormalizedEmail = "TEACHER2@YAHOO.COM",
            EmailConfirmed = true,
            // Raw Password: Password@123
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01201234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "a053d27a-715b-4af4-a5bc-660278dfd9ff",
            ConcurrencyStamp = "c9081a84-6af9-4469-ad9c-9ba72a9fca1d",
            Address = "Cairo, Egypt",
            UserType = UserType.Instructor,
            DepartmentId = 1,
            SupervisorId = 2
      },
      new Instructor
      {
            Id = 14,
            UserName = "teacher3",
            NameEn = "Dr. Karim Adel",
            NameAr = "د. كريم عادل",
            NormalizedUserName = "TEACHER3",
            Email = "teacher3@yahoo.com",
            NormalizedEmail = "TEACHER3@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01101234573",
            PhoneNumberConfirmed = true,
            SecurityStamp = "c9234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "c9234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Instructor,
            DepartmentId = 2,
            SupervisorId = null
      },
      new Instructor
      {
            Id = 15,
            UserName = "teacher4",
            NameEn = "Dr. Mona Samir",
            NameAr = "د. منى سمير",
            NormalizedUserName = "TEACHER4",
            Email = "teacher4@yahoo.com",
            NormalizedEmail = "TEACHER4@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01201234573",
            PhoneNumberConfirmed = true,
            SecurityStamp = "ca234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "ca234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Instructor,
            DepartmentId = 2,
            SupervisorId = 14
      },
      new Instructor
      {
            Id = 16,
            UserName = "teacher5",
            NameEn = "Dr. Tarek Naguib",
            NameAr = "د. طارق نجيب",
            NormalizedUserName = "TEACHER5",
            Email = "teacher5@yahoo.com",
            NormalizedEmail = "TEACHER5@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01001234574",
            PhoneNumberConfirmed = true,
            SecurityStamp = "cb234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "cb234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Instructor,
            DepartmentId = 3,
            SupervisorId = null
      },
      new Instructor
      {
            Id = 17,
            UserName = "teacher6",
            NameEn = "Dr. Nour Fathy",
            NameAr = "د. نور فتحي",
            NormalizedUserName = "TEACHER6",
            Email = "teacher6@yahoo.com",
            NormalizedEmail = "TEACHER6@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01101234574",
            PhoneNumberConfirmed = true,
            SecurityStamp = "cc234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "cc234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Instructor,
            DepartmentId = 3,
            SupervisorId = 16
      },
      new Instructor
      {
            Id = 18,
            UserName = "teacher7",
            NameEn = "Dr. Hanan Abbas",
            NameAr = "د. حنان عباس",
            NormalizedUserName = "TEACHER7",
            Email = "teacher7@yahoo.com",
            NormalizedEmail = "TEACHER7@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01201234574",
            PhoneNumberConfirmed = true,
            SecurityStamp = "cd234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "cd234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Instructor,
            DepartmentId = 4,
            SupervisorId = null
      },
      new Instructor
      {
            Id = 19,
            UserName = "teacher8",
            NameEn = "Dr. Yara Hamed",
            NameAr = "د. يارا حامد",
            NormalizedUserName = "TEACHER8",
            Email = "teacher8@yahoo.com",
            NormalizedEmail = "TEACHER8@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01001234575",
            PhoneNumberConfirmed = true,
            SecurityStamp = "ce234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "ce234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Instructor,
            DepartmentId = 4,
            SupervisorId = 18
      },
      new Instructor
      {
            Id = 20,
            UserName = "teacher9",
            NameEn = "Dr. Rania Fadel",
            NameAr = "د. رانيا فاضل",
            NormalizedUserName = "TEACHER9",
            Email = "teacher9@yahoo.com",
            NormalizedEmail = "TEACHER9@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01101234575",
            PhoneNumberConfirmed = true,
            SecurityStamp = "cf234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "cf234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Instructor,
            DepartmentId = 5,
            SupervisorId = null
      }
    };

    public static readonly Student[] Students = new[]
    {
      new Student
      {
            Id = 4,
            UserName = "student1",
            NameEn = "Ahmed Hassan",
            NameAr = "أحمد حسن",
            NormalizedUserName = "STUDENT1",
            Email = "student1@yahoo.com",
            NormalizedEmail = "STUDENT1@YAHOO.COM",
            EmailConfirmed = true,
            // Raw Password: Password@123
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01001234568",
            PhoneNumberConfirmed = true,
            SecurityStamp = "50729c87-1113-4c48-bbe4-80f88425eb01",
            ConcurrencyStamp = "d910958e-d9b5-4ab8-bd90-6ba0645dcc13",
            Address = "Cairo, Egypt",
            UserType = UserType.Student,
            DepartmentId = 1
      },
      new Student
      {
            Id = 5,
            UserName = "student2",
            NameEn = "Fatima Mohamed",
            NameAr = "فاطمة محمد",
            NormalizedUserName = "STUDENT2",
            Email = "student2@yahoo.com",
            NormalizedEmail = "STUDENT2@YAHOO.COM",
            EmailConfirmed = true,
            // Raw Password: Password@123
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01101234568",
            PhoneNumberConfirmed = true,
            SecurityStamp = "a3ae62a1-4c23-480c-8049-f90c2334fad8",
            ConcurrencyStamp = "13c8aa62-fafb-4710-9341-7850c3f82868",
            Address = "Giza, Egypt",
            UserType = UserType.Student,
            DepartmentId = 1
      },
      new Student
      {
            Id = 6,
            UserName = "student3",
            NameEn = "Omar Ali",
            NameAr = "عمر علي",
            NormalizedUserName = "STUDENT3",
            Email = "student3@yahoo.com",
            NormalizedEmail = "STUDENT3@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01201234570",
            PhoneNumberConfirmed = true,
            SecurityStamp = "c1234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "c1234567-1234-1234-1234-123456789012",
            Address = "Alexandria, Egypt",
            UserType = UserType.Student,
            DepartmentId = 2
      },
      new Student
      {
            Id = 7,
            UserName = "student4",
            NameEn = "Layla Ibrahim",
            NameAr = "ليلى إبراهيم",
            NormalizedUserName = "STUDENT4",
            Email = "student4@yahoo.com",
            NormalizedEmail = "STUDENT4@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01001234571",
            PhoneNumberConfirmed = true,
            SecurityStamp = "c2234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "c2234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Student,
            DepartmentId = 2
      },
      new Student
      {
            Id = 8,
            UserName = "student5",
            NameEn = "Mustafa Karim",
            NameAr = "مصطفى كريم",
            NormalizedUserName = "STUDENT5",
            Email = "student5@yahoo.com",
            NormalizedEmail = "STUDENT5@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01101234571",
            PhoneNumberConfirmed = true,
            SecurityStamp = "c3234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "c3234567-1234-1234-1234-123456789012",
            Address = "Helwan, Egypt",
            UserType = UserType.Student,
            DepartmentId = 3
      },
      new Student
      {
            Id = 9,
            UserName = "student6",
            NameEn = "Amira Hassan",
            NameAr = "أميرة حسن",
            NormalizedUserName = "STUDENT6",
            Email = "student6@yahoo.com",
            NormalizedEmail = "STUDENT6@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01201234571",
            PhoneNumberConfirmed = true,
            SecurityStamp = "c4234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "c4234567-1234-1234-1234-123456789012",
            Address = "New Cairo, Egypt",
            UserType = UserType.Student,
            DepartmentId = 3
      },
      new Student
      {
            Id = 10,
            UserName = "student7",
            NameEn = "Khaled Ahmed",
            NameAr = "خالد أحمد",
            NormalizedUserName = "STUDENT7",
            Email = "student7@yahoo.com",
            NormalizedEmail = "STUDENT7@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01001234572",
            PhoneNumberConfirmed = true,
            SecurityStamp = "c5234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "c5234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Student,
            DepartmentId = 4
      },
      new Student
      {
            Id = 11,
            UserName = "student8",
            NameEn = "Noor Saleh",
            NameAr = "نور صالح",
            NormalizedUserName = "STUDENT8",
            Email = "student8@yahoo.com",
            NormalizedEmail = "STUDENT8@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01101234572",
            PhoneNumberConfirmed = true,
            SecurityStamp = "c6234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "c6234567-1234-1234-1234-123456789012",
            Address = "Giza, Egypt",
            UserType = UserType.Student,
            DepartmentId = 4
      },
      new Student
      {
            Id = 12,
            UserName = "student9",
            NameEn = "Youssef Nasr",
            NameAr = "يوسف نصر",
            NormalizedUserName = "STUDENT9",
            Email = "student9@yahoo.com",
            NormalizedEmail = "STUDENT9@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01201234572",
            PhoneNumberConfirmed = true,
            SecurityStamp = "c7234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "c7234567-1234-1234-1234-123456789012",
            Address = "Cairo, Egypt",
            UserType = UserType.Student,
            DepartmentId = 5
      },
      new Student
      {
            Id = 13,
            UserName = "student10",
            NameEn = "Dina Khalil",
            NameAr = "دينا خليل",
            NormalizedUserName = "STUDENT10",
            Email = "student10@yahoo.com",
            NormalizedEmail = "STUDENT10@YAHOO.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGgmuCLZWf94oa24FH2/rouApN2oGDwRIygAbVxhDGez79ryX8CUrYiJQarwvIpPPw==",
            PhoneNumber = "01001234573",
            PhoneNumberConfirmed = true,
            SecurityStamp = "c8234567-1234-1234-1234-123456789012",
            ConcurrencyStamp = "c8234567-1234-1234-1234-123456789012",
            Address = "Helwan, Egypt",
            UserType = UserType.Student,
            DepartmentId = 5
      }
    };

    public static readonly IdentityUserRole<int>[] ApplicationUserRoles = new[]
    {
        new IdentityUserRole<int> { UserId = 1, RoleId = 1 }, // Admin
        new IdentityUserRole<int> { UserId = 2, RoleId = 2 }, // Instructor (Dr. Ali Mansour)
        new IdentityUserRole<int> { UserId = 3, RoleId = 2 }, // Instructor (Dr. Sarah Youssef)
        new IdentityUserRole<int> { UserId = 4, RoleId = 3 }, // Student (Ahmed Hassan)
        new IdentityUserRole<int> { UserId = 5, RoleId = 3 }, // Student (Fatima Mohamed)
        new IdentityUserRole<int> { UserId = 6, RoleId = 3 }, // Student (Omar Ali)
        new IdentityUserRole<int> { UserId = 7, RoleId = 3 }, // Student (Layla Ibrahim)
        new IdentityUserRole<int> { UserId = 8, RoleId = 3 }, // Student (Mustafa Karim)
        new IdentityUserRole<int> { UserId = 9, RoleId = 3 }, // Student (Amira Hassan)
        new IdentityUserRole<int> { UserId = 10, RoleId = 3 }, // Student (Khaled Ahmed)
        new IdentityUserRole<int> { UserId = 11, RoleId = 3 }, // Student (Noor Saleh)
        new IdentityUserRole<int> { UserId = 12, RoleId = 3 }, // Student (Youssef Nasr)
        new IdentityUserRole<int> { UserId = 13, RoleId = 3 }, // Student (Dina Khalil)
        new IdentityUserRole<int> { UserId = 14, RoleId = 2 }, // Instructor (Dr. Karim Adel)
        new IdentityUserRole<int> { UserId = 15, RoleId = 2 }, // Instructor (Dr. Mona Samir)
        new IdentityUserRole<int> { UserId = 16, RoleId = 2 }, // Instructor (Dr. Tarek Naguib)
        new IdentityUserRole<int> { UserId = 17, RoleId = 2 }, // Instructor (Dr. Nour Fathy)
        new IdentityUserRole<int> { UserId = 18, RoleId = 2 }, // Instructor (Dr. Hanan Abbas)
        new IdentityUserRole<int> { UserId = 19, RoleId = 2 }, // Instructor (Dr. Yara Hamed)
        new IdentityUserRole<int> { UserId = 20, RoleId = 2 }  // Instructor (Dr. Rania Fadel)
    };

    public static readonly ApplicationRole[] ApplicationRoles = new[]
    {
        new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "a97a4913-dfc9-41b6-beba-aa2ba780c52d" },
        new ApplicationRole { Id = 2, Name = "Instructor", NormalizedName = "INSTRUCTOR", ConcurrencyStamp = "0731cd45-e921-4851-842d-7eaaa0ba7b40" },
        new ApplicationRole { Id = 3, Name = "Student", NormalizedName = "STUDENT", ConcurrencyStamp = "2fd85377-76f7-489d-a723-e7656161415f" }
    };
}
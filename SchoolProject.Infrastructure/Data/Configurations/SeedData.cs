using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Configurations;

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

    public static readonly Student[] Students = new[]
    {
           new Student { Id = 1, NameEn = "Ahmed Hassan", NameAr = "أحمد حسن", Address = "Cairo, Egypt", Phone = "01001234567", DepartmentId = 1 },
            new Student { Id = 2, NameEn = "Fatima Mohamed", NameAr = "فاطمة محمد", Address = "Giza, Egypt", Phone = "01101234567", DepartmentId = 1 },
            new Student { Id = 3, NameEn = "Omar Ali", NameAr = "عمر علي", Address = "Alexandria, Egypt", Phone = "01201234567", DepartmentId = 2 },
            new Student { Id = 4, NameEn = "Layla Ibrahim", NameAr = "ليلى إبراهيم", Address = "Cairo, Egypt", Phone = "01001234568", DepartmentId = 2 },
            new Student { Id = 5, NameEn = "Mustafa Karim", NameAr = "مصطفى كريم", Address = "Helwan, Egypt", Phone = "01101234568", DepartmentId = 3 },
            new Student { Id = 6, NameEn = "Amira Hassan", NameAr = "أميرة حسن", Address = "New Cairo, Egypt", Phone = "01201234568", DepartmentId = 3 },
            new Student { Id = 7, NameEn = "Khaled Ahmed", NameAr = "خالد أحمد", Address = "Cairo, Egypt", Phone = "01001234569", DepartmentId = 4 },
            new Student { Id = 8, NameEn = "Noor Saleh", NameAr = "نور صالح", Address = "Giza, Egypt", Phone = "01101234569", DepartmentId = 4 },
            new Student { Id = 9, NameEn = "Youssef Nasr", NameAr = "يوسف نصر", Address = "Cairo, Egypt", Phone = "01201234569", DepartmentId = 5 },
            new Student { Id = 10, NameEn = "Dina Khalil", NameAr = "دينا خليل", Address = "Helwan, Egypt", Phone = "01001234570", DepartmentId = 5 }
    };

    public static readonly Instructor[] Instructors = new[]
    {
        new Instructor { Id = 1, NameEn = "Dr. Ali Mansour", NameAr = "د. علي منصور", DepartmentId = 1 },
        new Instructor { Id = 2, NameEn = "Dr. Sarah Youssef", NameAr = "د. سارة يوسف", DepartmentId = 1, SupervisorId = 1 },
        new Instructor { Id = 3, NameEn = "Dr. Karim Adel", NameAr = "د. كريم عادل", DepartmentId = 2 },
        new Instructor { Id = 4, NameEn = "Dr. Mona Samir", NameAr = "د. منى سمير", DepartmentId = 2, SupervisorId = 3 },
        new Instructor { Id = 5, NameEn = "Dr. Tarek Naguib", NameAr = "د. طارق نجيب", DepartmentId = 3 },
        new Instructor { Id = 6, NameEn = "Dr. Nour Fathy", NameAr = "د. نور فتحي", DepartmentId = 3, SupervisorId = 5 },
        new Instructor { Id = 7, NameEn = "Dr. Hanan Abbas", NameAr = "د. حنان عباس", DepartmentId = 4 },
        new Instructor { Id = 8, NameEn = "Dr. Yara Hamed", NameAr = "د. يارا حامد", DepartmentId = 4, SupervisorId = 7 },
        new Instructor { Id = 9, NameEn = "Dr. Rania Fadel", NameAr = "د. رانيا فاضل", DepartmentId = 5 }
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
        // Ahmed Hassan (Student 1) - Subjects 1, 2, 3
        new StudentSubject { StudentId = 1, SubjectId = 1, Grade = 85.5m },
        new StudentSubject { StudentId = 1, SubjectId = 2, Grade = 90.0m },
        new StudentSubject { StudentId = 1, SubjectId = 3, Grade = 78.0m },
        // Fatima Mohamed (Student 2) - Subjects 1, 3
        new StudentSubject { StudentId = 2, SubjectId = 1, Grade = 92.5m },
        new StudentSubject { StudentId = 2, SubjectId = 3, Grade = 88.0m },
        // Omar Ali (Student 3) - Subjects 4, 5
        new StudentSubject { StudentId = 3, SubjectId = 4, Grade = 65.0m },
        new StudentSubject { StudentId = 3, SubjectId = 5, Grade = 72.5m },
        // Layla Ibrahim (Student 4) - Subjects 4, 5
        new StudentSubject { StudentId = 4, SubjectId = 4, Grade = 81.0m },
        new StudentSubject { StudentId = 4, SubjectId = 5, Grade = 89.5m },
        // Mustafa Karim (Student 5) - Subjects 6, 7
        new StudentSubject { StudentId = 5, SubjectId = 6, Grade = 95.0m },
        new StudentSubject { StudentId = 5, SubjectId = 7, Grade = 87.0m },
        // Amira Hassan (Student 6) - Subjects 6, 7
        new StudentSubject { StudentId = 6, SubjectId = 6, Grade = 74.5m },
        new StudentSubject { StudentId = 6, SubjectId = 7, Grade = 80.0m },
        // Khaled Ahmed (Student 7) - Subject 8
        new StudentSubject { StudentId = 7, SubjectId = 8, Grade = 83.0m },
        // Noor Saleh (Student 8) - Subject 8
        new StudentSubject { StudentId = 8, SubjectId = 8, Grade = 91.5m },
        // Youssef Nasr (Student 9) - Subject 9
        new StudentSubject { StudentId = 9, SubjectId = 9, Grade = 77.0m },
        // Dina Khalil (Student 10) - Subject 9
        new StudentSubject { StudentId = 10, SubjectId = 9, Grade = 86.5m }
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
        new InstructorSubject { InstructorId = 1, SubjectId = 1 },
        new InstructorSubject { InstructorId = 1, SubjectId = 2 },
        new InstructorSubject { InstructorId = 1, SubjectId = 3 },
        new InstructorSubject { InstructorId = 2, SubjectId = 1 },
        new InstructorSubject { InstructorId = 2, SubjectId = 3 },
        new InstructorSubject { InstructorId = 3, SubjectId = 4 },
        new InstructorSubject { InstructorId = 3, SubjectId = 5 },
        new InstructorSubject { InstructorId = 4, SubjectId = 4 },
        new InstructorSubject { InstructorId = 5, SubjectId = 6 },
        new InstructorSubject { InstructorId = 5, SubjectId = 7 },
        new InstructorSubject { InstructorId = 6, SubjectId = 7 },
        new InstructorSubject { InstructorId = 7, SubjectId = 8 },
        new InstructorSubject { InstructorId = 8, SubjectId = 8 },
        new InstructorSubject { InstructorId = 9, SubjectId = 9 }
    };
}
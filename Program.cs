using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KCAStudentResultSystem
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static List<Result> results = new List<Result>();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n1. Register Student");
                Console.WriteLine("2. Enter Result");
                Console.WriteLine("3. View Result");
                Console.WriteLine("4. Generate Report");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterStudent();
                        break;
                    case "2":
                        EnterResult();
                        break;
                    case "3":
                        ViewResult();
                        break;
                    case "4":
                        GenerateReport();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void RegisterStudent()
        {
            Console.Write("Enter Student ID: ");
            string studentId = Console.ReadLine();
            if (students.Any(s => s.StudentId == studentId))
            {
                Console.WriteLine("Error: Student ID already exists.");
                return;
            }

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Student Class: ");
            string studentClass = Console.ReadLine();

            students.Add(new Student { StudentId = studentId, Name = name, StudentClass = studentClass });
            Console.WriteLine("Student registered successfully.");
        }

        static void EnterResult()
        {
            Console.Write("Enter Student ID: ");
            string studentId = Console.ReadLine();
            var student = students.Find(s => s.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase));

            if (student != null)
            {
                Console.Write("Enter Subject Code: ");
                string subjectCode = Console.ReadLine();
                Console.Write("Enter Subject Description: ");
                string subjectDescription = Console.ReadLine();

                Console.Write("Enter Credit Hours: ");
                if (!int.TryParse(Console.ReadLine(), out int creditHours))
                {
                    Console.WriteLine("Invalid input. Credit Hours must be a number.");
                    return;
                }

                Console.Write("Enter Marks: ");
                if (!int.TryParse(Console.ReadLine(), out int marks))
                {
                    Console.WriteLine("Invalid input. Marks must be a number.");
                    return;
                }

                string grade = GetGrade(marks);
                double points = GetPoints(marks);

                results.Add(new Result
                {
                    Student = student,
                    SubjectCode = subjectCode,
                    SubjectDescription = subjectDescription,
                    CreditHours = creditHours,
                    Marks = marks,
                    Grade = grade,
                    Points = points
                });

                Console.WriteLine("Result entered successfully.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        static void ViewResult()
        {
            Console.Write("Enter Student ID: ");
            string studentId = Console.ReadLine();
            var student = students.Find(s => s.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase));

            if (student != null)
            {
                var studentResults = results.Where(r => r.Student.StudentId == student.StudentId).ToList();
                double gpa = CalculateGPA(studentResults);
                double totalPoints = studentResults.Sum(r => r.Points);
                int totalCreditHours = studentResults.Sum(r => r.CreditHours);

                Console.WriteLine("\n=====================================");
                Console.WriteLine("           EXAMINATION RESULTS SLIP");
                Console.WriteLine("=====================================");
                Console.WriteLine($"Student No: {student.StudentId}");
                Console.WriteLine($"Name: {student.Name}");
                Console.WriteLine($"Faculty: SCHOOL OF TECHNOLOGY");
                Console.WriteLine($"Programme: BACHELOR OF SCIENCE IN SOFTWARE DEVELOPMENT");
                Console.WriteLine($"Semester: TRIM1 24\n");

                Console.WriteLine("UNIT CODE\tDESCRIPTION\tCREDIT HOURS\tGRADE\tPOINTS");
                Console.WriteLine("----------------------------------------------------------------");

                foreach (var result in studentResults)
                {
                    Console.WriteLine($"{result.SubjectCode}\t{result.SubjectDescription}\t{result.CreditHours}\t{result.Grade}\t{result.Points}");
                }

                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine($"TRIMESTER GPA: {gpa:F2} \t TOTAL CREDIT HOURS: {totalCreditHours} \t TOTAL POINTS: {totalPoints}");
                Console.WriteLine("\nSigned: ........................................................");
                Console.WriteLine("Dean  SCHOOL OF TECHNOLOGY");
                Console.WriteLine("Date: ...........................................................");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
        static void GenerateReport()
        {
            string filePath = "ResultSlip.pdf";

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document(PageSize.A4);
                PdfWriter.GetInstance(document, fs);
                document.Open();

                foreach (var student in students)
                {
                    var studentResults = results.Where(r => r.Student.StudentId == student.StudentId).ToList();
                    double gpa = CalculateGPA(studentResults);
                    double totalPoints = studentResults.Sum(r => r.Points);
                    int totalCreditHours = studentResults.Sum(r => r.CreditHours);

                    document.Add(new Paragraph("EXAMINATION RESULTS SLIP", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16))
                    {
                        Alignment = Element.ALIGN_CENTER
                    });

                    document.Add(new Paragraph($"\nStudent No: {student.StudentId}", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    document.Add(new Paragraph($"Name: {student.Name}"));
                    document.Add(new Paragraph("Faculty: SCHOOL OF TECHNOLOGY"));
                    document.Add(new Paragraph("Programme: BACHELOR OF SCIENCE IN SOFTWARE DEVELOPMENT"));
                    document.Add(new Paragraph($"Semester: TRIM1 24\n"));

                    PdfPTable table = new PdfPTable(5) { WidthPercentage = 100, SpacingBefore = 10f, SpacingAfter = 10f };
                    string[] headers = { "UNIT CODE", "DESCRIPTION", "CREDIT HOURS", "GRADE", "POINTS" };

                    foreach (string header in headers)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(header, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Border = Rectangle.BOX,
                            Padding = 5f
                        };
                        table.AddCell(cell);
                    }

                    foreach (var result in studentResults)
                    {
                        table.AddCell(new PdfPCell(new Phrase(result.SubjectCode)));
                        table.AddCell(new PdfPCell(new Phrase(result.SubjectDescription)));
                        table.AddCell(new PdfPCell(new Phrase(result.CreditHours.ToString())));
                        table.AddCell(new PdfPCell(new Phrase(result.Grade)));
                        table.AddCell(new PdfPCell(new Phrase(result.Points.ToString())));
                    }

                    document.Add(table);
                    document.Add(new Paragraph($"\nTRIMESTER GPA: {gpa:F2}   TOTAL CREDIT HOURS: {totalCreditHours}   TOTAL POINTS: {totalPoints}",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));

                    document.Add(new Paragraph("\nSigned: ........................................................"));
                    document.Add(new Paragraph("Dean  SCHOOL OF TECHNOLOGY"));
                    document.Add(new Paragraph("Date: ..........................................................."));
                }

                document.Close();
            }

            Console.WriteLine($"PDF result slip generated successfully: {filePath}");
        }
        static double CalculateGPA(List<Result> studentResults)
        {
            double totalPoints = studentResults.Sum(r => r.Points * r.CreditHours);
            double totalCreditHours = studentResults.Sum(r => r.CreditHours);
            return totalCreditHours == 0 ? 0.0 : totalPoints / totalCreditHours;
        }

        static string GetGrade(int marks)
        {
            if (marks >= 74) return "A";
            else if (marks >= 70) return "A-";
            else if (marks >= 67) return "B+";
            else if (marks >= 64) return "B";
            else if (marks >= 60) return "B-";
            else if (marks >= 57) return "C+";
            else if (marks >= 54) return "C";
            else if (marks >= 50) return "C-";
            else if (marks >= 47) return "D+";
            else if (marks >= 44) return "D";
            else if (marks >= 40) return "D-";
            else return "F";
        }

        static double GetPoints(int marks)
        {
            if (marks >= 74) return 12.0;
            else if (marks >= 70) return 11.0;
            else if (marks >= 67) return 10.0;
            else if (marks >= 64) return 9.0;
            else if (marks >= 60) return 8.1;
            else if (marks >= 57) return 6.9;
            else if (marks >= 54) return 6.0;
            else if (marks >= 50) return 5.3;
            else if (marks >= 47) return 4.9;
            else if (marks >= 44) return 4.6;
            else if (marks >= 40) return 4.3;
            else return 0.0;
        }
    }

    class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string StudentClass { get; set; }
    }

    class Result
    {
        public Student Student { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectDescription { get; set; }
        public int CreditHours { get; set; }
        public int Marks { get; set; }
        public string Grade { get; set; }
        public double Points { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace SchoolGradingSystem
{
    // --- Student Class ---
    public class Student
    {
        public int Id { get; }
        public string FullName { get; }
        public int Score { get; }

        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName;
            Score = score;
        }

        public string GetGrade()
        {
            if (Score >= 80) return "A";
            if (Score >= 70) return "B";
            if (Score >= 60) return "C";
            if (Score >= 50) return "D";
            return "F";
        }
    }

    // --- Custom Exceptions ---
    public class InvalidScoreFormatException : Exception
    {
        public InvalidScoreFormatException(string message) : base(message) { }
    }

    public class MissingFieldException : Exception
    {
        public MissingFieldException(string message) : base(message) { }
    }

    // --- StudentResultProcessor Class ---
    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length != 3)
                        throw new MissingFieldException($"Missing fields in line: {line}");

                    try
                    {
                        int id = int.Parse(parts[0].Trim());
                        string fullName = parts[1].Trim();
                        int score = int.Parse(parts[2].Trim());

                        students.Add(new Student(id, fullName, score));
                    }
                    catch (FormatException)
                    {
                        throw new InvalidScoreFormatException($"Invalid score format in line: {line}");
                    }
                }
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (Student student in students)
                {
                    writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
                }
            }
        }
    }

    // --- Main Application ---
    class Program
    {
        static void Main()
        {
            string inputPath = "students.txt";
            string outputPath = "summary_report.txt";
            var processor = new StudentResultProcessor();

            try
            {
                // Auto-create input file if missing, using new Ghanaian names
                if (!File.Exists(inputPath))
                {
                    Console.WriteLine("Input file not found. Creating sample students.txt...");

                    using (StreamWriter writer = new StreamWriter(inputPath))
                    {
                        writer.WriteLine("301,Mawuena Adjei,87");
                        writer.WriteLine("302,Fiifi Nkrumah,79");
                        writer.WriteLine("303,Abenaa Sika,65");
                        writer.WriteLine("304,Selorm Quaye,53");
                        writer.WriteLine("305,Yaw Manu,41");
                    }

                    Console.WriteLine("Sample students.txt created successfully.");
                }

                List<Student> students = processor.ReadStudentsFromFile(inputPath);
                processor.WriteReportToFile(students, outputPath);

                Console.WriteLine("Report generated successfully.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File error: {ex.Message}");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($"Score format error: {ex.Message}");
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine($"Data error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
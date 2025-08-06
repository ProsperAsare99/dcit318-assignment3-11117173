using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareSystem
{
    // --- Generic Repository ---
    public class Repository<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> GetAll()
        {
            return items;
        }

        public T? GetById(Func<T, bool> predicate)
        {
            return items.FirstOrDefault(predicate);
        }

        public bool Remove(Func<T, bool> predicate)
        {
            var item = items.FirstOrDefault(predicate);
            if (item != null)
            {
                items.Remove(item);
                return true;
            }
            return false;
        }
    }

    // --- Domain Models ---
    public class Patient
    {
        public int Id { get; }
        public string Name { get; }
        public int Age { get; }
        public string Gender { get; }

        public Patient(int id, string name, int age, string gender)
        {
            Id = id;
            Name = name;
            Age = age;
            Gender = gender;
        }
    }

    public class Prescription
    {
        public int Id { get; }
        public int PatientId { get; }
        public string MedicationName { get; }
        public DateTime DateIssued { get; }

        public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
        {
            Id = id;
            PatientId = patientId;
            MedicationName = medicationName;
            DateIssued = dateIssued;
        }
    }

    // --- Health System Application ---
    public class HealthSystemApp
    {
        private Repository<Patient> _patientRepo = new Repository<Patient>();
        private Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
        private Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();

        public void SeedData()
        {
            _patientRepo.Add(new Patient(1, "Ama Boateng", 28, "Female"));
            _patientRepo.Add(new Patient(2, "Kwame Mensah", 45, "Male"));
            _patientRepo.Add(new Patient(3, "Efua Adjei", 34, "Female"));

            _prescriptionRepo.Add(new Prescription(101, 1, "Paracetamol", DateTime.Now.AddDays(-3)));
            _prescriptionRepo.Add(new Prescription(102, 1, "Amoxicillin", DateTime.Now.AddDays(-2)));
            _prescriptionRepo.Add(new Prescription(103, 2, "Ibuprofen", DateTime.Now.AddDays(-5)));
            _prescriptionRepo.Add(new Prescription(104, 3, "Cough Syrup", DateTime.Now.AddDays(-1)));
            _prescriptionRepo.Add(new Prescription(105, 2, "Vitamin C", DateTime.Now));
        }

        public void BuildPrescriptionMap()
        {
            var prescriptions = _prescriptionRepo.GetAll();
            _prescriptionMap = prescriptions
                .GroupBy(p => p.PatientId)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public void PrintAllPatients()
        {
            var patients = _patientRepo.GetAll();
            Console.WriteLine("Patient Records:");
            foreach (var p in patients)
            {
                Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Age: {p.Age}, Gender: {p.Gender}");
            }
        }

        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            return _prescriptionMap.ContainsKey(patientId)
                ? _prescriptionMap[patientId]
                : new List<Prescription>();
        }

        public void PrintPrescriptionsForPatient(int id)
        {
            var prescriptions = GetPrescriptionsByPatientId(id);
            Console.WriteLine($"\nPrescriptions for Patient ID {id}:");
            if (prescriptions.Count == 0)
            {
                Console.WriteLine("No prescriptions found.");
            }
            else
            {
                foreach (var p in prescriptions)
                {
                    Console.WriteLine($"ID: {p.Id}, Medication: {p.MedicationName}, Date Issued: {p.DateIssued:d}");
                }
            }
        }
    }

    // --- Main Entry ---
    class Program
    {
        static void Main()
        {
            var app = new HealthSystemApp();
            app.SeedData();
            app.BuildPrescriptionMap();
            app.PrintAllPatients();

            Console.WriteLine("\nSelect a Patient ID to view prescriptions:");
            Console.Write("Enter Patient ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                app.PrintPrescriptionsForPatient(id);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }

            Console.WriteLine("\nHealth system execution completed.");
        }
    }
}
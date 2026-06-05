using System;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Appointment> appointments = new List<Appointment>()
            {
                new Appointment { AppointmentId = 1, PatientName = "Ravi", Department = "Cardiology", AppointmentDate = new DateTime(2026, 6, 5), Status = "Scheduled", ConsultationFee = 700 },
                new Appointment { AppointmentId = 2, PatientName = "Priya", Department = "Neurology", AppointmentDate = new DateTime(2026, 6, 3), Status = "Completed", ConsultationFee = 600 },
                new Appointment { AppointmentId = 3, PatientName = "Arun", Department = "Cardiology", AppointmentDate = new DateTime(2026, 6, 10), Status = "Completed", ConsultationFee = 800 },
                new Appointment { AppointmentId = 4, PatientName = "Meena", Department = "Dental", AppointmentDate = new DateTime(2026, 5, 28), Status = "Cancelled", ConsultationFee = 400 },
                new Appointment { AppointmentId = 5, PatientName = "Kumar", Department = "Ortho", AppointmentDate = new DateTime(2026, 6, 15), Status = "Scheduled", ConsultationFee = 500 }
            };

            Console.WriteLine("\nAll Appointments:");
            DisplayList(appointments);

            Console.WriteLine("\nScheduled Appointments:");
            DisplayList(appointments.Where(a => a.Status == "Scheduled").ToList());

            Console.WriteLine("\nCompleted Appointments:");
            DisplayList(appointments.Where(a => a.Status == "Completed").ToList());

            Console.WriteLine("\nCardiology Appointments:");
            DisplayList(appointments.Where(a => a.Department == "Cardiology").ToList());

            Console.WriteLine("\nConsultation Fee Greater Than 500:");
            DisplayList(appointments.Where(a => a.ConsultationFee > 500).ToList());

            Console.WriteLine("\nAppointments Sorted By Date:");
            DisplayList(appointments.OrderBy(a => a.AppointmentDate).ToList());

            Console.WriteLine("\nSearch Appointment By Patient Name:");
            Console.Write("Enter patient name: ");
            string name = Console.ReadLine();

            var searchResult = appointments
                .Where(a => a.PatientName.ToLower().Contains(name.ToLower()))
                .ToList();

            DisplayList(searchResult);

            Console.WriteLine("\nGroup Appointments By Department:");
            var departmentGroups = appointments.GroupBy(a => a.Department);

            foreach (var group in departmentGroups)
            {
                Console.WriteLine($"\nDepartment: {group.Key}");
                DisplayList(group.ToList());
            }

            Console.WriteLine("\nCount Appointments By Status:");
            var statusCount = appointments.GroupBy(a => a.Status);

            foreach (var group in statusCount)
            {
                Console.WriteLine($"{group.Key} : {group.Count()}");
            }

            double totalRevenue = appointments
                .Where(a => a.Status == "Completed")
                .Sum(a => a.ConsultationFee);

            Console.WriteLine($"\nTotal Revenue From Completed Appointments: {totalRevenue}");

            double averageFee = appointments.Average(a => a.ConsultationFee);

            Console.WriteLine($"\nAverage Consultation Fee: {averageFee}");

            Console.WriteLine("\nUpcoming Appointments:");
            DisplayList(appointments
                .Where(a => a.AppointmentDate >= DateTime.Today && a.Status == "Scheduled")
                .OrderBy(a => a.AppointmentDate)
                .ToList());

            Console.ReadLine();
        }

        static void DisplayList(List<Appointment> list)
        {
            Console.WriteLine($"{"ID",-5} {"Patient",-15} {"Department",-15} {"Date",-15} {"Status",-12} Fee");
            Console.WriteLine("-----------------------------------------------------------------------");

            foreach (var appointment in list)
            {
                appointment.Display();
            }
        }
    }
} 
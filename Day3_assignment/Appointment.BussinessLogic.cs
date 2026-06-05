using System;

namespace AppointmentApp
{
    public partial class Appointment
    {
        public void Display()
        {
            Console.WriteLine($"{AppointmentId,-5} {PatientName,-15} {Department,-15} {AppointmentDate.ToShortDateString(),-15} {Status,-12} {ConsultationFee}");
        }
    }
}
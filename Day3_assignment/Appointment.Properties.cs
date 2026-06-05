using System;

namespace AppointmentApp
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string Department { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public double ConsultationFee { get; set; }
    }
}
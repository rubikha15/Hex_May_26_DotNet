using System;

namespace AppointmentApp
{
    public partial class Appointment
    {
        public bool IsValid()
        {
            return AppointmentId > 0 &&
                   !string.IsNullOrEmpty(PatientName) &&
                   !string.IsNullOrEmpty(Department) &&
                   ConsultationFee > 0;
        }
    }
}
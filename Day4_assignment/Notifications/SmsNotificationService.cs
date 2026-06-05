using Day4_Assignment.Models;

namespace Day4_Assignment.Notifications
{
    public class SmsNotificationService : INotificationServices
    {
        public void SendNotification(CourierBooking booking)
        {
            Console.WriteLine($"SMS sent to {booking.Customer.MobileNumber}: Courier booking confirmed.");
        }
    }
}
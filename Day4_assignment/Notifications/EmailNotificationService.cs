using Day4_Assignment.Models;

namespace Day4_Assignment.Notifications
{
    public class EmailNotificationService : INotificationServices
    {
        public void SendNotification(CourierBooking booking)
        {
            Console.WriteLine($"Email sent to {booking.Customer.Email}: Courier booking confirmed.");
        }
    }
}
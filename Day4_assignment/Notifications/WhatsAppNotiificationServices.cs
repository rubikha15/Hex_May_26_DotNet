using Day4_Assignment.Models;

namespace Day4_Assignment.Notifications
{
    public class WhatsAppNotificationService : INotificationServices
    {
        public void SendNotification(CourierBooking booking)
        {
            Console.WriteLine($"WhatsApp message sent to {booking.Customer.MobileNumber}: Courier booking confirmed.");
        }
    }
}
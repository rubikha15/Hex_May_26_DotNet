using Day4_Assignment.Models;

namespace Day4_Assignment.Notifications
{
    public interface INotificationServices
    {
        void SendNotification(CourierBooking booking);
    }
}
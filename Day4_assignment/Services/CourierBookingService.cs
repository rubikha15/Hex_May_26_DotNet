using Day4_Assignment.Models;
using Day4_Assignment.DeliveryCalculators;
using Day4_Assignment.Notifications;
using Day4_Assignment.Invoices;

namespace Day4_Assignment.Services
{
    public class CourierBookingService
    {
        private readonly IDeliveryChargeCalculator _deliveryChargeCalculator;
        private readonly INotificationServices _notificationService;
        private readonly IInvoiceGenerator _invoiceGenerator;

        public CourierBookingService(
            IDeliveryChargeCalculator deliveryChargeCalculator,
            INotificationServices notificationService,
            IInvoiceGenerator invoiceGenerator)
        {
            _deliveryChargeCalculator = deliveryChargeCalculator;
            _notificationService = notificationService;
            _invoiceGenerator = invoiceGenerator;
        }

        public void BookCourier(CourierBooking booking)
        {
            booking.TotalCharge = _deliveryChargeCalculator.CalculateCharge(booking.Parcel);

            Console.WriteLine("\nCourier booked successfully!");

            _notificationService.SendNotification(booking);

            _invoiceGenerator.GenerateInvoice(booking);
        }
    }
}
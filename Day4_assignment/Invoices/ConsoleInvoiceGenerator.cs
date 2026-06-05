using Day4_Assignment.Models;


namespace Day4_Assignment.Invoices
{
    public class ConsoleInvoiceGenerator : IInvoiceGenerator
    {
        public void GenerateInvoice(CourierBooking booking)
        {
            Console.WriteLine("\n========== COURIER INVOICE ==========");
            Console.WriteLine($"Customer Name       : {booking.Customer.Name}");
            Console.WriteLine($"Source City         : {booking.SourceCity}");
            Console.WriteLine($"Destination City    : {booking.DestinationCity}");
            Console.WriteLine($"Parcel Weight       : {booking.Parcel.Weight} kg");
            Console.WriteLine($"Delivery Type       : {booking.DeliveryType}");
            Console.WriteLine($"Total Charge        : Rs. {booking.TotalCharge}");
            Console.WriteLine("=====================================");
        }
    }
}
using Day4_Assignment.Models;
using Day4_Assignment.DeliveryCalculators;
using Day4_Assignment.Notifications;
using Day4_Assignment.Invoices;
using Day4_Assignment.Services;

namespace Day4_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== SmartCourier Delivery Management System =====");

            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Customer Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Customer Mobile Number: ");
            string mobile = Console.ReadLine();

            Console.Write("Enter Parcel Weight: ");
            double weight = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Source City: ");
            string sourceCity = Console.ReadLine();

            Console.Write("Enter Destination City: ");
            string destinationCity = Console.ReadLine();

            Console.WriteLine("\nSelect Delivery Type:");
            Console.WriteLine("1. Standard Delivery");
            Console.WriteLine("2. Express Delivery");
            Console.WriteLine("3. International Delivery");
            Console.Write("Enter choice: ");
            int deliveryChoice = Convert.ToInt32(Console.ReadLine());

            IDeliveryChargeCalculator deliveryCalculator;
            string deliveryType;

            if (deliveryChoice == 1)
            {
                deliveryCalculator = new StandardDeliveryCalculator();
                deliveryType = "Standard Delivery";
            }
            else if (deliveryChoice == 2)
            {
                deliveryCalculator = new ExpressDeliveryCalculator();
                deliveryType = "Express Delivery";
            }
            else if (deliveryChoice == 3)
            {
                deliveryCalculator = new InternationalDeliveryCalculator();
                deliveryType = "International Delivery";
            }
            else
            {
                Console.WriteLine("Invalid delivery type.");
                return;
            }

            Console.WriteLine("\nSelect Notification Type:");
            Console.WriteLine("1. Email");
            Console.WriteLine("2. SMS");
            Console.WriteLine("3. WhatsApp");
            Console.Write("Enter choice: ");
            int notificationChoice = Convert.ToInt32(Console.ReadLine());

            INotificationServices notificationService;

            if (notificationChoice == 1)
            {
                notificationService = new EmailNotificationService();
            }
            else if (notificationChoice == 2)
            {
                notificationService = new SmsNotificationService();
            }
            else if (notificationChoice == 3)
            {
                notificationService = new WhatsAppNotificationService();
            }
            else
            {
                Console.WriteLine("Invalid notification type.");
                return;
            }

            Customer customer = new Customer(name, email, mobile);
            Parcel parcel = new Parcel(weight);

            CourierBooking booking = new CourierBooking(
                customer,
                parcel,
                sourceCity,
                destinationCity,
                deliveryType
            );

            IInvoiceGenerator invoiceGenerator = new ConsoleInvoiceGenerator();

            CourierBookingService bookingService = new CourierBookingService(
                deliveryCalculator,
                notificationService,
                invoiceGenerator
            );

            bookingService.BookCourier(booking);

            Console.ReadLine();
        }
    }
}
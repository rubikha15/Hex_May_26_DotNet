using Day4_Assignment.Models;

namespace Day4_Assignment.Invoices
{
    public interface IInvoiceGenerator
    {
        void GenerateInvoice(CourierBooking booking);
    }
}
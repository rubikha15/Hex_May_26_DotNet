using System;
using ECommerceBillingApp.Services;

namespace ECommerceBillingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderBillingService billingService = new OrderBillingService();

            Console.Write("Enter Product Price: ");
            decimal productPrice = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            try
            {
                decimal subTotal = billingService.CalculateSubTotal(productPrice, quantity);
                decimal discount = billingService.CalculateDiscount(subTotal);
                decimal amountAfterDiscount = subTotal - discount;
                decimal deliveryCharge = billingService.CalculateDeliveryCharge(amountAfterDiscount);
                decimal finalAmount = billingService.CalculateFinalAmount(productPrice, quantity);

                Console.WriteLine("\n----- Bill Details -----");
                Console.WriteLine("Sub Total: " + subTotal);
                Console.WriteLine("Discount: " + discount);
                Console.WriteLine("Amount After Discount: " + amountAfterDiscount);
                Console.WriteLine("Delivery Charge: " + deliveryCharge);
                Console.WriteLine("Final Amount: " + finalAmount);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
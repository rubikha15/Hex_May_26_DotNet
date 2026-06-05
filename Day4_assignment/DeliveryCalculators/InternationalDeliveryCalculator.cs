using Day4_Assignment.Models;

namespace Day4_Assignment.DeliveryCalculators
{
    public class InternationalDeliveryCalculator : IDeliveryChargeCalculator
    {
        public double CalculateCharge(Parcel parcel)
        {
            return parcel.Weight * 150 + 500;
        }
    }
}
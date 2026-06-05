using Day4_Assignment.Models;

namespace Day4_Assignment.DeliveryCalculators
{
    public class StandardDeliveryCalculator : IDeliveryChargeCalculator
    {
        public double CalculateCharge(Parcel parcel)
        {
            return parcel.Weight * 50;
        }
    }
}
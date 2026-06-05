using Day4_Assignment.Models;

namespace Day4_Assignment.DeliveryCalculators
{
    public interface IDeliveryChargeCalculator
    {
        double CalculateCharge(Parcel parcel);
    }
}
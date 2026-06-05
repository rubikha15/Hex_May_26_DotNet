namespace Day4_Assignment.Models
{
    public class CourierBooking
    {
        public Customer Customer { get; set; }
        public Parcel Parcel { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }
        public string DeliveryType { get; set; }
        public double TotalCharge { get; set; }

        public CourierBooking(Customer customer, Parcel parcel, string sourceCity, string destinationCity, string deliveryType)
        {
            Customer = customer;
            Parcel = parcel;
            SourceCity = sourceCity;
            DestinationCity = destinationCity;
            DeliveryType = deliveryType;
        }
    }
}
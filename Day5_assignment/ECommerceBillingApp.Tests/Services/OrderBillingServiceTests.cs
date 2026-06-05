using NUnit.Framework;
using ECommerceBillingApp.Services;

namespace ECommerceBillingApp.Tests.Services
{
    [TestFixture]
    public class OrderBillingServiceTests
    {
        private OrderBillingService _billingService;

        [SetUp]
        public void Setup()
        {
            _billingService = new OrderBillingService();
        }

        [Test]
        public void CalculateSubTotal_ValidInput_ReturnsCorrectSubtotal()
        {
            decimal result = _billingService.CalculateSubTotal(1000, 2);

            Assert.That(result, Is.EqualTo(2000));
        }

        [Test]
        public void CalculateDiscount_WhenSubtotalIs5000_Returns10PercentDiscount()
        {
            decimal discount = _billingService.CalculateDiscount(5000);

            Assert.That(discount, Is.EqualTo(500));
        }

        [Test]
        public void CalculateDiscount_WhenSubtotalIs3000_Returns5PercentDiscount()
        {
            decimal discount = _billingService.CalculateDiscount(3000);

            Assert.That(discount, Is.EqualTo(150));
        }

        [Test]
        public void CalculateDiscount_WhenSubtotalIs1500_ReturnsZeroDiscount()
        {
            decimal discount = _billingService.CalculateDiscount(1500);

            Assert.That(discount, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDeliveryCharge_WhenAmountIsLessThan1000_Returns100()
        {
            decimal charge = _billingService.CalculateDeliveryCharge(900);

            Assert.That(charge, Is.EqualTo(100));
        }

        [Test]
        public void CalculateDeliveryCharge_WhenAmountIs1000OrMore_ReturnsZero()
        {
            decimal charge = _billingService.CalculateDeliveryCharge(1000);

            Assert.That(charge, Is.EqualTo(0));
        }

        [Test]
        public void CalculateFinalAmount_WithDiscount_ReturnsCorrectAmount()
        {
            decimal finalAmount = _billingService.CalculateFinalAmount(1000, 5);

            Assert.That(finalAmount, Is.EqualTo(4500));
        }
    }
}
using FlyEatsApp.Models;

namespace FlyEatsApp.Payloads
{
    public class OrderPayload: Filter
    {
        public int? BusinessId { get; set; }
        public string OrderStatus { get; set; }
    }
}

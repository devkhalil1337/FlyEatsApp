using System.Data;

namespace FlyEatsApp.Models
{
    public class PaymentGateway
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string GatewayName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public bool IsActive { get; set; }
        public int PaymentMode { get; set; }

        public static PaymentGateway ExtractFromDataRow(DataRow dataRow)
        {
            PaymentGateway paymentGateway = new PaymentGateway();
            paymentGateway.Id = (int)dataRow["Id"];
            paymentGateway.BusinessId = (int)dataRow["BusinessId"];
            paymentGateway.GatewayName = dataRow["GatewayName"].ToString();
            paymentGateway.ApiKey = dataRow["ApiKey"].ToString();
            paymentGateway.ApiSecret = dataRow["ApiSecret"].ToString();
            paymentGateway.IsActive = (bool)dataRow["IsActive"];
            paymentGateway.PaymentMode = (int)dataRow["PaymentMode"];

            return paymentGateway;
        }
    }

    public class PaymentGatewayKeys
    {
        public int BusinessId { get; set; }
        public string GatewayName { get; set; }
        public string ApiKey { get; set; }

        public static PaymentGatewayKeys ExtractFromDataRow(DataRow dataRow)
        {
            PaymentGatewayKeys paymentGateway = new PaymentGatewayKeys();
            paymentGateway.BusinessId = (int)dataRow["BusinessId"];
            paymentGateway.GatewayName = dataRow["GatewayName"].ToString();
            paymentGateway.ApiKey = dataRow["ApiKey"].ToString();

            return paymentGateway;
        }
    }

    public class PaymentCharge
    {
        public decimal amount { get; set; }
       
    }
}

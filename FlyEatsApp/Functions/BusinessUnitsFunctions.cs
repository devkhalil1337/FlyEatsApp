namespace FlyEatsApp.Functions
{
    public class BusinessUnitsFunctions
    {

        public int GetBusinessIdFromHeaders(HttpRequest request)
        {
            int businessId = -1; // default value

            if (request.Headers.TryGetValue("businessId", out var headerValue))
            {
                int.TryParse(headerValue, out businessId);
            }

            return businessId;
        }

    }
}

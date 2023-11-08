using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PaymentCalculationApp.Web.Helper
{
    public class CustomSerializer : OutputFormatter
    {
        public CustomSerializer()
        {
            SupportedMediaTypes.Add("application/custom"); // Define the custom media type
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            var data = context.Object;

            // Implement the logic to serialize 'data' into the custom format
            // For simplicity, we'll just use a placeholder string in this example.
            var customData =  data.ToString();

            return response.WriteAsync(customData);
        }
    }
}
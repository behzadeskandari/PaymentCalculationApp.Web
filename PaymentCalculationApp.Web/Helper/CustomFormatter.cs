using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace PaymentCalculationApp.Web.Helper
{
    public class CustomFormatter : OutputFormatter
    {
        public CustomFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/custom"));
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            var serializer = new CustomSerializer();
            var result = serializer.WriteResponseBodyAsync(context);
            var data = context.Object;

            // Implement the logic to serialize 'data' into the custom format
            // For simplicity, we'll just use a placeholder string in th
            // is example.
            var customData =  data.ToString();
            return response.WriteAsync(customData);
        }
    }
}

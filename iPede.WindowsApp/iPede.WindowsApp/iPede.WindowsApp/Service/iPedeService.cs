using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;

namespace iPede.WindowsApp.Service
{
    public class iPedeService
    {
        private HttpClient httpClient;
        private Uri serviceUri;

        public iPedeService()
        {
            httpClient = new HttpClient();
            serviceUri = new Uri("http://ipede.yanscorp.com/api/products");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            HttpResponseMessage response = await httpClient.GetAsync(serviceUri).AsTask();
            var text = await response.Content.ReadAsStringAsync();

            JsonArray jsResponse = JsonArray.Parse(text);
            return jsResponse
                .Where(o => o.ValueType == JsonValueType.Object)
                .Select(o => FromJson(o.GetObject()));
        }

        public async Task<IEnumerable<Product>> GetSuggestedProduct()
        {
            return (await GetProducts()).Where(p => p.IsSuggested);
        }

        private Product FromJson(JsonObject o)
        {
            return new Product
            {
                ProductId = (int)o.GetNamedNumber("ProductId"),
                CategoryId = (int)o.GetNamedNumber("CategoryId"),
                Name = o.GetNamedString("Name"),
                ShortDescription = o.GetNamedString("ShortDescription"),
                FullDescription = StringFromJson(o["FullDescription"]),
                Price = (decimal)o.GetNamedNumber("Price"),
                CategoryName = o.GetNamedString("CategoryName"),
                MainImageUrl = StringFromJson(o["MainImageUrl"]),
                IsSuggested = o.GetNamedBoolean("IsSuggested")
            };
        }

        private string StringFromJson(IJsonValue value)
        {
            return value.ValueType == JsonValueType.String ? value.GetString() : null;
        }
    }
}

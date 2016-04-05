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
            serviceUri = new Uri("http://localhost:58921/api/products/");
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

        private Product FromJson(JsonObject o)
        {
            return new Product
            {
                ProductId = (int)o.GetNamedNumber("ProductId"),
                CategoryId = (int)o.GetNamedNumber("CategoryId"),
                Name = o.GetNamedString("Name"),
                ShortDescription = o.GetNamedString("ShortDescription"),
                FullDescription = o.GetNamedString("FullDescription"),
                Price = (decimal)o.GetNamedNumber("Price"),
                CategoryName = o.GetNamedString("FullDescription")
            };
        }
    }
}

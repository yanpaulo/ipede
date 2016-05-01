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
        private Uri productsUri,
            categorizedProductsUri;

        public iPedeService()
        {
            httpClient = new HttpClient();
            productsUri = new Uri("http://ipede.yanscorp.com/api/products");
            categorizedProductsUri = new Uri("http://ipede.yanscorp.com/api/products/categorized");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            HttpResponseMessage response = await httpClient.GetAsync(productsUri).AsTask();
            var text = await response.Content.ReadAsStringAsync();

            JsonArray jsResponse = JsonArray.Parse(text);
            return jsResponse
                .Where(o => o.ValueType == JsonValueType.Object)
                .Select(o => ProductFromJson(o.GetObject()));
        }

        public async Task<IEnumerable<Product>> GetSuggestedProduct()
        {
            return (await GetProducts()).Where(p => p.IsSuggested);
        }

        public async Task<IEnumerable<Category>> GetProductsCategorized()
        {
            HttpResponseMessage response = await httpClient.GetAsync(categorizedProductsUri).AsTask();
            var text = await response.Content.ReadAsStringAsync();

            JsonArray jsResponse = JsonArray.Parse(text);
            return jsResponse
                .Where(o => o.ValueType == JsonValueType.Object)
                .Select(o => CategoryFromJson(o.GetObject()));
        }

        private Category CategoryFromJson(JsonObject o)
        {
            return new Category
            {
                Id = (int)o.GetNamedNumber("CategoryId"),
                Name = o.GetNamedString("Name"),
                ParentCategoryId = IntFromJson(o["ParentCategoryId"]),
                ParentCategoryName = StringFromJson(o["ParentCategoryName"]),
                SubCategories = o.GetNamedArray("SubCategories").Select(p => CategoryFromJson(p.GetObject())),
                Products = o.GetNamedArray("Products").Select(p => ProductFromJson(p.GetObject()))
            };
        }

        private Product ProductFromJson(JsonObject o)
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

        private int? IntFromJson(IJsonValue value)
        {
            return value.ValueType == JsonValueType.Number ? (int?)value.GetNumber() : null;
        }
    }
}

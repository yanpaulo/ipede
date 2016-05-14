using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Json;

namespace IPede.App.Models
{
    public class IPedeService
    {
        private HttpClient httpClient;
        private Uri productsUri,
            categorizedProductsUri;
        private static IEnumerable<Product> _products;
        private static IEnumerable<Category> _categoriesWithProducts;

        public IPedeService()
        {
            httpClient = new HttpClient();
            productsUri = new Uri("http://ipede.yanscorp.com/api/products");
            categorizedProductsUri = new Uri("http://ipede.yanscorp.com/api/products/categorized");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            _products = _products ?? await LoadProducts();
            return _products;
        }

        public async Task<IEnumerable<Product>> GetSuggestedProducts()
        {
            return (await GetProducts()).Where(p => p.IsSuggested);
        }

        public async Task<IEnumerable<Category>> GetProductsCategorized()
        {
            _categoriesWithProducts = _categoriesWithProducts ?? await LoadProductsCategorized();
            //Ordena Categorias, sub-categorias e produtos por nome.
            var result = _categoriesWithProducts
                .OrderBy(c => c.Name)
                .Select(c =>
                {
                    var subList = c.SubCategories.ToList();
                    //Adds a new nameless Category for products outside of sub-categories.
                    if (c.Products.Count() > 0)
                    {
                        subList.Add(new Category { Name = "", Products = c.Products }); 
                    }
                    c.SubCategories = subList
                    .OrderBy(sub => sub.Name)
                    .Select(sub =>
                    {
                        sub.Products = sub.Products.OrderBy(p => p.Name);
                        return sub;
                    }
                    );
                    return c;
                });
            return result;
        }

        private async Task<IEnumerable<Product>> LoadProducts()
        {
            HttpResponseMessage response = await httpClient.GetAsync(productsUri);
            var text = await response.Content.ReadAsStringAsync();

            var jsResponse = JsonArray.Parse(text) as JsonArray;
            return jsResponse
                .Where(o => o.JsonType == JsonType.Object)
                .Select(o => ProductFromJson(o as JsonObject));
        }

        private async Task<IEnumerable<Category>> LoadProductsCategorized()
        {
            HttpResponseMessage response = await httpClient.GetAsync(categorizedProductsUri);
            var text = await response.Content.ReadAsStringAsync();

            JsonArray jsResponse = JsonArray.Parse(text) as JsonArray;
            return jsResponse
                .Where(o => o.JsonType == JsonType.Object)
                .Select(o => CategoryFromJson(o as JsonObject));
        }

        private Category CategoryFromJson(JsonObject o)
        {
            return new Category
            {
                Id = o["Id"],
                Name = o["Name"],
                ParentCategoryId = IntFromJson(o["ParentCategoryId"]),
                ParentCategoryName = StringFromJson(o["ParentCategoryName"]),
                SubCategories = ((JsonArray)o["SubCategories"]).Select(p => CategoryFromJson(p as JsonObject)),
                Products = ((JsonArray)o["Products"]).Select(p => ProductFromJson(p as JsonObject))
            };
        }

        private Product ProductFromJson(JsonObject o)
        {
            return new Product
            {
                ProductId = o["Id"],
                CategoryId = o["CategoryId"],
                Name = o["Name"],
                ShortDescription = o["ShortDescription"],
                FullDescription = StringFromJson(o["FullDescription"]),
                Price = o["Price"],
                CategoryName = o["CategoryName"],
                MainImageUrl = StringFromJson(o["MainImageUrl"]),
                MainImageThumbUrl = StringFromJson(o["MainImageThumbUrl"]),
                IsSuggested = o["IsSuggested"]
            };
        }

        private string StringFromJson(JsonValue value)
        {
            return value?.JsonType == JsonType.String ? value : null;
        }

        private int? IntFromJson(JsonValue value)
        {
            return value?.JsonType == JsonType.Number ? (int?)value : null;
        }
    }
}

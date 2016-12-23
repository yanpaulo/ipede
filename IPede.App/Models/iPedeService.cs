using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace IPede.App.Models
{
    public class IPedeService
    {
        private readonly string
#if DEBUG
            SERVICE_URL = "http://localhost:58921/api/",
#else
            SERVICE_URL = "http://ipede.yanscorp.com/api/",
#endif
            PRODUCTS_URL = "products",
            CATEGORIZED_PRODUCTS_URL = "products/categorized",
            TABLES_URL = "tables",
            ORDERS_URL = "orders",
            ORDER_ITEMS_URL = "orderItems";

        private static IPedeService _instance;
        private HttpClient httpClient;

        private static IEnumerable<Product> _products;
        private static IEnumerable<Category> _categoriesWithProducts;

        private IPedeService()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(SERVICE_URL)
            };

        }

        public static IPedeService Instance => _instance ?? (_instance = new IPedeService());

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
                    //For each Category
                    //Get a list of all SubCategories
                    var subList = c.SubCategories.ToList();
                    //If Category has Products outside any of its sub-categories
                    if (c.Products.Count() > 0)
                    {
                        //Add them to a new SubCategory without name
                        subList.Add(new Category { Name = "", Products = c.Products });
                    }

                    //Order SubCategories by name
                    c.SubCategories = subList
                    .OrderBy(sub => sub.Name)
                    .Select(sub =>
                    {
                        //For each SubCategory, order its children Products.
                        sub.Products = sub.Products.OrderBy(p => p.Name);
                        return sub;
                    }
                    );
                    return c;
                });
            return result;
        }

        public async Task<Table> GetTable(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{TABLES_URL}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Table>(text);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task<Order> PostOrder(Order order)
        {
            var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ORDERS_URL, content);
            var text = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Order>(text);
        }
        public async Task<OrderItem> PostOrderItem(OrderItem item)
        {
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ORDER_ITEMS_URL, content);
            var text = await response.Content.ReadAsStringAsync();
            var product = (await GetProducts()).Single(p => p.Id == item.ProductId);
            item = JsonConvert.DeserializeObject<OrderItem>(text);
            item.Product = product;
            return item;
        }


        private async Task<IEnumerable<Product>> LoadProducts()
        {
            HttpResponseMessage response = await httpClient.GetAsync(PRODUCTS_URL);
            var text = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(text);
        }

        private async Task<IEnumerable<Category>> LoadProductsCategorized()
        {
            HttpResponseMessage response = await httpClient.GetAsync(CATEGORIZED_PRODUCTS_URL);
            var text = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Category>>(text);
        }
    }
}

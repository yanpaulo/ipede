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
        private HttpClient httpClient;
        private readonly Uri productsUri,
            categorizedProductsUri,
            tablesUri,
            ordersUri;
        private static IEnumerable<Product> _products;
        private static IEnumerable<Category> _categoriesWithProducts;

        public IPedeService()
        {
            httpClient = new HttpClient();
            productsUri = new Uri("http://ipede.yanscorp.com/api/products");
            categorizedProductsUri = new Uri("http://ipede.yanscorp.com/api/products/categorized");
            tablesUri = new Uri("http://ipede.yanscorp.com/api/tables");
            ordersUri = new Uri("http://ipede.yanscorp.com/api/orders");
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
            HttpResponseMessage response = await httpClient.GetAsync($"{tablesUri}/{id}");
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
            var response = await httpClient.PostAsync(ordersUri, content);
            var text = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Order>(text);
        }

        private async Task<IEnumerable<Product>> LoadProducts()
        {
            HttpResponseMessage response = await httpClient.GetAsync(productsUri);
            var text = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(text);
        }

        private async Task<IEnumerable<Category>> LoadProductsCategorized()
        {
            HttpResponseMessage response = await httpClient.GetAsync(categorizedProductsUri);
            var text = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Category>>(text);
        }
    }
}

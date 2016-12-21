
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using IPede.App.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IPede.WindowsApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductPage : Page
    {
        private ModelContext _context = ModelContext.Instance;
        private IPedeService _service = new IPedeService();
        public ProductPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Product p = (Product)e.Parameter;
            this.DataContext = p;

            base.OnNavigatedTo(e);
        }

        private async void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = _context.ActiveOrder;
            var product = (Product)DataContext;
            var item = new OrderItem() { OrderId = order.Id,  ProductId = product.Id, Price = product.Price, Quantity = 1 };

            item = await _service.PostOrderItem(item);
            order.Items.Add(item);

            AddToCartButton.Content = "No Pedido";
            AddToCartButton.IsEnabled = false;
        }
    }
}

using iPede.WindowsApp.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace iPede.WindowsApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SuggestedProductsPage : Page
    {
        private iPedeService service = new iPedeService();

        public SuggestedProductsPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductsControl.ItemsSource = await service.GetSuggestedProducts();
        }

        private void FoodItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button source = (Button)sender;
            this.Frame.Navigate(typeof(ProductPage), source.DataContext);
        }
    }


}

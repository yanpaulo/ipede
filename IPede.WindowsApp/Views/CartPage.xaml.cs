using IPede.App.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IPede.WindowsApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CartPage : Page
    {
        private ModelContext _context = ModelContext.Instance;
        private IPedeService _service = IPedeService.Instance;

        public CartPage()
        {
            this.DataContext = _context.ActiveOrder;
            this.InitializeComponent();
        }

        private void CartPage_Loaded(object sender, RoutedEventArgs e)
        {
            var order = _context.ActiveOrder;
            //Hides MainPage AppBar
            MainPage.Current.BottomAppBar.Visibility = Visibility.Collapsed;
            //Put the command bar in the bottom row on MainPage.
            pageGrid.Children.Remove(commandBar);
            Grid.SetRow(commandBar, 1);
            MainPage.Current.PageGrid.Children.Add(commandBar);

            AcceptAppBarButton.IsEnabled = order.StatusName == OrderStatusNames.Open ? true : false;
        }

        private async void AcceptAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var order = _context.ActiveOrder;
            var list = _context.Table.Orders;
            var listIndex = list.IndexOf(order);

            order = await _service.PostOrder(order);
            _context.Table.Orders[listIndex] = order;

            //If there's more than one Order at the list
            if (list.Count > 1)
            {
                //Mark the ActiveOrder as the next one, if there is one. Otherwise, the anterior one.
                _context.ActiveOrder = list[listIndex < list.Count - 1 ?
                    listIndex + 1 :
                    listIndex - 1];
            }
            else
            {
                this.DataContext = order;
                new MessageDialog("Pedido Realizado.").ShowAsync();
            }

        }
    }
}

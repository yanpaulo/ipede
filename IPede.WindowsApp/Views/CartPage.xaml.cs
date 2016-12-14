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
        public CartPage()
        {
            this.DataContext = ModelContext.Instance.ActiveOrder;
            this.InitializeComponent();
        }

        private void CartPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Hides MainPage AppBar
            MainPage.Current.BottomAppBar.Visibility = Visibility.Collapsed;
            //Put the command bar in the bottom row on MainPage.
            pageGrid.Children.Remove(commandBar);
            Grid.SetRow(commandBar, 1);
            MainPage.Current.PageGrid.Children.Add(commandBar);
        }

        private async void AcceptAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("Dá pra comprar n, man").ShowAsync();
        }
    }
}

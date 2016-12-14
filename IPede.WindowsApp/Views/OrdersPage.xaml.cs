﻿using IPede.App.Models;
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

namespace IPede.WindowsApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrdersPage : Page
    {
        private ModelContext _context = ModelContext.Instance;
        
        public OrdersPage()
        {
            this.DataContext = _context;

            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Hides MainPage AppBar
            MainPage.Current.BottomAppBar.Visibility = Visibility.Collapsed;
            //Put the command bar in the bottom row on MainPage.
            pageGrid.Children.Remove(commandBar);
            Grid.SetRow(commandBar, 4);
            MainPage.Current.PageGrid.Children.Add(commandBar);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var order = (Order)e.ClickedItem;
            _context.ActiveOrder = order;
            MainPage.Current.AppFrame.Navigate(typeof(CartPage));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var o = new Order();
            _context.Table.Orders.Add(_context.ActiveOrder = new Order());
            MainPage.Current.AppFrame.Navigate(typeof(CartPage));
        }
    }
}

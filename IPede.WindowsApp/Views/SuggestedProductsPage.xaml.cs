﻿using System;
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
using IPede.App.Models;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IPede.WindowsApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SuggestedProductsPage : Page
    {
        private IPedeService _service = IPedeService.Instance;

        public SuggestedProductsPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            ProductsControl.ItemsSource = await _service.GetSuggestedProducts();
            progressRing.IsActive = false;
        }

        private void FoodItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button source = (Button)sender;
            this.Frame.Navigate(typeof(ProductPage), source.DataContext);
        }
    }


}

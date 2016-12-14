
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using IPede.App.Models;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IPede.WindowsApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategorizedProductsPage : Page
    {
        private IPedeService service = new IPedeService();

        public CategorizedProductsPage()
        {
            this.InitializeComponent();
        }

        public List<Category> Categories { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            var categories = await service.GetProductsCategorized();
            cvs.Source = categories.ToList();
            (semanticZoom.ZoomedOutView as ListViewBase).ItemsSource = cvs.View.CollectionGroups;
            zoomedInListView.ItemsSource = cvs.View;
            progressRing.IsActive = false;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(ProductPage), e.ClickedItem, new DrillInNavigationTransitionInfo());
        }
    }
}

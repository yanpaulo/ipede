using IPede.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using ZXing.Mobile;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IPede.WindowsApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        private IPedeService _service = IPedeService.Instance;
        private ModelContext _context = ModelContext.Instance;

        public StartPage()
        {
            this.InitializeComponent();
        }
        

        private async void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            var scanner = new MobileBarcodeScanner();
            var result = await scanner.Scan();
            await HandleCode(result?.Text);
            
        }

        private async void CodeButton_Click(object sender, RoutedEventArgs e)
        {
            await HandleCode(CodeTextBox.Text);
        }

        private async void CodeTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                await HandleCode(CodeTextBox.Text);
            }
        }

        private async Task HandleCode(string s)
        {
            mainPanel.Visibility = Visibility.Collapsed;
            progressRing.IsActive = true;
            try
            {
                int code = int.Parse(s);
                var table = await _service.GetTable(code);
                _context.Table = table;
                _context.ActiveOrder = table.Orders.FirstOrDefault();

                Frame.Navigate(typeof(MainPage));
            }
            catch (Exception)
            {
                await new MessageDialog("Código Inválido.").ShowAsync();
            }
            finally
            {
                mainPanel.Visibility = Visibility.Visible;
                progressRing.IsActive = false;
            }
        }
    }
}

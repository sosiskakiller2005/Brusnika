using Brusnika.AppData;
using Brusnika.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Brusnika.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeliveryPage.xaml
    /// </summary>
    public partial class DeliveryPage : Page
    {
        private static BrusnikaDbEntities _context = App.GetContext();
        public DeliveryPage()
        {
            InitializeComponent();
            DeliveryiesLb.ItemsSource = _context.Delivery.ToList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameHelper.selectedFrame.Navigate(new NavigationPage());
        }
    }
}

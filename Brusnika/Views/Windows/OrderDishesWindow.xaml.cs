using Brusnika.AppData;
using Brusnika.Model;
using Brusnika.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Brusnika.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrderDishesWindow.xaml
    /// </summary>
    public partial class OrderDishesWindow : Window
    {
        private static BrusnikaDbEntities _context = App.GetContext();
        public ObservableCollection<DIsh> Dishes { get; set; }
        public ObservableCollection<DishOrderVM> SelectedDishes { get; set; } = new ObservableCollection<DishOrderVM>();
        public DIsh SelectedDish { get; set; }
        public DishOrderVM SelectedFromOrder { get; set; }

        public List<DishDelivery> dishDeliveries = new List<DishDelivery>();
        public OrderDishesWindow()
        {
            InitializeComponent();
            Dishes = new ObservableCollection<DIsh>(_context.DIsh.ToList());
            DataContext = this;
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDish != null && !SelectedDishes.Any(x => x.Dish.Id == SelectedDish.Id))
            {
                SelectedDishes.Add(new DishOrderVM { Dish = SelectedDish, Quantity = 1 });
            }
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFromOrder != null)
                SelectedDishes.Remove(SelectedFromOrder);
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DishOrderVM item)
                item.Quantity++;
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DishOrderVM item)
            {
                item.Quantity--;
                if (item.Quantity < 1)
                    item.Quantity = 1;
            }
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDishes.Count == 0)
            {
                MessageBox.Show("Добавьте блюда в заказ.");
                return;
            }
            int lastId = _context.Delivery.OrderByDescending(d => d.Id).FirstOrDefault()?.Id ?? 0;

            foreach (var item in SelectedDishes)
            {
                    dishDeliveries.Add(new DishDelivery
                    {
                        DeliveryId = lastId++,
                        DIsh = item.Dish,
                        Quantity = item.Quantity
                    });
            }
            DialogResult = true;
            Close();
        }
    }

    public class DishOrderVM : System.ComponentModel.INotifyPropertyChanged
    {
        public DIsh Dish { get; set; }
        private int _quantity = 1;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 1) value = 1;
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }
}

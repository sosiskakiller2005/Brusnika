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
using System.Windows.Shapes;

namespace Brusnika.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditOrderWIndow.xaml
    /// </summary>
    public partial class AddEditOrderWIndow : Window
    {
        private static BrusnikaDbEntities _context = App.GetContext();

        private Delivery _selectedDelivery;
        private Delivery _newDelivery = new Delivery();
        private List<DishDelivery> _dishDeliveries = new List<DishDelivery>();
        public AddEditOrderWIndow()
        {
            InitializeComponent();
            EditBtn.Visibility = Visibility.Collapsed;
            TitleTbl.Text = "Новый заказ";
            UserCmb.ItemsSource = _context.User.ToList();
            UserCmb.DisplayMemberPath = "Lastname";
            DateTimeTbl.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
        }
        public AddEditOrderWIndow(Delivery selectedDelivery)
        {
            InitializeComponent();
            _selectedDelivery = selectedDelivery;
            DataContext = _selectedDelivery;
            UserCmb.ItemsSource = _context.User.ToList();
            UserCmb.DisplayMemberPath = "Lastname";
            AddBtn.Visibility = Visibility.Collapsed;
            TitleTbl.Text = "Редактирование заказа";
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AdressTb.Text) && !string.IsNullOrEmpty(PhoneTb.Text) && UserCmb.SelectedItem != null)
            {

                if (CommentTb.Text == null)
                {
                    Delivery newDelivery = new Delivery()
                    {
                        DateTIme = DateTime.Now,
                        Adress = AdressTb.Text,
                        PhoneNumber = PhoneTb.Text,
                        User = UserCmb.SelectedItem as User
                    };
                    _context.Delivery.Add(newDelivery);
                    _context.SaveChanges();
                    var deliveryId = newDelivery.Id;
                    foreach (var item in _dishDeliveries)
                    {
                        item.DeliveryId = deliveryId;
                    }

                    Delivery lastDelivery = _context.Delivery.OrderByDescending(d => d.Id).FirstOrDefault();
                    if (_dishDeliveries == null || !_dishDeliveries.Any())
                    {
                        MessageBoxHelper.Error("Добавьте хотя бы одно блюдо к заказу.", "Ошибка");
                        return;
                    }

                    foreach (var item in _dishDeliveries)
                    {
                        if (item == null || item.DIsh == null)
                        {
                            MessageBoxHelper.Error("В списке блюд есть некорректные записи.", "Ошибка");
                            return;
                        }
                        if (item.DIsh.Id <= 0)
                        {
                            MessageBoxHelper.Error("Некорректный идентификатор блюда.", "Ошибка");
                            return;
                        }
                        if (!item.Quantity.HasValue || item.Quantity.Value <= 0)
                        {
                            MessageBoxHelper.Error("Укажите количество для каждого блюда.", "Ошибка");
                            return;
                        }
                    }
                    foreach (var item in _dishDeliveries)
                    {
                        _context.DishDelivery.Add(item);
                    }
                    _context.SaveChanges();
                    MessageBoxHelper.Information("Заказ успешно добавлен");
                    DialogResult = true;
                }
                else
                {
                    Delivery newDelivery = new Delivery()
                    {
                        DateTIme = DateTime.Now,
                        Adress = AdressTb.Text,
                        PhoneNumber = PhoneTb.Text,
                        User = UserCmb.SelectedItem as User,
                        Comment = CommentTb.Text
                    };
                    _context.Delivery.Add(newDelivery);
                    _context.SaveChanges();
                    var deliveryId = newDelivery.Id;
                    foreach (var item in _dishDeliveries)
                    {
                        item.DeliveryId = deliveryId;
                    }

                    Delivery lastDelivery = _context.Delivery.OrderByDescending(d => d.Id).FirstOrDefault();
                    if (_dishDeliveries == null || !_dishDeliveries.Any())
                    {
                        MessageBoxHelper.Error("Добавьте хотя бы одно блюдо к заказу.", "Ошибка");
                        return;
                    }

                    foreach (var item in _dishDeliveries)
                    {
                        if (item == null || item.DIsh == null)
                        {
                            MessageBoxHelper.Error("В списке блюд есть некорректные записи.", "Ошибка");
                            return;
                        }
                        if (item.DIsh.Id <= 0)
                        {
                            MessageBoxHelper.Error("Некорректный идентификатор блюда.", "Ошибка");
                            return;
                        }
                        if (!item.Quantity.HasValue || item.Quantity.Value <= 0)
                        {
                            MessageBoxHelper.Error("Укажите количество для каждого блюда.", "Ошибка");
                            return;
                        }
                    }
                    foreach (var item in _dishDeliveries)
                    {
                        _context.DishDelivery.Add(item);
                    }
                    _context.SaveChanges();
                    MessageBoxHelper.Information("Заказ успешно добавлен");
                    DialogResult = true;
                }
            }
            else
            {
                MessageBoxHelper.Error("Пожалуйста, заполните все поля", "Ошибка");
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AdressTb.Text) && !string.IsNullOrEmpty(PhoneTb.Text) && UserCmb.SelectedItem != null)
            {
                if (CommentTb.Text == null)
                {
                    _selectedDelivery.Adress = AdressTb.Text;
                    _selectedDelivery.PhoneNumber = PhoneTb.Text;
                    _selectedDelivery.User = UserCmb.SelectedItem as User;
                    _context.SaveChanges();
                    MessageBoxHelper.Information("Заказ успешно изменен", "Успех");
                    DialogResult = true;
                }
                else
                {
                    _selectedDelivery.Adress = AdressTb.Text;
                    _selectedDelivery.PhoneNumber = PhoneTb.Text;
                    _selectedDelivery.Comment = CommentTb.Text;
                    _selectedDelivery.User = UserCmb.SelectedItem as User;
                    _context.SaveChanges();
                    MessageBoxHelper.Information("Заказ успешно изменен", "Успех");
                    DialogResult = true;
                }
            }
            else
            {
                MessageBoxHelper.Error("Пожалуйста, заполните все поля", "Ошибка");
            }
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderDishesWindow orderDishesWindow = new OrderDishesWindow();
            orderDishesWindow.ShowDialog();
            if (orderDishesWindow.DialogResult == true)
            {
                _dishDeliveries = orderDishesWindow.dishDeliveries;
            }
        }
    }
}

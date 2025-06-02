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
                    MessageBoxHelper.Information("Заказ успешно добавлен", "Успех");
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
                    MessageBoxHelper.Information("Заказ успешно добавлен", "Успех");
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
    }
}

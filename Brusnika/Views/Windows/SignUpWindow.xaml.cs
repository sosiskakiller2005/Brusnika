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
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private static BrusnikaDbEntities _context = App.GetContext();
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private bool CheckForEmptyValues()
        {
            if (LastnameTb.Text != string.Empty && NameTb.Text != string.Empty && LoginTb.Text != string.Empty && PassTb.Password != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void EntryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForEmptyValues())
            {
                User newUser = new User()
                {
                    Lastname = LastnameTb.Text,
                    Name = NameTb.Text,
                    Login = LoginTb.Text,
                    Password = PassTb.Password
                };
                _context.User.Add(newUser);
                _context.SaveChanges();
                MessageBoxHelper.Information("Пользователь добавлен.");
                AuthorisationWIndow authorisationWIndow = new AuthorisationWIndow();
                authorisationWIndow.Show();
                Close();
            }
            else
            {
                MessageBoxHelper.Error("Заполните все поля для ввода.");
            }
        }

        private void AuthorisationHl_Click(object sender, RoutedEventArgs e)
        {
            AuthorisationWIndow authorisationWIndow = new AuthorisationWIndow();
            authorisationWIndow.Show();
            Close();
        }
    }
}

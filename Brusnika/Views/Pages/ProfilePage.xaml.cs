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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private static BrusnikaDbEntities _context = App.GetContext();
        public ProfilePage()
        {
            InitializeComponent();
            DataContext = AuthorisationHelper.selectedUser;
            PasswordPb.Password = AuthorisationHelper.selectedUser.Password;
        }

        private void EditProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LastnameTb.Text) && !string.IsNullOrEmpty(NameTb.Text) && !string.IsNullOrEmpty(LoginTb.Text) && !string.IsNullOrEmpty(PasswordPb.Password))
            {
                AuthorisationHelper.selectedUser.Lastname = LastnameTb.Text;
                AuthorisationHelper.selectedUser.Name = NameTb.Text;
                AuthorisationHelper.selectedUser.Login = LoginTb.Text;
                AuthorisationHelper.selectedUser.Password = PasswordPb.Password;
                _context.SaveChanges();
                MessageBoxHelper.Information("Профиль успешно обновлён");
            }
            else
            {
                MessageBoxHelper.Error("Пожалуйста, заполните все поля");
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameHelper.selectedFrame.Navigate(new NavigationPage());
        }
    }
}

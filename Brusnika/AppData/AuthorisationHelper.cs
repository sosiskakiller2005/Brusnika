using Brusnika.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Brusnika.AppData
{
    internal class AuthorisationHelper
    {
        private static BrusnikaDbEntities _context = App.GetContext();
        public static User selectedUser;
        /// <summary>
        /// Авторизует пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        
        public static bool Authorise(string login, string password)
        {
            List<User> users = _context.User.ToList();

            if (login == string.Empty || password == string.Empty)
            {
                MessageBoxHelper.Error("Не все поля для ввода были заполнены.");
                return false;
            }
            else 
            {
                foreach (User user in users)
                {
                    if (user.Login == login && user.Password == password)
                    {
                        selectedUser = user;
                        return true;
                    }
                }
                if (selectedUser != null)
                {
                    return true;
                }
                else
                {
                    MessageBoxHelper.Error("Неправильно введен логин или пароль.");
                    return false;
                }
            }
        }
    }
}

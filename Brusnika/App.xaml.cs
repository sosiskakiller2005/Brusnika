using Brusnika.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Brusnika
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static BrusnikaDbEntities _context;
        public static BrusnikaDbEntities GetContext()
        {
            if (_context == null)
            {
                _context = new BrusnikaDbEntities();
            }
            return _context;
        }
    }
}

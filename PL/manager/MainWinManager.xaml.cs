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
using PL.OrderWin;
using PLL.ProductWin;

namespace PL.manager
{
    /// <summary>
    /// Interaction logic for MainWinManager.xaml
    /// </summary>
    public partial class MainWinManager : Window
    {
        public MainWinManager()
        {
            InitializeComponent();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void orders_Click(object sender, RoutedEventArgs e)
        {
            new MainOrderWin(EnumWin.ClientOrManager.manager).Show();
            this.Close();
        }
        private void products_Click(object sender, RoutedEventArgs e)
        {
            new MainProductWin(EnumWin.ClientOrManager.manager).Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}

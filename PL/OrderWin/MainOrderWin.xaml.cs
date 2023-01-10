using BO;
using PL.manager;
using PL.ProductWin;
using PLL.ProductWin;
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
using static PL.EnumWin;

namespace PL.OrderWin
{
    /// <summary>
    /// Interaction logic for MainOrderWin.xaml
    /// </summary>
    public partial class MainOrderWin : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private IEnumerable<BO.OrderForList> orders;
        ClientOrManager enumWin;
        public MainOrderWin(ClientOrManager x)
        {
            InitializeComponent();
            orders = bl.Order.GetAllToManager()!;

            OrderForList.ItemsSource = orders;

            enumWin = x;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(enumWin==EnumWin.ClientOrManager.manager)
                new Update((BO.OrderForList)OrderForList.SelectedItem).Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           if (enumWin == EnumWin.ClientOrManager.client)
                new MainProductWin(ClientOrManager.client).Show();
            else
                new MainWinManager().Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

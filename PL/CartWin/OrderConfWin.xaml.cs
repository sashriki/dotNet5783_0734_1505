using PL.OrderTraking;
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

namespace PL.CartWin
{
    /// <summary>
    /// Interaction logic for OrderConfWin.xaml
    /// </summary>
    public partial class OrderConfWin : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderConfWin(int orderID)
        {
            InitializeComponent();
            idOrder.Content = bl.Order.GetOrderByID(orderID).OrderId;
        }
    }
}

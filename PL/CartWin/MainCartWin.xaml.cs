using BlApi;
using BO;
using PLL.ProductWin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for MainCartWin.xaml
    /// </summary>
    public partial class MainCartWin : Window
    {
        static BO.Cart NewCart;
        public MainCartWin(BO.Cart newCart)
        {
            InitializeComponent();
            NewCart = new Cart();
            NewCart.OrderItems = new List<OrderItem?>();
            NewCart=newCart;
            if (NewCart.OrderItems.Count() == 0)
            {
                OrderItemList.Visibility = Visibility.Hidden;
                MakeOrder.Visibility=Visibility.Hidden;
            }
            else
                EmptyCart.Visibility = Visibility.Hidden;
            OrderItemList.ItemsSource = NewCart.OrderItems;
        }

        private void OrderItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
       
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainProductWin(NewCart).Show();
            this.Close();
        }

        private void MakeOrder_Click(object sender, RoutedEventArgs e)
        {
            new MakeOrderWin(NewCart).Show();
            this.Close();
        }
    }
}

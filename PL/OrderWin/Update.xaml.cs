using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.OrderWin
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        //BO.OrderForList orderForList;
        BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty OrderDep =
            DependencyProperty.Register(nameof(order), typeof(BO.Order),typeof(Update));

        BO.Order order
        {
            get => (BO.Order)GetValue(OrderDep);
            set => SetValue(OrderDep, value);
        }

        public Update(BO.Order ord)
        {
            InitializeComponent();
            //orderForList = ordForLst;
            status.ItemsSource = System.Enum.GetValues(typeof(BO.OrderStatus));
            status.SelectedIndex = 0;
            order = ord;
            OrderItemList.ItemsSource = order.OrderItems;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BO.OrderItem tmp = new OrderItem();
            tmp = (BO.OrderItem)((Button)sender).DataContext;
            bl.Order.UpdateToManager(order, tmp.ProductId, 0);
            OrderItemList.ItemsSource = null;
            OrderItemList.ItemsSource = bl.Order.GetOrderByID(order.OrderId).OrderItems;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BO.OrderItem tmp = new OrderItem();
            tmp = (BO.OrderItem)((Button)sender).DataContext;
            bl.Order.UpdateToManager(order, tmp.ProductId, tmp.AmountOfProduct++);
            OrderItemList.ItemsSource = null;
            OrderItemList.ItemsSource = bl.Order.GetOrderByID(order.OrderId).OrderItems;

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BO.OrderItem tmp = new OrderItem();
            tmp = (BO.OrderItem)((Button)sender).DataContext;
            bl.Order.UpdateToManager(order, tmp.ProductId, tmp.AmountOfProduct--);
            OrderItemList.ItemsSource = null;
            OrderItemList.ItemsSource = bl.Order.GetOrderByID(order.OrderId).OrderItems;

        }

        private void OrderItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

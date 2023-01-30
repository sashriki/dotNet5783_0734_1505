using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Security.Cryptography;
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
            order = ord;
            if (ord.OrderStatus == BO.OrderStatus.Confirmed)
            {
                status.Items.Add(BO.OrderStatus.Confirmed);
                status.Items.Add(BO.OrderStatus.Shipped);               
            }
            
            else if (ord.OrderStatus == BO.OrderStatus.Shipped)
            {
                status.Items.Add(BO.OrderStatus.Shipped);
                status.Items.Add(BO.OrderStatus.Delivered);
            }
            else if (ord.OrderStatus == BO.OrderStatus.Delivered)
            {
                status.Items.Add(BO.OrderStatus.Delivered);
            }

            status.SelectedIndex = 0;
            status.SelectedItem = ord.OrderStatus;

            OrderItemList.ItemsSource = order.OrderItems;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((BO.OrderStatus)status.SelectedItem != (BO.OrderStatus)order.OrderStatus)
            {
                if ((BO.OrderStatus)status.SelectedItem == BO.OrderStatus.Shipped)
                    order = bl.Order.ShippingUpdateToManager(order.OrderId);
                if ((BO.OrderStatus)status.SelectedItem == BO.OrderStatus.Delivered)
                    order = bl.Order.supplyUpdateToManager(order.OrderId);
            }
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
            bl.Order.UpdateToManager(order, tmp.ProductId, tmp.AmountOfProduct+1);
            OrderItemList.ItemsSource = null;
            OrderItemList.ItemsSource = bl.Order.GetOrderByID(order.OrderId).OrderItems;

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BO.OrderItem tmp = new OrderItem();
            tmp = (BO.OrderItem)((Button)sender).DataContext;
            bl.Order.UpdateToManager(order, tmp.ProductId, tmp.AmountOfProduct-1);
            OrderItemList.ItemsSource = null;
            OrderItemList.ItemsSource = bl.Order.GetOrderByID(order.OrderId).OrderItems;
        }

        private void OrderItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

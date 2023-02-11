using BO;
using PLL.ProductWin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL.CartWin
{
    /// <summary>
    /// Interaction logic for MainCartWin.xaml
    /// </summary>
    public partial class MainCartWin : Window
    {
        static BO.Cart NewCart;
        BlApi.IBl? bl = BlApi.Factory.Get();
        int sumOfItems;

        //public static readonly DependencyProperty NewCartDep =
        //    DependencyProperty.Register(nameof(NewCart), typeof(BO.Cart), typeof(MainCartWin));
        //static BO.Cart NewCart { get => (BO.Cart?)GetValue(NewCartDep); set => SetValue(NewCartDep, value); }

        public MainCartWin(BO.Cart newCart)
        {
            InitializeComponent();
            NewCart = new Cart();
            NewCart.OrderItems = new List<OrderItem?>();
            NewCart=newCart;
            sumOfItems = newCart.OrderItems.Count();
            if (NewCart.OrderItems.Count() == 0)
            {
                OrderItemList.Visibility = Visibility.Hidden;
                MakeOrder.Visibility=Visibility.Hidden;
            }
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BO.OrderItem tmp = new OrderItem();
            tmp= (BO.OrderItem)((Button)sender).DataContext;
            NewCart.OrderItems.Remove(tmp);
            OrderItemList.ItemsSource = null;
            OrderItemList.ItemsSource = NewCart.OrderItems;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem tmp = new OrderItem();
                tmp = (BO.OrderItem)((Button)sender).DataContext;
                NewCart = bl.Cart.UpdateAmount(NewCart, tmp.ProductId, tmp.AmountOfProduct + 1);
                OrderItemList.ItemsSource = null;
                OrderItemList.ItemsSource = NewCart.OrderItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BO.OrderItem tmp = new OrderItem();
            tmp = (BO.OrderItem)((Button)sender).DataContext;
            NewCart = bl.Cart.UpdateAmount(NewCart, tmp.ProductId, tmp.AmountOfProduct - 1);
            OrderItemList.ItemsSource = null;
            OrderItemList.ItemsSource = NewCart.OrderItems;
        }

    }
}

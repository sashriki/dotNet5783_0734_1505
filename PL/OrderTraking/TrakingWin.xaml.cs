using BO;
using PL.OrderWin;
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

namespace PL.OrderTraking
{
    /// <summary>
    /// Interaction logic for TrakingWin.xaml
    /// </summary>
    public partial class TrakingWin : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty orderTrackingDep =
            DependencyProperty.Register(nameof(orderTracking), typeof(BO.OrderTracking), typeof(TrakingWin));

        BO.OrderTracking orderTracking
        {
            get => (BO.OrderTracking)GetValue(orderTrackingDep);
            set => SetValue(orderTrackingDep, value);
        }

        public static readonly DependencyProperty orderDep =
            DependencyProperty.Register(nameof(order), typeof(BO.Order), typeof(TrakingWin));

        BO.Order order
        {
            get => (BO.Order)GetValue(orderDep);
            set => SetValue(orderDep, value);
        }

        public TrakingWin()
        {
            InitializeComponent();
            orderTracking=new BO.OrderTracking();
            order=new BO.Order();
        }

        private void Txt_OrderId_TextChanged(object sender, TextChangedEventArgs e)
        { }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { 
            orderTracking = bl.Order.OrderTracking(int.Parse(Txt_OrderId.Text));
            order = bl.Order.GetOrderByID(int.Parse(Txt_OrderId.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Txt_OrderId.Text = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}

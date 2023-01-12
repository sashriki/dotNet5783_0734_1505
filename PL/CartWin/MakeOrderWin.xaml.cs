using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MakeOrderWin.xaml
    /// </summary>
    public partial class MakeOrderWin : Window
    {
        static BO.Cart NewCart;
        BlApi.IBl? bl = BlApi.Factory.Get();

        public MakeOrderWin(BO.Cart newCart)
        {
            InitializeComponent();
            NewCart=new BO.Cart();
            NewCart.OrderItems=new List<BO.OrderItem?>();
            NewCart = newCart;
        }

        private void Txt_name_TextChanged(object sender, TextChangedEventArgs e)
        { }
        private void Txt_email_TextChanged(object sender, TextChangedEventArgs e)
        { }
        private void Txt_address_TextChanged(object sender, TextChangedEventArgs e)
        { }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            NewCart.CustomerName = Txt_name.Text;
            NewCart.CustomerEmail=Txt_email.Text;
            NewCart.CustomerAdress = Txt_address.Text;
            try
            {
                int OrderId =bl.Cart.OrderConfirmation(NewCart);
                new OrderConfWin(OrderId).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Txt_name.Text = "";
                Txt_email.Text = "";
                Txt_address.Text = "";
            }
        }
    }
}

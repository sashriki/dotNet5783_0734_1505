using BO;
using PL.CartWin;
using PLL.ProductWin;
using System;
using System.Collections.Generic;
using System.Globalization;
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



namespace PL.ProductWin
{
    /// <summary>
    /// Interaction logic for ProductItemWin.xaml
    /// </summary>

    public partial class ProductItemWin : Window
    {
        static BO.Cart NewCart;
        BlApi.IBl? bl = BlApi.Factory.Get();
        public static readonly DependencyProperty ProductItemDep = DependencyProperty.Register(nameof(ProductItem), typeof(BO.ProductItem), typeof(ProductItemWin));
        BO.ProductItem? ProductItem { get => (BO.ProductItem?)GetValue(ProductItemDep); set => SetValue(ProductItemDep, value); }
        public ProductItemWin(BO.ProductItem productItem, BO.Cart cart)
        {
            InitializeComponent();
            NewCart = cart;
            ProductItem=new BO.ProductItem();
            ProductItem = productItem;
            if (productItem.AmmountInCart == 0)
                AmmountInCartTextBox.Text = "";         
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //לשאול את דורון- האם שורות 49 50 צריכות להיות פה?
            //if (AmmountInCartTextBox.Text != "")
            //    ProductItem.AmmountInCart = int.Parse(AmmountInCartTextBox.Text);
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            NewCart = bl.Cart.AddProductToCart(NewCart, ProductItem.ProductId);
            new MainProductWin(EnumWin.ClientOrManager.client).Show();
            this.Close();
        }
    }
}

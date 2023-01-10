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
        { }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if(!NewCart.OrderItems.Exists(x => x.ProductId == ProductItem.ProductId))
                NewCart = bl.Cart.AddProductToCart(NewCart, ProductItem.ProductId);

            if (int.Parse(AmmountInCartTextBox.Text)>1)
                NewCart = bl.Cart.UpdateAmount(NewCart, ProductItem.ProductId, int.Parse(AmmountInCartTextBox.Text));

            new MainProductWin(NewCart).Show();
            this.Close();
        }
    }
}

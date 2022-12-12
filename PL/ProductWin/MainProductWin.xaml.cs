using System;
using System.Windows;
using System.Windows.Controls;
using BlApi;
using BlImplementation;
using BO;
namespace PLL.ProductWin
{
    /// <summary>
    /// Interaction logic for MainProductWin.xaml
    /// </summary>
    public partial class MainProductWin : Window
    {
        private IBl bl; 
        public MainProductWin()
        {
            InitializeComponent();
            bl = new Bl();
            ProductListview.ItemsSource = bl.Product.getAllProducts();
            SelectionProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category option = (Category)SelectionProduct.SelectedItem;
            if(option != BO.Category.All)
                ProductListview.ItemsSource= bl.Product.getAllProducts(x=> x.Category== option);
            else
                ProductListview.ItemsSource = bl.Product.getAllProducts();
        }
        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

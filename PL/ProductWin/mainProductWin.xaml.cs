using System.Windows;
using System.Windows.Controls;
using BlApi;
using BO;
using BlImplementation;

namespace PL.ProductWin
{
    /// <summary>
    /// Interaction logic for mainProductWin.xaml
    /// </summary>
    public partial class mainProductWin : Window
    {
        private IBl bl; 
        public mainProductWin()
        {
            InitializeComponent();
            bl = new Bl();
            ProductListview.ItemsSource = bl.Product.getAllProducts();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}

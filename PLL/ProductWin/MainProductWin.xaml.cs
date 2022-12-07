using System.Windows;
using System.Windows.Controls;

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
            ProductListview.ItemsSource = bl.Products.getAllProducts();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}

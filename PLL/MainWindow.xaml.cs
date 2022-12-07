using PLL.ProductWin;
using System.Windows;

namespace PLL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl;
        public MainWindow()
        {
            InitializeComponent();
            bl = new Bl();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainProductWin().Show();
        }
    }
}

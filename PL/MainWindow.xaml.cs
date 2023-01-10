using System.Windows;
using PLL.ProductWin;
using PL.manager;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void manager_Click(object sender, RoutedEventArgs e)
        {
            new MainWinManager().Show();
            this.Close();
        }

        private void client_Click(object sender, RoutedEventArgs e)
        {
            new MainProductWin(EnumWin.ClientOrManager.client).Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        { }
    }
}

using System.Windows;
using PLL.ProductWin;
using PL.manager;
using PL.OrderTraking;
using PL.Simulator;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool _simulatorClick
        {
            get { return (bool)GetValue(_simulatorClickProperty); }
            set { SetValue(_simulatorClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _simulatorClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _simulatorClickProperty =
            DependencyProperty.Register("_simulatorClick", typeof(bool), typeof(MainWindow));


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

        private void SimulatorOn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        { }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new TrakingWin().Show();
            this.Close();
        }

        private void Simulator(object sender, RoutedEventArgs e)
        {
            _simulatorClick = false;
            new SimulatorWin(()=> _simulatorClick= !_simulatorClick).Show();
        }
    }
}

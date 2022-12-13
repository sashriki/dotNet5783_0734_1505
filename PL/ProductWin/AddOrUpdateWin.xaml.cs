using BlApi;
using BlImplementation;
using PLL.ProductWin;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace PL.ProductWin
{
    /// <summary>
    /// Interaction logic for AddOrUpdateWin.xaml
    /// </summary>
    public partial class AddOrUpdateWin : Window
    {

        private IBl bl = new Bl();
        BO.Product product;
        public enum state { Add, Update };
        state State;
        int HowManyTimesWeCalledToTxt_TextChangedFunction = 0;

        public AddOrUpdateWin()  //add
        {
            InitializeComponent();
            Categories.ItemsSource = Enum.GetValues(typeof(BO.Category));
            State = state.Add;
        }

        public AddOrUpdateWin(BO.ProductForList selected_item)  //update
        {
            InitializeComponent();
            Categories.ItemsSource = Enum.GetValues(typeof(BO.Category));
            State = state.Update;
            TxtID.Text = $"{selected_item.ProductId}";
            TxtName.Text = selected_item.ProductName;
            TxtPrice.Text = $"{selected_item.Price}";
            Categories.SelectedItem = selected_item.Category;
            BO.Product prod = bl.Product.getByIdToMannage(selected_item.ProductId);
            TxtInStock.Text = $"{prod.AmmountInStock}";
            TxtID.IsReadOnly = true;
        }


        private void AddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            product = new BO.Product();
            product.ProductId = int.Parse(TxtID.Text);
            product.ProductName = TxtName.Text;
            product.ProductPrice = float.Parse(TxtPrice.Text);
            product.AmmountInStock = int.Parse(TxtInStock.Text);
            product.ProductCategory = (BO.Category)Categories.SelectedItem;
            try
            {
                if (State == state.Update)
                    bl.Product.updateProduct(product);
                else
                    bl.Product.addProduct(product);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
            permissionScreen();
        }

        private void permissionScreen()
        {
            TxtID.Visibility = Visibility.Hidden;
            TxtName.Visibility = Visibility.Hidden;
            TxtPrice.Visibility = Visibility.Hidden;
            TxtInStock.Visibility = Visibility.Hidden;
            Categories.Visibility = Visibility.Hidden;
            IdText.Visibility = Visibility.Hidden;
            InStockText.Visibility = Visibility.Hidden;
            CategoryText.Visibility = Visibility.Hidden;
            NameText.Visibility = Visibility.Hidden;
            PriceText.Visibility = Visibility.Hidden;
            AddOrUpdate.Visibility = Visibility.Hidden;
            Return.Visibility = Visibility.Visible;
            Termination.Visibility = Visibility.Visible;
        }

        private void Categories_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        { }

        private void DifitsOnlyID(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            if (regex.IsMatch(e.Text))
            {
                Error_massageID.Visibility = Visibility.Visible;
                e.Handled = true;
            }
            else
            {
                e.Handled = regex.IsMatch(e.Text);
                Error_massageID.Visibility = Visibility.Hidden;
            }
        }

        private void DifitsOnlyAmount(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            if (regex.IsMatch(e.Text))
            {
                Error_massageAmount.Visibility = Visibility.Visible;
                e.Handled = true;
            }
            else
            {
                e.Handled = regex.IsMatch(e.Text);
                Error_massageAmount.Visibility = Visibility.Hidden;
            }
        }

        private void DifitsOnlyPrice(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9.]+");
            if (regex.IsMatch(e.Text))
            {
                Error_massagePrice1.Visibility = Visibility.Visible;
                e.Handled = true;
            }
            else
            {
                Error_massagePrice1.Visibility = Visibility.Hidden;
                e.Handled = regex.IsMatch(e.Text);
            }

            int index = TxtPrice.Text.IndexOf(".");
            if (index != -1 && e.Text == ".")
            {
                Error_massagePrice2.Visibility = Visibility.Visible;
                e.Handled = true;
            }
            else
            {
               Error_massagePrice2.Visibility = Visibility.Hidden; 
               e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Txt_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            HowManyTimesWeCalledToTxt_TextChangedFunction++;
            if (TxtID.Text != "" && TxtInStock.Text != "" && TxtName.Text != "" && TxtPrice.Text != "" && Categories.SelectedItem!= null && 
                ((State == state.Update && HowManyTimesWeCalledToTxt_TextChangedFunction>4)||(State == state.Add)))
                AddOrUpdate.IsEnabled = true;

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new MainProductWin().Show();
            this.Close();
        }
    }
}

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
        /// <summary>
        /// Parameterless constructor for adding a product
        /// </summary>
        public AddOrUpdateWin()  
        {
            InitializeComponent();
            Categories.ItemsSource = Enum.GetValues(typeof(BO.Category));
            State = state.Add;
        }

        /// <summary>
        /// constructor that receives a parameter to update a product
        /// </summary>
        /// <param name="selected_item"></param>
        public AddOrUpdateWin(BO.ProductForList selected_item) 
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                MessageBox.Show(ex.ToString());    
            }
            permissionScreen();
        }
        /// <summary>
        /// A function responsible for hiding/revealing controls
        /// </summary>
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
        /// <summary>
        /// Function to check input integrity for ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// A function to check input integrity for a amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// A function to check the validity of an input for a price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// A function to fill in the product fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            HowManyTimesWeCalledToTxt_TextChangedFunction++;
            if (TxtID.Text != "" && TxtInStock.Text != "" && TxtName.Text != "" && TxtPrice.Text != "" && Categories.SelectedItem!= null && 
                ((State == state.Update && HowManyTimesWeCalledToTxt_TextChangedFunction>4)||(State == state.Add)))
                AddOrUpdate.IsEnabled = true;
            else
                AddOrUpdate.IsEnabled = false;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new MainProductWin().Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainProductWin().Show();
            this.Close();
        }
    }
}

using PLL.ProductWin;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;


namespace PL.ProductWin
{
    /// <summary>
    /// Interaction logic for AddOrUpdateWin.xaml
    /// </summary>
    public partial class AddOrUpdateWin : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();

        //BO.Product product;
        EnumWin.state State;
        int HowManyTimesWeCalledToTxt_TextChangedFunction = 0;

        public static readonly DependencyProperty ProductDep =
         DependencyProperty.Register(nameof(product), typeof(BO.Product), typeof(AddOrUpdateWin));
        BO.Product? product { get => (BO.Product?)GetValue(ProductDep); set => SetValue(ProductDep, value); }


        /// <summary>
        /// Parameterless constructor for adding a product
        /// </summary>
        public AddOrUpdateWin()  
        {
            InitializeComponent();
            Categories.ItemsSource = System.Enum.GetValues(typeof(BO.Category));
            State = EnumWin.state.Add;
            TxtID.IsReadOnly = false;
        }

        /// <summary>
        /// constructor that receives a parameter to update a product
        /// </summary>
        /// <param name="selected_item"></param>
        public AddOrUpdateWin(BO.ProductForList selected_item) 
        {
            InitializeComponent();
            Categories.ItemsSource = Enum.GetValues(typeof(BO.Category));
            State = EnumWin.state.Update;
            product = bl.Product.getByIdToMannage(selected_item.ProductId);
            Categories.SelectedItem = product.ProductCategory;
            TxtID.IsReadOnly = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (State == EnumWin.state.Update)
                    bl.Product.updateProduct(product);
                else
                    bl.Product.addProduct(product);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
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
            Termination.Visibility = Visibility.Visible;
        }
        private void Categories_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        { }
  
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
                ((State == EnumWin.state.Update && HowManyTimesWeCalledToTxt_TextChangedFunction>4)||(State == EnumWin.state.Add)))
                AddOrUpdate.IsEnabled = true;
            else
                AddOrUpdate.IsEnabled = false;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            InvalidInput.numOfConvert = 0;
            new MainProductWin(EnumWin.ClientOrManager.manager).Show();
            this.Close();
        }
    }
}


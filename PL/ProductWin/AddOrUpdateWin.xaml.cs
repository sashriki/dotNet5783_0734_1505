using PLL.ProductWin;
using System;
using System.Reflection.Metadata;
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

        public EnumWin.state State
        {
            get { return (EnumWin.state)GetValue(StateDep); }
            set { SetValue(StateDep, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateDep =
            DependencyProperty.Register(nameof(State), typeof(EnumWin.state), typeof(AddOrUpdateWin));



        int HowManyTimesWeCalledToTxt_TextChangedFunction = 0;

        public static readonly DependencyProperty ProductDep =
            DependencyProperty.Register(nameof(product), typeof(BO.Product), typeof(AddOrUpdateWin));
        BO.Product? product { get => (BO.Product?)GetValue(ProductDep); set => SetValue(ProductDep, value); }


        /// <summary>
        /// Parameterless constructor for adding a product
        /// </summary>
        public AddOrUpdateWin()
        {
            State = EnumWin.state.Add;
            InitializeComponent();
            product = new BO.Product();
            Categories.ItemsSource = System.Enum.GetValues(typeof(BO.Category));
            TxtID.IsReadOnly = false;
        }

        /// <summary>
        /// constructor that receives a parameter to update a product
        /// </summary>
        /// <param name="selected_item"></param>
        public AddOrUpdateWin(BO.ProductForList selected_item) 
        {
            InitializeComponent(); 
            product = new BO.Product();
            Categories.ItemsSource = Enum.GetValues(typeof(BO.Category));
            State = EnumWin.state.Update;
            product = bl.Product.getByIdToMannage(selected_item.ProductId);
            Categories.SelectedItem = product.ProductCategory;
            TxtID.IsReadOnly = true;
        }

        //טוב סבבה אז תחזיא את זה לקודמו?  טעוזבוב, זה לא חשוב, עדיף סימולטור, לא אכפת לי שיראו אפסים
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
                new MainProductWin(EnumWin.ClientOrManager.manager).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }  
            this.Close();
        }
        private void Categories_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            product.ProductCategory = (BO.Category)Categories.SelectedItem;
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


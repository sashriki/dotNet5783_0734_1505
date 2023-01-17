using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BO;
using PL;
using PL.CartWin;
using PL.manager;
using PL.OrderWin;
using PL.ProductWin;
using static PL.EnumWin;

namespace PLL.ProductWin
{
    /// <summary>
    /// Interaction logic for MainProductWin.xaml
    /// </summary>
    public partial class MainProductWin : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private IEnumerable<BO.ProductForList> productsForList;
        private IEnumerable<BO.ProductItem> productsItems;
        ClientOrManager clientOrManager;
        static BO.Cart NewCart;
        private string groupName = "Category";
        PropertyGroupDescription propertyGroupDescription;
        public ICollectionView CollectionViewProductItemList { set; get; }
        public MainProductWin(ClientOrManager x)
        {
            InitializeComponent();

            selectedCategory.Items.Add("All");
            foreach (var item in System.Enum.GetValues(typeof(BO.Category)))
            {
                selectedCategory.Items.Add(item.ToString());
            }
            selectedCategory.SelectedIndex = 0;

            clientOrManager = new ClientOrManager();

            if (x == ClientOrManager.manager)
            {
                clientOrManager = ClientOrManager.manager;
                cart.Visibility = Visibility.Hidden;
                productsForList = bl.Product.getAllProducts()!;
                ProductListview.ItemsSource = productsForList;
                CollectionViewProductItemList = CollectionViewSource.GetDefaultView(productsForList);
            }
            else
            {
                NewCart = new BO.Cart();
                NewCart.OrderItems = new List<OrderItem?>();
                clientOrManager = ClientOrManager.client;
                Add.Visibility = Visibility.Hidden;
                productsItems = bl.Product.GetAllToCastumer(NewCart);
                ProductListview.ItemsSource = productsItems;    
                CollectionViewProductItemList = CollectionViewSource.GetDefaultView(productsItems);
            }

            propertyGroupDescription = new PropertyGroupDescription(groupName);
            CollectionViewProductItemList.GroupDescriptions.Add(propertyGroupDescription);
        }

        public MainProductWin(BO.Cart newCart)
        {
            InitializeComponent();
            NewCart = new BO.Cart();
            NewCart.OrderItems = new List<OrderItem?>(); 
            NewCart=newCart;
            clientOrManager = ClientOrManager.client;
            Add.Visibility = Visibility.Hidden;
            productsItems = bl.Product.GetAllToCastumer(NewCart);
            ProductListview.ItemsSource = productsItems;
        }

        /// <summary>
        /// A function to filter a list by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectedCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedCategory.SelectedItem is not null)
            {
                string _selectedCategory = (string)selectedCategory.SelectedItem;
                ProductListview.ItemsSource = _selectedCategory == "All" ? productsForList :
                 bl.Product.GetAllByCondition(p => p.Category.ToString() == _selectedCategory, productsForList);
            }
        }
        /// <summary>
        /// A function that opens a window to add a product to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new AddOrUpdateWin().Show();
            this.Close();
        }
        /// <summary>
        /// A function that opens a window to update a product in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(clientOrManager== ClientOrManager.manager)
                new AddOrUpdateWin((BO.ProductForList)ProductListview.SelectedItem).Show();
            else
                new ProductItemWin((BO.ProductItem)ProductListview.SelectedItem,NewCart).Show();
            this.Close();
        }
        /// <summary>
        /// Function to close a window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// A function to search for a product in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (clientOrManager==EnumWin.ClientOrManager.manager)
            {
                if (Search.Text != "")
                {
                    string x = Search.Text;
                    ProductListview.ItemsSource = bl.Product.GetAllByCondition(p => p.ProductName.StartsWith(x), productsForList);
                }
            }
            else
            {
                if (Search.Text != "")
                {
                    string x = Search.Text;
                    ProductListview.ItemsSource = bl.Product.GetAllByConditionToCastumer(p => p.ProductName.StartsWith(x), productsItems);
                }
            }
        }
        /// <summary>
        /// Function to return to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (clientOrManager == ClientOrManager.client)
                new MainWindow().Show();
            else
                new MainWinManager().Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MainOrderWin(clientOrManager).Show();
            this.Close();
        }

        private void cart_Click(object sender, RoutedEventArgs e)
        {
            new MainCartWin(NewCart).Show();
            this.Close();
        }
    }
}

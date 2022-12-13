using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BlApi;
using BlImplementation;
using PL.ProductWin;

namespace PLL.ProductWin
{
    /// <summary>
    /// Interaction logic for MainProductWin.xaml
    /// </summary>
    public partial class MainProductWin : Window
    {
        private IBl bl;
         private IEnumerable<BO.ProductForList> productsForList;
        public MainProductWin()
        {
            InitializeComponent();
            bl = new Bl();
            productsForList = bl.Product.getAllProducts()!;
            ProductListview.ItemsSource = productsForList;
            
            selectedCategory.Items.Add("All");
           
            foreach (var item in Enum.GetValues(typeof(BO.Category)))
            {
                selectedCategory.Items.Add(item.ToString());
            }
            selectedCategory.SelectedIndex = 0;
        }

        private void selectedCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedCategory.SelectedItem is not null)
            {
                string _selectedCategory = (string)selectedCategory.SelectedItem;
                ProductListview.ItemsSource = _selectedCategory == "All" ? productsForList :
                 bl.Product.GetAllByCondition(p => p.Category.ToString() == _selectedCategory, productsForList);
            }            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddOrUpdateWin().Show();
        }

        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

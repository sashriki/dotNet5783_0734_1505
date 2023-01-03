﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BO;
using PL;
using PL.CartWin;
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
        ClientOrManager clientOrManager;
        public MainProductWin(EnumWin.ClientOrManager x)
        {
            InitializeComponent();

            productsForList = bl.Product.getAllProducts()!;
                ProductListview.ItemsSource = productsForList;
                selectedCategory.Items.Add("All");

            foreach (var item in System.Enum.GetValues(typeof(BO.Category)))
            {
                selectedCategory.Items.Add(item.ToString());
            }
            selectedCategory.SelectedIndex = 0;

            clientOrManager = new ClientOrManager();

            ProductListview.ItemsSource = productsForList;

            if (x == ClientOrManager.manager)
            {              
                clientOrManager = ClientOrManager.manager;
                cart.Visibility = Visibility.Hidden; 
                orders.Visibility= Visibility.Hidden;
            }
            else
            {
                clientOrManager = ClientOrManager.client;
                Add.Visibility = Visibility.Hidden;
            }
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
            new AddOrUpdateWin((BO.ProductForList)ProductListview.SelectedItem).Show();
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
            if (Search.Text != "")
            {
                string x = Search.Text;
                ProductListview.ItemsSource = bl.Product.GetAllByCondition(p => p.ProductName.StartsWith(x), productsForList);
            }
        }
        /// <summary>
        /// Function to return to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MainOrderWin(clientOrManager).Show();
            this.Close();
        }

        private void cart_Click(object sender, RoutedEventArgs e)
        {
            new MainCartWin().Show();
        }
    }
}

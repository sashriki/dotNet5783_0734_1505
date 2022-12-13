﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using BlApi;
using BlImplementation;
using PLL.ProductWin;

namespace PL
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
        /// <summary>
        /// A function that opens a window for operations on a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainProductWin().Show();
            this.Close();
        }
        /// <summary>
        /// Function to close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
    }
}

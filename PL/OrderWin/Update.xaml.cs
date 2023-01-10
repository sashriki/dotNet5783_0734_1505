using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.OrderWin
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        //BO.OrderForList orderForList;
        BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty OrderForListDep =
            DependencyProperty.Register(nameof(OrderList), typeof(OrderForList), typeof(Update));

        OrderForList OrderList
        {
            get => (OrderForList)GetValue(OrderForListDep);
            set => SetValue(OrderForListDep, value);
        }

        public Update(BO.OrderForList ordForLst)
        {
            InitializeComponent();
            //orderForList = ordForLst;
            status.ItemsSource = System.Enum.GetValues(typeof(BO.OrderStatus));
            status.SelectedIndex = 0;
            OrderList = ordForLst;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

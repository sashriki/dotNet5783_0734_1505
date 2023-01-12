using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace PL.OrderTraking
{
    public class InvalidInput : IValueConverter
    {
        Regex regex = new("[^0-9]+");
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
                => regex.IsMatch((string)value) ? Visibility.Visible : Visibility.Hidden;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvalidInput_ToSearch : IValueConverter
    {
        Regex regex = new("[^0-9]+");
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
                => !regex.IsMatch((string)value) && (string)value != "" ? true : false;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

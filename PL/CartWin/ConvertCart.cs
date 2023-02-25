using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using BO;
using System.Text.RegularExpressions;

namespace PL.CartWin
{
    public class VisibilityOrderConf : IMultiValueConverter  //multy binding
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values.All(v=> v as string  "")
            foreach (var item in values)   //if all is full the button will be visble
                if (item as string == "")
                    return false;
            return true;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (!(value is bool b) || targetTypes.Any(t => !t.IsAssignableFrom(typeof(bool))))
                return null;
            if (b)
                return targetTypes.Select(t => (object)true).ToArray();
            else
                return null;
        }
    }

    public class EmptyCart : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
                return ((IEnumerable<OrderItem>)value).Count() == 0 ? Visibility.Visible : Visibility.Hidden;
            else
                return Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FullCart : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
                return ((IEnumerable<OrderItem>)value).Count() == 0 ? Visibility.Hidden : Visibility.Visible;
            else
                return Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
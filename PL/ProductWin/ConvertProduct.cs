using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ProductWin
{
    public class EmptyToVisable : IValueConverter
    {
        Regex regex = new("[^0-9]+");
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !regex.IsMatch((string)value) && value as string != "" ? true : false;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvalidInput : IValueConverter
    {
        Regex regex = new("[^0-9]+");
        public static int numOfConvert = 0; //איך לעזעאל לבדוק שזה לא הפעם הראשונה שהוא נכנס לפה??????
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            numOfConvert++;
            if (numOfConvert > 3)
                return !regex.IsMatch((string)value) ?  Visibility.Hidden : Visibility.Visible;
            return Visibility.Hidden;                
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvalidInputPrice : IValueConverter
    {
        Regex regex = new("[^0-9.]+");
        public static int numOfConvert = 0; //איך לעזעאל לבדוק שזה לא הפעם הראשונה שהוא נכנס לפה??????
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            numOfConvert++;
            if (numOfConvert > 3)
                return !regex.IsMatch((string)value) ? Visibility.Hidden : Visibility.Visible;
            return Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class VisibilityAddOrApdate : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            for (int i = 0; i < values.Length-1; i++)
              if (values[i] as string == "")
                    return false;
            if ((Category)values[values.Length - 1] == null)
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
}

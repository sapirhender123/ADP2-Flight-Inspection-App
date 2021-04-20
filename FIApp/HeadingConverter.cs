using System;
using System.Globalization;
using System.Windows.Data;

namespace FIApp
{
    // converter for heading binding: changes the spin direction 
    class HeadingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newVal = (double)value;
            return newVal * -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

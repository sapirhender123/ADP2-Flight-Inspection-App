using System;
using System.Globalization;
using System.Windows.Data;

namespace FIApp
{
    class AirSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newVal = (double)value;
            if (newVal <= 40)
            {
                return 15.5;
            }
            else if (newVal >= 240)
            {
                return 345;
            }
            return 15.5 + (((newVal - 40) / 10) * 16.5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}

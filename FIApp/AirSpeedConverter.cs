using System;
using System.Globalization;
using System.Windows.Data;

namespace FIApp
{
    // converter for airspeed binding: converts speed to degrees
    class AirSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newVal = (double)value;
            //display any speed between 0 and 40 as 40
            if (newVal <= 40)
            {
                return 15.5;
            }
            //display any speed above 240 as 240
            else if (newVal >= 240)
            {
                return 345;
            }
            //calculate the suitable degree
            return 15.5 + (((newVal - 40) / 10) * 16.5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}

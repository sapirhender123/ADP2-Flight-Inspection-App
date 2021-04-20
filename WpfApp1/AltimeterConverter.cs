using System;
using System.Globalization;
using System.Windows.Data;

namespace FIApp
{
    class AltimeterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            double newVal = (double)value;
            int newParameter = Int32.Parse((string)parameter);
            if (newVal < 0)
            {
                return 0;
            }
            if (newParameter == 10000)
            {
                if (newVal < 10000)
                {
                    return 0;
                }
                return ((newVal) / 10000) * 36;
            }
            if (newParameter == 1000)
            {
                if (newVal < 1000)
                {
                    return 0;
                }
                return ((newVal % 10000) / 1000) * 36;
            }
            //newParameter is 100
            return ((newVal % 1000) / 100) * 36;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}

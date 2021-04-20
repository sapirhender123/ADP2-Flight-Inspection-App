using System;
using System.Globalization;
using System.Windows.Data;

namespace FIApp
{
    // converter for altimeter binding: converts altitude to degrees
    class AltimeterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newVal = (double)value;
            //the parameter indicates which altimeter dial it is: 100 foot, 1000 foot or 10000 foot
            int newParameter = Int32.Parse((string)parameter);
            //display negative altitude as 0
            if (newVal < 0)
            {
                return 0;
            }
            //10000 dial, moves only if the altitude is higher than 10000 foot
            if (newParameter == 10000)
            {
                if (newVal < 10000)
                {
                    return 0;
                }
                return ((newVal) / 10000) * 36;
            }
            //1000 dial, moves only if the altitude is higher than 1000 foot
            if (newParameter == 1000)
            {
                if (newVal < 1000)
                {
                    return 0;
                }
                return ((newVal % 10000) / 1000) * 36;
            }
            //100 dial
            return ((newVal % 1000) / 100) * 36;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}

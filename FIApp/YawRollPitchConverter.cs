using System;
using System.Globalization;
using System.Windows.Data;

namespace FIApp
{
    /**
     * converter for yaw, roll and pitch binding: converts value to degrees.
     * yaw and roll range is from -180 to 180, pitch range is from -90 to 90.
     */
    class YawRollPitchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newVal = (double)value;
            //the parameter indicates which convertion is needed: 1 for yaw and roll, 2 for pitch 
            int newParameter = Int32.Parse((string)parameter);
            if (newVal > 180)
            {
                return (0.9 * newParameter) * 180;
            }
            if (newVal < -180)
            {
                return (0.9 * newParameter) * -180;
            }
            return (0.9 * newParameter) * newVal;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}

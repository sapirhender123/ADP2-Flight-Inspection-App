using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FIApp
{
    class YawRollPitchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newVal = (double)value;
            int newParameter = Int32.Parse((string)parameter);//1 for yaw and roll, 2 for pitch 

            if (newVal > 180)
            {
                return (0.9*newParameter) * 180;
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

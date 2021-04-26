using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FIApp
{
    /**
    * converter for aileron and elevator binding.
    * aileron and elevator normal values are between -1 and 1.
    * The conversion increases the range in order to emphasize the joystick's movement. 
    */

    class AileronElevatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double newVal = (double)value;
            return newVal * 40 + 125;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Atlantis.Recursos
{
    class CheckBoxValueToParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                if (value is string)
                {
                    return ((string)value) == "1";
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is bool)
            {
                bool valor = (bool)value;
                if (valor)
                {
                    return "1";
                }
            }

            return null;
        }
    }

}

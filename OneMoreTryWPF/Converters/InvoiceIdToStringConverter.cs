using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OneMoreTryWPF.Converters
{
    public class InvoiceIdToStringConverter : IValueConverter
    {
        public object Convert(object id, Type targetType, object parameter, CultureInfo culture)
        {
            return (long)id == 0 ? String.Empty : id.ToString();
        }

        public object ConvertBack(object id, Type targetType, object parameter, CultureInfo culture)
        {
            return String.IsNullOrEmpty(id.ToString()) ? 0 : long.Parse(id.ToString());
        }
    }
}
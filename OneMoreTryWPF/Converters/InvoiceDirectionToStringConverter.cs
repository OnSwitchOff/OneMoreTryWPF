using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OneMoreTryWPF.Converters
{
	public class InvoiceDirectionToStringConverter : IValueConverter
	{
		public object Convert(object direction, Type targetType, object parameter, CultureInfo culture)
		{
			string result = String.Empty;
			switch ((InvoiceService.InvoiceDirection) direction)
			{
				case InvoiceService.InvoiceDirection.INBOUND:
					result = "Входящие";
					break;
				case InvoiceService.InvoiceDirection.OUTBOUND:
					result = "Исходящие";
					break;
				default:
					break;
			}
			return result;
		}

		public object ConvertBack(object direction, Type targetType, object parameter, CultureInfo culture)
		{
			object result = null;
			switch (direction)
			{
				case "Входящие":
					result = InvoiceService.InvoiceDirection.INBOUND;
					break;
				case "Исходящие":
					result = InvoiceService.InvoiceDirection.OUTBOUND;
					break;
				default:
					break;
			}
			return result;
		}
	}	
}

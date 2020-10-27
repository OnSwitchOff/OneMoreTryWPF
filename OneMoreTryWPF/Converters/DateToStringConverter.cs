using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OneMoreTryWPF.Converters
{
	public class DateToStringConverter : IValueConverter
	{
		public object Convert(object status, Type targetType, object parameter, CultureInfo culture)
		{
			return (DateTime)status != new DateTime() ? ((DateTime)status).ToString("dd.MM.yyyy") : String.Empty;
		}

		public object ConvertBack(object dateString, Type targetType, object parameter, CultureInfo culture)
		{
			return String.IsNullOrEmpty(dateString.ToString()) ? new DateTime() : (DateTime)ParseDate(dateString.ToString());
		}

		private static object ParseDate(string innerText)
		{
			try
			{
				string[] tmp = new string[] { };
				int day, month, year;

				tmp = innerText.Split('.');
				day = int.Parse(tmp[0]);
				month = int.Parse(tmp[1]);
				year = int.Parse(tmp[2]);
				return new DateTime(year, month, day);
			}
			catch (Exception)
			{
				return null;
			}

		}
	}
}


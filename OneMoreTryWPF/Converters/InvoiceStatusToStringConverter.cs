using OneMoreTryWPF.InvoiceService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OneMoreTryWPF.Converters
{
	public class InvoiceStatusToStringConverter : IValueConverter
	{
		public object Convert(object status, Type targetType, object parameter, CultureInfo culture)
		{
			string result = String.Empty;
			switch ((InvoiceStatus)status)
			{
				case InvoiceStatus.CREATED:
					result = "Созданный";
					break;
				case InvoiceStatus.DELIVERED:
					result = "Доставленный";
					break;
				case InvoiceStatus.CANCELED:
					result = "Аннулированный";
					break;
				case InvoiceStatus.CANCELED_BY_OGD:
					result = "Аннулирован ИС ЭСФ для отнесения в зачет и на вычеты";
					break;
				case InvoiceStatus.CANCELED_BY_SNT_DECLINE:
					result = "Аннулирован.....";
					break;
				case InvoiceStatus.CANCELED_BY_SNT_REVOKE:
					result = "Аннулирован.....";
					break;
				case InvoiceStatus.REVOKED:
					result = "Отозванный";
					break;
				case InvoiceStatus.IMPORTED:
					result = "Импортированный";
					break;
				case InvoiceStatus.DRAFT:
					result = "Черновик";
					break;
				case InvoiceStatus.FAILED:
					result = "Ошибочный";
					break;
				case InvoiceStatus.DELETED:
					result = "Удаленный";
					break;
				case InvoiceStatus.DECLINED:
					result = "Отклоненный";
					break;
			}
			return result;
		}

		public object ConvertBack(object status, Type targetType, object parameter, CultureInfo culture)
		{
			object result = null;
			switch (status)
			{
				case "Созданный":
					result = InvoiceStatus.CREATED;
					break;
				case "Доставленный":
					result = InvoiceStatus.DELIVERED;
					break;
				case "Аннулированный":
					result = InvoiceStatus.CANCELED;
					break;
				case "Аннулирован ИС ЭСФ для отнесения в зачет и на вычеты":
					result = InvoiceStatus.CANCELED_BY_OGD;
					break;
				case "Отозванный":
					result = InvoiceStatus.REVOKED;
					break;
				case "Импортированный":
					result = InvoiceStatus.IMPORTED;
					break;
				case "Черновик":
					result = InvoiceStatus.DRAFT;
					break;
				case "Ошибочный":
					result = InvoiceStatus.FAILED;
					break;
				case "Удаленный":
					result = InvoiceStatus.DELETED;
					break;
				case "Отклоненный":
					result = InvoiceStatus.DECLINED;
					break;
				default:
					break;
			}
			return result;
		}
	}
}
using OneMoreTryWPF.ENUMs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OneMoreTryWPF.Converters
{
	class CustomerStatusToStringConverter : IValueConverter
	{
		public object Convert(object status, Type targetType, object parameter, CultureInfo culture)
		{
			string result = String.Empty;
			switch ((CustomerType)status)
			{
				case CustomerType.COMMITTENT:
					result = "Комитент";
					break;
				case CustomerType.BROKER:
					result = "Комиссионер";
					break;
				case CustomerType.LESSEE:
					result = "Лизингополучатель";
					break;
				case CustomerType.JOINT_ACTIVITY_PARTICIPANT:
					result = "Участник договора совместной деятельности";
					break;
				case CustomerType.PUBLIC_OFFICE:
					result = "Государственное учреждение";
					break;
				case CustomerType.NONRESIDENT:
					result = "Нерезидент";
					break;
				case CustomerType.SHARING_AGREEMENT_PARTICIPANT:
					result = "Участник СРП или сделки, заключенной в рамках СРП";
					break;
				case CustomerType.PRINCIPAL:
					result = "Доверитель";
					break;
				case CustomerType.RETAIL:
					result = "Розничная реализация";
					break;
				case CustomerType.INDIVIDUAL:
					result = "Физическое лицо";
					break;
				default:
					break;
			}
			return result;
		}

		public object ConvertBack(object status, Type targetType, object parameter, CultureInfo culture)
		{
			object result = null;
			switch (status)
			{
				case "Комитент":
					result = CustomerType.COMMITTENT;
					break;
				case "Комиссионер":
					result = CustomerType.BROKER;
					break;
				case "Лизингополучатель":
					result = CustomerType.LESSEE;
					break;
				case "Участник договора совместной деятельности":
					result = CustomerType.JOINT_ACTIVITY_PARTICIPANT;
					break;
				case "Государственное учреждение":
					result = CustomerType.PUBLIC_OFFICE;
					break;
				case "Нерезидент":
					result = CustomerType.NONRESIDENT;
					break;
				case "Участник СРП или сделки, заключенной в рамках СРП":
					result = CustomerType.SHARING_AGREEMENT_PARTICIPANT;
					break;
				case "Доверитель":
					result = CustomerType.PRINCIPAL;
					break;
				case "Розничная реализация":
					result = CustomerType.RETAIL;
					break;
				case "Физическое лицо":
					result = CustomerType.INDIVIDUAL;
					break;
				default:
					break;
			}
			return result;
		}
	}
}

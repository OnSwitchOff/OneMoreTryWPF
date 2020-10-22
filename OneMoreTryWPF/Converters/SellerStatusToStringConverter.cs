using OneMoreTryWPF.ENUMs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OneMoreTryWPF.Converters
{
	public class SellerStatusToStringConverter : IValueConverter
    {
		public object Convert(object status, Type targetType, object parameter, CultureInfo culture)
		{
			string result = String.Empty;
			switch ((SellerType)status)
			{
				case SellerType.COMMITTENT:
					result = "Комитент";
					break;
				case SellerType.BROKER:
					result = "Комиссионер";
					break;
				case SellerType.FORWARDER:
					result = "Экспедитор";
					break;
				case SellerType.LESSOR:
					result = "Лизингодатель";
					break;
				case SellerType.JOINT_ACTIVITY_PARTICIPANT:
					result = "Участник договора совместной деятельности";
					break;
				case SellerType.SHARING_AGREEMENT_PARTICIPANT:
					result = "Участник СРП или сделки, заключенной в рамках СРП";
					break;
				case SellerType.EXPORTER:
					result = "Экспортёр";
					break;
				case SellerType.TRANSPORTER:
					result = "Международный перевозчик";
					break;
				case SellerType.PRINCIPAL:
					result = "Доверитель";
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
					result = SellerType.COMMITTENT;
					break;
				case "Комиссионер":
					result = SellerType.BROKER;
					break;
				case "Экспедитор" :
					result = SellerType.FORWARDER;
					break;
				case "Лизингодатель":
					result = SellerType.LESSOR;
					break;
				case "Участник договора совместной деятельности":
					result = SellerType.JOINT_ACTIVITY_PARTICIPANT;
					break;
				case "Участник СРП или сделки, заключенной в рамках СРП":
					result = SellerType.SHARING_AGREEMENT_PARTICIPANT ;
					break;
				case "Экспортёр":
					result = SellerType.EXPORTER;
					break;
				case "Международный перевозчик":
					result = SellerType.TRANSPORTER;
					break;
				case "Доверитель":
					result = SellerType.PRINCIPAL;
					break;
				default:
					break;
			}
			return result;
		}
    }
}

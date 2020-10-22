using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class PublicOffice:INotifyPropertyChanged
	{
		//Реквизиты государственного учреждения (C1)
		//default="KKMFKZ2A"
		private string Bik = "KKMFKZ2A";
		public string bik
		{
			get { return Bik; }
			set
			{
				Bik = value;
				OnPropertyChanged("bik");
			}
		}

		//ИИК (C1 21)
		//pattern value="[0-9A-Z]{20}
		public string Iik;
		public string iik
		{
			get { return Iik; }
			set
			{
				Iik = value;
				OnPropertyChanged("iik");
			}
		}

		//Назначение платежа (C1 23)
		//pattern value = "[^\s:][^:\n\r\t]*"
		private string PayPurpose;
		public string payPurpose
		{
			get { return PayPurpose; }
			set
			{
				PayPurpose= value;
				OnPropertyChanged("payPurpose");
			}
		}

		//Код товаров (работ, услуг) (C1 22)
		public string ProductCode;
		public string productCode
		{
			get { return ProductCode; }
			set
			{
				ProductCode = value;
				OnPropertyChanged("productCode");
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public PublicOffice() { }
	}
}

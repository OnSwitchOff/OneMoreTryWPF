using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	//Реквизиты грузополучателя (D 26)

	public class ConsigneeV2:INotifyPropertyChanged
	{
		//Адрес (D 26.3)+
		private string Address;
		public string address
		{
			get { return Address; }
			set
			{
				Address = value;
				OnPropertyChanged("address");
			}
		}

		//Код страны(D 26.4)-
		private string CountryCode = "KZ";
		public string countryCode
		{
			get { return CountryCode; }
			set
			{
				CountryCode = value;
				OnPropertyChanged("countryCode");
			}
		}

		//Наименование (D 26.2)+
		private string Name;
		public string name
		{
			get { return Name; }
			set
			{
				Name = value;
				OnPropertyChanged("name");
			}
		}

		//ИИН/БИН (D 26.1)+
		private string Tin;
		public string tin
		{
			get { return Tin; }
			set
			{
				Tin = value;
				OnPropertyChanged("tin");
			}
		}

		public ConsigneeV2() { }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

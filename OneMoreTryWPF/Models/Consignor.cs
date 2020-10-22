using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	//Реквизиты грузоотправителя(D 25)
	public class Consignor:INotifyPropertyChanged
	{
		//Адрес(D 25.3)
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

		//Наименование (D 25.2)
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

		//ИИН/БИН (D 25.1)
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

		public Consignor() { }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

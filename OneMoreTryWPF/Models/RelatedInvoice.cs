using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{	
	//Служит для связки исправленного/дополнительного ЭСФ с основным
	public class RelatedInvoice:INotifyPropertyChanged
	{
		//Дата выписки ЭСФ (A 4.1, A 5.1)
		private DateTime Date;
		public string date
		{
			get { return this.Date.ToString("dd.MM.yyyy"); }
			set
			{
				this.Date = DateTime.Parse(value);
				OnPropertyChanged("date");
			}
		}

		//Исходящий номер ЭСФ в бухгалтерии отправителя (A 4.2, A 5.2)
		//pattern value="[0-9]{1,30}
		private string Num;
		public string num
		{
			get { return Num; }
			set
			{
				Num = value;
				OnPropertyChanged("num");
			}
		}

		//Регистрационный номер ЭСФ на которую ссылается данная ЭСФ
		private string RegistrationNumber;
		public string registrationNumber
		{
			get { return RegistrationNumber; }
			set
			{
				RegistrationNumber = value;
				OnPropertyChanged("registrationNumber");
			}
		}

		public RelatedInvoice() { }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}	
}

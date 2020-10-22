using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class DeliveryTermV2:INotifyPropertyChanged
	{
		//Дата договора(контракт) на поставку товаров (работ, услуг) (E 27.4)+
		private DateTime ContractDate;
		public string contractDate
		{
			get { return this.ContractDate.ToString("dd.MM.yyyy"); }
			set 
			{ 
				this.ContractDate = DateTime.Parse(value);
				OnPropertyChanged("contractDate");
			}
		}



		//Номер договора(контракт) на поставку товаров (работ, услуг) (E 27.3)+
		private string ContractNum;
		public string contractNum
		{
			get { return ContractNum; }
			set
			{
				ContractNum = value;
				OnPropertyChanged("contractNum");
			}
		}

		//Условия поставки(E 31.1)-
		private string DeliveryConditionCode;
		public string deliveryConditionCode
		{
			get { return DeliveryConditionCode; }
			set
			{
				DeliveryConditionCode = value;
				OnPropertyChanged("deliveryConditionCode");
			}
		}

		//Пункт назначения поставляемых товаров (работ, услуг) (E 31)+
		private string Destination;
		public string destination
		{
			get { return Destination; }
			set
			{
				Destination = value;
				OnPropertyChanged("destination");
			}
		}

		//Договор/без договора(E 27.1 - true, E27.2 - false)
		private bool HasContract = false;
		public bool hasContract
		{
			get { return HasContract; }
			set
			{
				HasContract = value;
				OnPropertyChanged("hasContract");
			}
		}

		//Условия оплаты по договору (E 28)+
		private string Term;
		public string term
		{
			get { return Term; }
			set
			{
				Term = value;
				OnPropertyChanged("term");
			}
		}

		//Способ отправления (E 29)
		private string TransportTypeCode;
		public string transportTypeCode
		{
			get { return TransportTypeCode; }
			set
			{
				TransportTypeCode = value;
				OnPropertyChanged("transportTypeCode");
			}
		}

		//Номер доверенности на поставку товаров (работ, услуг) (E 30.1)+
		private string Warrant;
		public string warrant
		{
			get { return Warrant; }
			set
			{
				Warrant = value;
				OnPropertyChanged("warrant");
			}
		}

		//Дата доверенности на поставку товаров(работ, услуг) (E 30.2)+
		private DateTime WarrantDate;
		public string warrantDate
		{
			get { return WarrantDate.ToString("dd.MM.yyyy"); }
			set 
			{
				this.WarrantDate = DateTime.Parse(value);
				OnPropertyChanged("warrantDate");
			}
		}

		public DeliveryTermV2() { }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

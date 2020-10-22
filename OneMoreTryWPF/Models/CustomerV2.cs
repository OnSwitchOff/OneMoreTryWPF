using OneMoreTryWPF.Facades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OneMoreTryWPF.Models
{
	public class CustomerV2 : INotifyPropertyChanged
	{
		//Адрес(C 18)+
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

		//БИН филиала, выписавшего ЭСФ за голову
		private string BranchTin;
		public string branchTin
		{
			get { return BranchTin; }
			set
			{
				BranchTin = value;
				OnPropertyChanged("branchTin");
			}
		}

		//Код страны получателя. Обязательно заполняется если установлен статус CustomerType.NONRESIDENT и SellerType.EXPORTER (C 18.1)+
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

		//Наименование получателя (C 17)+
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

		//БИН реорганизованного лица (C 16.1)-
		private string ReorganizedTin;
		public string reorganizedTin
		{
			get { return ReorganizedTin; }
			set
			{
				ReorganizedTin = value;
				OnPropertyChanged("reorganizedTin");
			}
		}

		//Доля участия (С 17.1)
		//fractionDigits value="6", totalDigits value="18"
		private float? ShareParticipation;
		public float? shareParticipation
		{
			get { return ShareParticipation; }
			set
			{
				ShareParticipation = value;
				OnPropertyChanged("shareParticipation");
			}
		}

		//Категории получателя (С 20)+
		[XmlArrayItem(ElementName = "status")]
		private ObservableCollection<UserStatus> Statuses;
		public ObservableCollection<UserStatus> statuses
		{
			get { return Statuses; }
			set
			{
				Statuses = value;
				OnPropertyChanged("statuses");
			}
		}

		//ИИН/БИН. Может отсутствовать если установлен статус CustomerType.NONRESIDENT (C 16)+
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

		//Дополнительные сведения(C 19)+
		private string Trailer;
		public string trailer
		{
			get { return Trailer; }
			set
			{
				Trailer = value;
				OnPropertyChanged("trailer");
			}
		}

		public CustomerV2()
		{
			statuses = SessionDataManagerFacade.GetRandomCustomerStatuses();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

using OneMoreTryWPF.ENUMs;
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
	public class SellerV2 : INotifyPropertyChanged
	{
		//Адрес (B 8)+
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

		//Наименование банка(B1 15)+
		private string Bank;
		public string bank
		{
			get { return Bank; }
			set
			{
				Bank = value;
				OnPropertyChanged("bank");
			}
		}

		//БИК (B1 14)+
		//pattern value="[0-9A-Z]{8}"
		private string Bik;
		public string bik
		{
			get { return Bik; }
			set
			{
				Bik = value;
				OnPropertyChanged("bik");
			}
		}

		//БИН филиала, выписавшего ЭСФ за голову-
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

		//Номер cвидетельства НДС (B 9.2)+
		private string CertificateNum;
		public string certificateNum
		{
			get { return CertificateNum; }
			set
			{
				CertificateNum = value;
				OnPropertyChanged("certificateNum");
			}
		}


		//Серия cвидетельства НДС+
		private string CertificateSeries;
		public string certificateSeries
		{
			get { return CertificateSeries; }
			set
			{
				CertificateSeries = value;
				OnPropertyChanged("certificateSeries");
			}
		}

		//Расчетный счет (B1 13)+
		//pattern value="[0-9A-Z]{20}
		private string Iik;
		public string iik
		{
			get { return Iik; }
			set
			{
				Iik = value;
				OnPropertyChanged("iik");
			}
		}

		//Cтруктурное подразделение юридического лица-нерезидента (B 9.3)-
		private bool IsBranchNonResident;
		public bool isBranchNonResident
		{
			get { return IsBranchNonResident; }
			set
			{
				IsBranchNonResident = value;
				OnPropertyChanged("isBranchNonResident");
			}
		}

		//КБе (B1 12)+
		private string Kbe;
		public string kbe
		{
			get { return Kbe; }
			set
			{
				Kbe = value;
				OnPropertyChanged("kbe");
			}
		}

		//Наименование поставщика (B 7)+
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

		//БИН реорганизованного лица (B 6.1)-
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

		//Доля участия (B 7.1)-
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

		//Категориu поставщика (B 10)+
		[XmlArrayItem(ElementName = "status")]
		private ObservableCollection<UserStatus> Statuses;
		public  ObservableCollection<UserStatus> statuses 
		{
			get	{ return Statuses; }
			set
			{
				Statuses = value;
				OnPropertyChanged("statuses");
			}
		}

		//ИИН/БИН (B 6)+
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

		//Дополнительные сведения(B 11)+
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

		public SellerV2()
		{
			statuses = SessionDataManagerFacade.GetRandomSellerStatuses();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

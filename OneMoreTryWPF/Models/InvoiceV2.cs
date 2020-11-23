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
	public class InvoiceV2 : INotifyPropertyChanged
	{
		//Дата выписки ЭСФ (A 2)
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

		//Тип ЭСФ
		private InvoiceType InvoiceType = InvoiceType.ORDINARY_INVOICE;
		public InvoiceType invoiceType
		{
			get { return InvoiceType; }
			set
			{
				InvoiceType = value;
				OnPropertyChanged("invoiceType");
			}
		}

		//Исходящий номер ЭСФ в бухгалтерии отправителя
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

		//ФИО оператора отправившего ЭСФ
		//{0,200}
		private string OperatorFullname;
		public string operatorFullname
		{
			get { return OperatorFullname; }
			set
			{
				OperatorFullname = value;
				OnPropertyChanged("operatorFullname");
			}
		}

		//Служит для связки исправленного/дополнительного ЭСФ с основным
		private RelatedInvoice RelatedInvoice;
		public RelatedInvoice relatedInvoice
		{
			get{ return RelatedInvoice; }
			set
			{
				RelatedInvoice = value;
				OnPropertyChanged("relatedInvoice");
			}
		}


		//Дата совершения оборота (A 3)
		private DateTime TurnoverDate;
		[XmlElement("turnoverDate")]
		public string turnoverDate
		{
			get { return this.TurnoverDate.ToString("dd.MM.yyyy"); }
			set 
			{ 
				this.TurnoverDate = DateTime.Parse(value);
				OnPropertyChanged("turnoverDate");
			}
		}

		//Дополнительные сведения (K 43
		private string AddInf;
		public string addInf
		{
			get { return AddInf; }
			set
			{
				AddInf = value;
				OnPropertyChanged("addInf");
			}
		}

		//Реквизиты грузополучателя (D 24)
		private ConsigneeV2 Consignee;
		public ConsigneeV2 consignee
		{
			get { return Consignee; }
			set
			{
				Consignee = value;
				OnPropertyChanged("consignee");
			}
		}

		//Реквизиты грузоотправителя (D 23)
		private Consignor Consignor;
		public Consignor consignor
		{
			get { return Consignor; }
			set
			{
				Consignor = value;
				OnPropertyChanged("consignor");
			}
		}

		//Реквизиты поверенного (оператора) покупателя. Адрес места нахождения (J 41)-
		private string CustomerAgentAddress;
		public string customerAgentAddress
		{
			get { return CustomerAgentAddress; }
			set
			{
				CustomerAgentAddress = value;
				OnPropertyChanged("customerAgentAddress");
			}
		}

		//Документ-Дата (J 42.2)-
		private DateTime CustomerAgentDocDate;
		public string customerAgentDocDate
		{
			get { return this.CustomerAgentDocDate.ToString("dd.MM.yyyy"); }
			set 
			{ 
				this.CustomerAgentDocDate = DateTime.Parse(value);
				OnPropertyChanged("customerAgentDocDate");
			}
		}

		//Документ-Номер (J 42.1)-
		private string CustomerAgentDocNum;
		public string customerAgentDocNum
		{
			get { return this.CustomerAgentDocNum; }
			set
			{
				this.CustomerAgentDocNum = value;
				OnPropertyChanged("customerAgentDocNum");
			}
		}

		//Реквизиты поверенного(оператора) покупателя.Поверенный(J 40)-
		private string CustomerAgentName;
		public string customerAgentName
		{
			get { return this.CustomerAgentName; }
			set
			{
				this.CustomerAgentName = value;
				OnPropertyChanged("customerAgentName");
			}
		}

		//Реквизиты поверенного (оператора) покупателя. БИН (J 39)-
		private string CustomerAgentTin;
		public string customerAgentTin
		{
			get { return this.CustomerAgentTin; }
			set
			{
				this.CustomerAgentTin = value;
				OnPropertyChanged("customerAgentTin");
			}
		}

		//Получатели (УСД) (H)
		[XmlArrayItem(ElementName = "participant")]
		public ObservableCollection<ParticipantV2> customerParticipants { get; set; }

		//Получатели (C)
		[XmlArrayItem(ElementName = "customer")]
		public ObservableCollection<CustomerV2> customers { get; set; }

		//Дата выписки на бумажном носителе (2.1)-
		private DateTime DatePaper;
		public string datePaper
		{
			get { return this.DatePaper.ToString("dd.MM.yyyy"); }
			set
			{
				this.DatePaper = DateTime.Parse(value);
				OnPropertyChanged("datePaper");
			}
		}

		//Дата документа, подтверждающего поставку товаров (работ, услуг) (F 32.2)-
		private DateTime DeliveryDocDate;
		public string deliveryDocDate
		{
			get { return this.DeliveryDocDate.ToString("dd.MM.yyyy"); }
			set
			{ 
				this.DeliveryDocDate = DateTime.Parse(value);
				OnPropertyChanged("deliveryDocDate");
			}
		}

		//Номер документа, подтверждающего поставку товаров (работ, услуг) (F 32.1)-
		private string DeliveryDocNum;
		public string deliveryDocNum
		{
			get { return this.DeliveryDocNum; }
			set
			{
				this.DeliveryDocNum = value;
				OnPropertyChanged("deliveryDocNum");
			}
		}

		//Условия поставки (E)
		private DeliveryTermV2 DeliveryTerm;
		public DeliveryTermV2 deliveryTerm
		{
			get { return DeliveryTerm; }
			set
			{
				DeliveryTerm = value;
				OnPropertyChanged("deliveryTerm");
			}
		}

		//Товары(работы, услуги) (G)
		private ProductSetV2 ProductSet;
		public ProductSetV2 productSet
		{
			get { return ProductSet; }
			set
			{
				ProductSet = value;
				OnPropertyChanged("productSet");
			}
		}


		//Реквизиты государственного учреждения (F)
		private PublicOffice PublicOffice;
		public PublicOffice publicOffice
		{
			get { return PublicOffice; }
			set
			{
				PublicOffice = value;
				OnPropertyChanged("publicOffice");
			}
		}

		//Причина выписки на бумажном носителе (2.1)-
		private PaperReasonType? ReasonPaper;
		public PaperReasonType? reasonPaper
		{
			get { return this.ReasonPaper; }
			set
			{
				this.ReasonPaper = value;
				OnPropertyChanged("reasonPaper");
			}
		}

		//Адрес места нахождения(I 37)-
		private string SellerAgentAddress;
		public string sellerAgentAddress
		{
			get { return this.SellerAgentAddress; }
			set
			{
				this.SellerAgentAddress = value;
				OnPropertyChanged("sellerAgentAddress");
			}
		}

		//Документ-Дата (I 38.2)-
		private DateTime SellerAgentDocDate;
		public string sellerAgentDocDate
		{
			get { return this.SellerAgentDocDate.ToString("dd.MM.yyyy"); }
			set
			{ 
				this.SellerAgentDocDate = DateTime.Parse(value);
				OnPropertyChanged("sellerAgentDocDate");
			}
		}


		//Документ-Номер (I 38.1)-
		private string SellerAgentDocNum;
		public string sellerAgentDocNum
		{
			get { return SellerAgentDocNum; }
			set
			{
				SellerAgentDocNum = value;
				OnPropertyChanged("sellerAgentDocNum");
			}
		}

		//Поверенный (I 36)-
		private string SellerAgentName;
		public string sellerAgentName
		{
			get { return SellerAgentName; }
			set
			{
				SellerAgentName = value;
				OnPropertyChanged("sellerAgentName");
			}
		}

		//БИН (I 35)-
		private string SellerAgentTin;
		public string sellerAgentTin
		{
			get { return SellerAgentTin; }
			set
			{
				SellerAgentTin = value;
				OnPropertyChanged("sellerAgentTin");
			}
		}

		//Поставщики(УСД) (H)
		[XmlArrayItem(ElementName = "participant")]
		public ObservableCollection<ParticipantV2> sellerParticipants { get; set; }

		//Поставщики (B)
		[XmlArrayItem(ElementName = "seller")]
		public ObservableCollection<SellerV2> sellers { get; set;}

		public InvoiceV2()
		{
			Date = DateTime.Now;
			//num = SessionDataManagerFacade.GetNewInvoiceNum();
			operatorFullname = SessionDataManagerFacade.GetOperatorFullName();
			//TurnoverDate
			consignee = new ConsigneeV2();
			customers = SessionDataManagerFacade.GetCustomers();
			deliveryTerm = new DeliveryTermV2();
			productSet = SessionDataManagerFacade.GetProductSet();
			sellers = SessionDataManagerFacade.GetSellers();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

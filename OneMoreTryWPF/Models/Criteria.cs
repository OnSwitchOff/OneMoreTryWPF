using OneMoreTryWPF.InvoiceService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class Criteria:INotifyPropertyChanged
	{

		private InvoiceDirection Direction = InvoiceDirection.INBOUND;
		public InvoiceDirection direction
		{
			get { return Direction; }
			set
			{
				Direction = value;
				OnPropertyChanged("direction");
			}
		}


		/*private string ContragentTin = String.Empty;
		public string contragentTin
		{
			get { return ContragentTin; }
			set
			{
				ContragentTin = value;
				OnPropertyChanged("contragentTin");
			}
		}*/

		private DateTime DateFrom = DateTime.Now.AddDays(-10);
		public DateTime dateFrom
		{
			get { return DateFrom; }
			set
			{
				DateFrom = value;
				OnPropertyChanged("dateFrom");
			}
		}

		private DateTime DateTo = DateTime.Now;
		public DateTime dateTo
		{
			get { return DateTo; }
			set
			{
				DateTo = value;
				OnPropertyChanged("dateTo");
			}
		}

		private bool IsCreated = true;
		public bool isCreated
		{
			get { return IsCreated; }
			set 
			{ 
				IsCreated = value;
				OnPropertyChanged("isCreated");
			}
		}

		private bool IsDelivered = true;
		public bool isDelivered
		{
			get { return IsDelivered; }
			set
			{
				IsDelivered = value;
				OnPropertyChanged("isDelivered");
			}
		}

		private bool IsCanceled = true;
		public bool isCanceled
		{
			get { return IsCanceled; }
			set
			{
				IsCanceled= value;
				OnPropertyChanged("isCanceled");
			}
		}

		private bool IsRevoked = true;
		public bool isRevoked
		{
			get { return IsRevoked; }
			set
			{
				IsRevoked = value;
				OnPropertyChanged("isRevoked");
			}
		}

		private bool IsImported = false;
		public bool isImported
		{
			get { return IsImported; }
			set
			{
				IsImported = value;
				OnPropertyChanged("isImported");
			}
		}

		private bool IsFailed = true;
		public bool isFailed
		{
			get { return IsFailed; }
			set
			{
				IsFailed = value;
				OnPropertyChanged("isFailed");
			}
		}

		private bool IsDeleted = false;
		public bool isDeleted
		{
			get { return IsDeleted; }
			set
			{
				IsDeleted= value;
				OnPropertyChanged("isDeleted");
			}
		}

		private bool IsDeclined = true;
		public bool isDeclined
		{
			get { return IsDeclined; }
			set
			{
				IsDeclined = value;
				OnPropertyChanged("isDeclined");
			}
		}

		private bool IsDraft = false;
		public bool isDraft
		{
			get { return IsDraft; }
			set
			{
				IsDraft = value;
				OnPropertyChanged("isDraft");
			}
		}

		private bool IsCanceledByOGD = false;
		public bool isCanceledByOGD
		{
			get { return IsCanceledByOGD; }
			set
			{
				IsCanceledByOGD = value;
				OnPropertyChanged("isCanceledByOGD");
			}
		}


		private bool IsOrdinary = true;
		public bool isOrdinary
		{
			get { return IsOrdinary; }
			set
			{
				IsOrdinary = value;
				OnPropertyChanged("isOrdinary");
			}
		}

		private bool IsFixed = false;
		public bool isFixed
		{
			get { return IsFixed; }
			set
			{
				IsFixed = value;
				OnPropertyChanged("isFixed");
			}
		}

		private bool IsAdditional = false;
		public bool isAdditional
		{
			get { return IsAdditional; }
			set
			{
				IsAdditional = value;
				OnPropertyChanged("isAdditional");
			}
		}


		public Criteria() { }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

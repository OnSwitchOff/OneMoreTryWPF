using OneMoreTryWPF.InvoiceService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class MyInvoiceInfo:InvoiceInfo, INotifyPropertyChanged
	{
		private InvoiceV2 Invoice;
		public InvoiceV2 invoice 
		{
			get {return Invoice; }
			set
			{
				Invoice = value;
				turnoverDate = Invoice.turnoverDate;
				turnoverNum = Invoice.num;
				seller = Invoice.sellers[0].name;
				customer = Invoice.customers[0].name;
				totalPriceWithTax = Invoice.productSet.totalPriceWithTax;
				OnPropertyChanged("invoice");
			}
		}

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

		private string TurnoverDate;
		public string turnoverDate
		{
			get { return TurnoverDate; }
			set
			{
				TurnoverDate = value;
				OnPropertyChanged("turnoverDate");
			}
		}

		private string TurnoverNum;
		public string turnoverNum
		{
			get { return TurnoverNum; }
			set
			{
				TurnoverNum = value;
				OnPropertyChanged("turnoverNum");
			}
		}

		private string Seller;
		public string seller
		{
			get { return Seller; }
			set
			{
				Seller = value;
				OnPropertyChanged("seller");
			}
		}

		private string Customer;
		public string customer
		{
			get { return Customer; }
			set
			{
				Customer = value;
				OnPropertyChanged("customer");
			}
		}

		private float TotalPriceWithTax;
		public float totalPriceWithTax
		{
			get { return TotalPriceWithTax; }
			set
			{
				TotalPriceWithTax = value;
				OnPropertyChanged("totalPriceWithTax");
			}
		}


		public MyInvoiceInfo() { }

		public event PropertyChangedEventHandler MyPropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			MyPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

using OneMoreTryWPF.Facades;
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

				
				IsValidProductSet = ProductSetValidation();
				switch (Direction)
				{
					case InvoiceDirection.INBOUND:
						IsValidSeller = SellerValidation(invoice.sellers[0]);
						IsValidCustomer = true;
						break;
					case InvoiceDirection.OUTBOUND:
						IsValidSeller = true;
						IsValidCustomer = CustomerValidation(invoice.customers[0]);
						break;
					default:
						IsValidSeller = SellerValidation(invoice.sellers[0]);
						IsValidCustomer = CustomerValidation(invoice.customers[0]);
						break;
				}
				OnPropertyChanged("invoice");
			}
		}

		private bool isValidSeller;
		private bool isValidCustomer;
		private bool isValidProductSet;

		public bool IsValidSeller
		{
			get { return isValidSeller; }
			set
			{
				isValidSeller = value;
				OnPropertyChanged("IsValidSeller");
			}
		}

		public bool IsValidCustomer
		{
			get { return isValidCustomer; }
			set
			{
				isValidCustomer = value;
				OnPropertyChanged("IsValidCustomer");
			}
		}

		public bool IsValidProductSet
		{
			get { return isValidProductSet; }
			set
			{
				isValidProductSet = value;
				OnPropertyChanged("IsValidProductSet");
			}
		}


		private Partner MyPartner = new Partner();
		public Partner myPartner
		{
			get { return MyPartner; }
			set
			{
				MyPartner = value;
				OnPropertyChanged("myPartner");
			}
		}

		private Object MyObject = new Object();
		public Object myObject
		{
			get { return MyObject; }
			set
			{
				MyObject = value;
				OnPropertyChanged("myObject");
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

		private string InvoiceNumber;
		public string invoiceNumber
		{
			get { return InvoiceNumber; }
			set
			{
				InvoiceNumber = value;
				OnPropertyChanged("InvoiceNumber");
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


		private bool ProductSetValidation()
		{
			bool flag = true;
			foreach (ProductV2 prod in Invoice.productSet.products)
			{
				prod.IsContained = ProductValidation(prod);
				flag = flag && prod.IsContained;
			}
			return flag;
		}

		private bool SellerValidation(SellerV2 seller)
		{
			myPartner.Id = SessionDataManagerFacade.SearchInPartners(seller);		
			return SessionDataManagerFacade.GetRetailUserName() == seller.name? true: myPartner.Id != 0;
		}

		private bool CustomerValidation(CustomerV2 customer)
		{
			myPartner.Id = SessionDataManagerFacade.SearchInPartners(customer);
			return SessionDataManagerFacade.GetRetailUserName() == customer.name ? true : myPartner.Id != 0;
		}

		private bool ProductValidation(ProductV2 prod)
		{
			return SessionDataManagerFacade.SearchInGoods(prod) != 0;
		}
	}
}

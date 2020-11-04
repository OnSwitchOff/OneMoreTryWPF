using Microinvest.Common;
using OneMoreTryWPF.Facades;
using OneMoreTryWPF.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class InvoiceViewModel: INotifyPropertyChanged
	{
		//public ObservableCollection<ProductV2> Products { get; set; }

		private InvoiceV2 invoice;
		/*private ProductSetV2 productSet;
		private SellerV2 seller;
		private CustomerV2 customer;*/
		private ProductV2 selectedProduct;
		private bool isEditable;
		private bool isValidSeller;
		private bool godMode;




		private int partnerId;
		public int PartnerId
		{
			get { return partnerId; }
			set
			{
				partnerId = value;
				if(PartnerId!=0)
				{
					Partner _partner = new Partner();
					_partner.Id = PartnerId;
					_partner.Name = SessionDataManagerFacade.getPartners().Select(String.Format("ID={0} ", PartnerId))[0]["Company"].ToString();
					Partner = _partner;
					IsValidSeller = true;
				}
				OnPropertyChanged("PartnerId");
			}
		}

		private int objectId;
		public int ObjectId
		{
			get { return objectId; }
			set
			{
				objectId = value;
				if (ObjectId != 0)
				{
					Object _obj = new Object();
					_obj.Id = ObjectId;
					_obj.Name = SessionDataManagerFacade.getObjects().Select(String.Format("ID={0} ", ObjectId))[0]["Name"].ToString();
					Object = _obj;
				}
				OnPropertyChanged("ObjectId");
			}
		}

		private Partner partner;
		public Partner Partner
		{
			get { return partner; }
			set
			{
				partner = value;
				OnPropertyChanged("Partner");
			}
		}

		private Object obj;
		public Object Object
		{
			get { return obj; }
			set
			{
				obj = value;
				OnPropertyChanged("Object");
			}
		}

		public InvoiceV2 Invoice
		{
			get { return invoice; }
			set
			{
				invoice = value;
				ReCalcRowNumbers();
				IsValidSeller = SellerValidation(invoice.sellers[0]);
				OnPropertyChanged("Invoice");
			}
		}

		private bool SellerValidation(SellerV2 seller)
		{
			PartnerId = SessionDataManagerFacade.SearchInPartners(seller);
			return PartnerId !=0;
		}



		/*public ProductSetV2 ProductSet
		{
			get { return productSet; }
			set
			{
				productSet = value;
				OnPropertyChanged("ProductSet");
			}
		}*/

		/*public SellerV2 Seller
		{
			get { return seller; }
			set
			{
				seller = value;
				OnPropertyChanged("Seller");
			}
		}*/

		/*public CustomerV2 Customer
		{
			get { return customer; }
			set
			{
				customer = value;
				OnPropertyChanged("Customer");
			}
		}*/

		public ProductV2 SelectedProduct
		{
            get { return selectedProduct; }
			set
			{
				selectedProduct = value;
				OnPropertyChanged("SelectedProduct");
			}
        }
		public bool IsEditable
		{
			get { return isEditable; }
			set
			{
				isEditable = value;
				OnPropertyChanged("IsEditable");
			}
		}

		public bool GodMode
		{
			get { return godMode; }
			set
			{
				godMode = value;
				OnPropertyChanged("GodMode");
			}
		}

		public bool IsValidSeller
		{
			get { return isValidSeller; }
			set
			{
				isValidSeller = value;
				OnPropertyChanged("IsValidSeller");
			}
		}


		public InvoiceViewModel()
		{
			Invoice = new InvoiceV2();
			//ProductSet = new ProductSetV2();
			//ProductSet.products = SessionDataManagerFacade.GetRandomProducts();
			//Seller = new SellerV2();
			//Customer = new CustomerV2();
			IsEditable = false;
			GodMode = false;
			IsValidSeller = false;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		private RelayCommand addCommand;
		public RelayCommand AddCommand
		{
			get
			{
				return addCommand ??
				  (addCommand = new RelayCommand(obj =>
				  {
					  ProductV2 product = new ProductV2();
					  product.truOriginCode = 3;
					  product.rowNumber = Invoice.productSet.products.Count + 1;
					  Invoice.productSet.products.Add(product);
					  SelectedProduct = product;
				  }));
			}
		}

		private RelayCommand removeCommand;
		public RelayCommand RemoveCommand
		{
			get
			{
				return removeCommand ??
				  (removeCommand = new RelayCommand(obj =>
				  {
						while(selectedProduct!=null)
						{
						  Invoice.productSet.products.Remove(SelectedProduct);
							ReCalcRowNumbers();
						}
				  },
				  (obj)=> Invoice.productSet.products.Count>0));
			}
		}


		private RelayCommand selectPartnerCommand;
		public RelayCommand SelectPartnerCommand
		{
			get
			{
				return selectPartnerCommand ??
				  (selectPartnerCommand = new RelayCommand(obj =>
				  {
					  SelectPartnerWindow selectPartnerWindow = new SelectPartnerWindow();
					  if(selectPartnerWindow.ShowDialog()==true)
					  {
						  PartnerId = ((SelectPartnerViewModel)selectPartnerWindow.DataContext).SelectedPartner.Id;
					  }
				  }));
			}
		}

		private RelayCommand addPartnerCommand;
		public RelayCommand AddPartnerCommand
		{
			get
			{
				return addPartnerCommand ??
				  (addPartnerCommand = new RelayCommand(obj =>
				  {
					  PartnerId = SessionDataManagerFacade.AddRetailerToDB(Invoice);
				  }));
			}
		}

		private RelayCommand selectObjectCommand;
		public RelayCommand SelectObjectCommand
		{
			get
			{
				return selectObjectCommand ??
				  (selectObjectCommand = new RelayCommand(obj =>
				  {
					  SelectObjectWindow selectObjectWindow = new SelectObjectWindow();
					  if (selectObjectWindow.ShowDialog() == true)
					  {
						  ObjectId = ((SelectObjectViewModel)selectObjectWindow.DataContext).SelectedObject.Id;
					  }
				  }));
			}
		}

		private void ReCalcRowNumbers()
		{
			for (int i = 0; i < Invoice.productSet.products.Count; i++)
			{
				Invoice.productSet.products[i].rowNumber = i+1;
			}
		}
	}
}

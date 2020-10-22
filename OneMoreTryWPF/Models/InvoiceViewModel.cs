using OneMoreTryWPF.Facades;
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
		private ProductSetV2 productSet;
		private SellerV2 seller;
		private CustomerV2 customer;
		private ProductV2 selectedProduct;
		private bool isEditable;
		private bool godMode;


		public InvoiceV2 Invoice
		{
			get { return invoice; }
			set
			{
				invoice = value;
				OnPropertyChanged("Invoice");
			}
		}

		public ProductSetV2 ProductSet
		{
			get { return productSet; }
			set
			{
				productSet = value;
				OnPropertyChanged("ProductSet");
			}
		}

		public SellerV2 Seller
		{
			get { return seller; }
			set
			{
				seller = value;
				OnPropertyChanged("Seller");
			}
		}

		public CustomerV2 Customer
		{
			get { return customer; }
			set
			{
				customer = value;
				OnPropertyChanged("Customer");
			}
		}

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


		public InvoiceViewModel()
		{
			Invoice = new InvoiceV2();
			//ProductSet = new ProductSetV2();
			//ProductSet.products = SessionDataManagerFacade.GetRandomProducts();
			//Seller = new SellerV2();
			//Customer = new CustomerV2();
			IsEditable = false;
			GodMode = false;
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

		private void ReCalcRowNumbers()
		{
			for (int i = 0; i < Invoice.productSet.products.Count; i++)
			{
				Invoice.productSet.products[i].rowNumber = i+1;
			}
		}
	}
}

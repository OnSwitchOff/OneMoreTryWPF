using OneMoreTryWPF.Facades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class ProductSetViewModel: INotifyPropertyChanged
	{
		public ObservableCollection<ProductV2> Products { get; set; }

		private SellerV2 seller;
		private ProductV2 selectedProduct;
		private bool isEditable;
		private bool godMode;		
		

		public SellerV2 Seller
		{
			get { return seller; }
			set
			{
				seller = value;
				OnPropertyChanged("Seller");
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


		public ProductSetViewModel()
		{
			Products = SessionDataManagerFacade.GetRandomProducts();
			Seller = new SellerV2();
			isEditable = false;
			godMode = false;
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
					  product.rowNumber = Products.Count + 1;
					  Products.Add(product);
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
							Products.Remove(SelectedProduct);
							ReCalcRowNumbers();
						}
				  },
				  (obj)=>Products.Count>0));
			}
		}

		private void ReCalcRowNumbers()
		{
			for (int i = 0; i < Products.Count; i++)
			{
				Products[i].rowNumber = i+1;
			}
		}
	}
}

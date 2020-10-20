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
		private ProductV2 selectedProduct;
		private bool isEditable;

		
		public ObservableCollection<ProductV2> Products { get; set;}
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


		public ProductSetViewModel()
		{
			Products = SessionDataManagerFacade.GetRandomProducts();
			isEditable = false;
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
					  product.rowNumber = Products.Count + 1;
					  Products.Add(product);
					  SelectedProduct = product;
				  }));
			}
		}
	}
}

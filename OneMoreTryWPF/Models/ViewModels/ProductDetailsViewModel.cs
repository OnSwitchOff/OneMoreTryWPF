using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models.ViewModels
{
	public class ProductDetailsViewModel : INotifyPropertyChanged
	{
		private bool isEditable = false;
		public bool IsEditable
		{
			get { return isEditable; }
			set
			{
				isEditable = value;
				OnPropertyChanged("IsEditable");
			}
		}

		private ProductV2 product;
		public ProductV2 Product
		{
			get { return product; }
			set
			{
				product = value;
				OnPropertyChanged("Product");
			}
		}

		public ProductDetailsViewModel(ProductV2 _product)
		{
			Product = _product;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

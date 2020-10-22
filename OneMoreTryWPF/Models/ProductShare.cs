using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class ProductShare:INotifyPropertyChanged
	{
		private string Additional;
		public string additional
		{
			get { return Additional; }
			set
			{
				Additional = value;
				OnPropertyChanged("additional");
			}
		}

		//Акциз-Сумма (H 10)
		//fractionDigits value="2", totalDigits value="17"
		private float ExciseAmount;
		public float exciseAmount
		{
			get { return ExciseAmount; }
			set
			{
				ExciseAmount = value;
				OnPropertyChanged("exciseAmount");
			}
		}

		//НДС-Сумма(H 13)
		//fractionDigits value="2", totalDigits value="17"
		private float NdsAmount;
		public float ndsAmount
		{
			get { return NdsAmount; }
			set
			{
				NdsAmount = value;
				OnPropertyChanged("ndsAmount");
			}
		}
		//Стоимость ТРУ с учетом НДС (H 14)
		//fractionDigits value="2", totalDigits value="17"
		private float PriceWithTax;
		public float priceWithTax
		{
			get { return PriceWithTax; }
			set
			{
				PriceWithTax = value;
				OnPropertyChanged("priceWithTax");
			}
		}

		//Стоимость ТРУ без учета НДС (H 7)
		//fractionDigits value="2", totalDigits value="17"
		private float PriceWithoutTax;
		public float priceWithoutTax
		{
			get { return PriceWithoutTax; }
			set
			{
				PriceWithoutTax = value;
				OnPropertyChanged("priceWithoutTax");
			}
		}

		//Номер продукта (товара, услуги) (H 1)
		//1-200
		private int ProductNumber;
		public int productNumber
		{
			get { return ProductNumber; }
			set
			{
				ProductNumber = value;
				OnPropertyChanged("productNumber");
			}
		}

		//Кол-во (объем) (H 6)
		//fractionDigits value="6", totalDigits value="18"
		private string Quantity;
		public string quantity
		{
			get { return Quantity; }
			set
			{
				Quantity = value;
				OnPropertyChanged("quantity");
			}
		}

		//Размер оборота по реализации (H 11)
		//fractionDigits value="2", totalDigits value="17"
		private float TurnoverSize;
		public float turnoverSize
		{
			get { return TurnoverSize; }
			set
			{
				TurnoverSize = value;
				OnPropertyChanged("turnoverSize");
			}
		}

		public ProductShare() { }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

using OneMoreTryWPF.ENUMs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OneMoreTryWPF.Models
{
	public class ProductSetV2 : INotifyPropertyChanged
	{
		//Код валюты (G 33.1)
		private string CurrencyCode = "KZT";
		public string currencyCode
		{
			get { return CurrencyCode; }
			set
			{
				CurrencyCode = value;
				OnPropertyChanged("currencyCode");
			}
		}


		//Курс валюты(G 33.2)
		//fractionDigits value="6", totalDigits value="18"
		private float? CurrencyRate;
		public float? currencyRate
		{
			get { return CurrencyRate; }
			set
			{
				CurrencyRate = value;
				OnPropertyChanged("currencyRate");
			}
		}

		//Тип НДС ('Без НДС – не РК')-
	    private NdsRateType? NdsRateType;
		public NdsRateType? ndsRateType
		{
			get { return NdsRateType; }
			set
			{
				NdsRateType = value;
				OnPropertyChanged("ndsRateType");
			}
		}

		//Список ТРУ+
		[XmlArrayItem(ElementName = "product")]
		public ObservableCollection<ProductV2> products { get; set; }

		//Итоговая Акциз-Сумма (G 10)+
		//fractionDigits value="2", totalDigits value="17"
		private float TotalExciseAmount;
		public float totalExciseAmount
		{
			get { return TotalExciseAmount; }
			set
			{
				TotalExciseAmount = value;
				OnPropertyChanged("totalExciseAmount");
			}
		}


		//Итоговая НДС-Сумма (G 13)+
		//fractionDigits value="2", totalDigits value="17"
		private float TotalNdsAmount;
		public float totalNdsAmount
		{
			get { return TotalNdsAmount; }
			set
			{
				TotalNdsAmount = value;
				OnPropertyChanged("totalNdsAmount");
			}
		}

		//Итоговая стоимость ТРУ с учетом НДС (G 14)+
		//fractionDigits value="2", totalDigits value="17"
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
		//Итоговая стоимость ТРУ без учета НДС (G 8)+
		//fractionDigits value="2", totalDigits value="17"
		private float TotalPriceWithoutTax;
		public float totalPriceWithoutTax
		{
			get { return TotalPriceWithoutTax; }
			set
			{
				TotalPriceWithoutTax = value;
				OnPropertyChanged("totalPriceWithTax");
			}
		}

		//Итоговый размер оборота по реализации (G 11)+
		//fractionDigits value="2", totalDigits value="17"
		private float TotalTurnoverSize;
		public float totalTurnoverSize
		{
			get { return TotalTurnoverSize; }
			set
			{
				TotalTurnoverSize = value;
				OnPropertyChanged("totalTurnoverSize");
			}
		}

		public ProductSetV2() { }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class ProductV2 : INotifyPropertyChanged
	{
		//Дополнительные данные (G 18)
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

		//Идентификатор товара, работ, услуг (G 17)-
		private string CatalogTruId;
		public string catalogTruId
		{
			get { return CatalogTruId; }
			set
			{
				CatalogTruId = value;
				OnPropertyChanged("catalogTruId");
			}
		}


		//Наименование ТРУ (G 3)
		private string Description;
		public string description
		{
			get { return Description; }
			set
			{
				Description = value;
				OnPropertyChanged("description");
			}
		}

		//Акциз-Сумма (G 10)
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

		//Акциз-Ставка (G 9)
		//fractionDigits value="2",minInclusive value="0",totalDigits value="14"
		private float ExciseRate;
		public float exciseRate
		{
			get { return ExciseRate; }
			set
			{
				ExciseRate = value;
				OnPropertyChanged("exciseRate");
			}
		}

		//Классификатор продукции по видам экономической деятельности-
		private string KpvedCode;
		public string kpvedCode
		{
			get { return KpvedCode; }
			set
			{
				KpvedCode = value;
				OnPropertyChanged("kpvedCode");
			}
		}

		//НДС-Сумма(G 13)
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

		//НДС-Ставка (G 12)
		private float NdsRate;

		public float ndsRate
		{
			get { return NdsRate; }
			set
			{
				NdsRate = value;
				OnPropertyChanged("ndsRate");
			}
		}

		//Стоимость ТРУ с учетом НДС (G 14)
		//fractionDigits value = "2", totalDigits value="17
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

		//Стоимость ТРУ без косвенных налогов (G 8)
		//fractionDigits value = "2", totalDigits value="17
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

		//Декларации на товары, заявления в рамках ТС, СТ-1 или СТ-KZ(G 15)-
		private string ProductDeclaration;
		public string productDeclaration
		{
			get { return ProductDeclaration; }
			set
			{
				ProductDeclaration = value;
				OnPropertyChanged("productDeclaration");
			}
		}

		//Номер товарной позиции из заявления в рамках ТС или Декларации на товары (G 16)-
		private string ProductNumberInDeclaration;
		public string productNumberInDeclaration
		{
			get { return ProductNumberInDeclaration; }
			set
			{
				ProductNumberInDeclaration = value;
				OnPropertyChanged("productNumberInDeclaration");
			}
		}
		//Кол-во (объем) (G 6)
		//fractionDigits value="6", totalDigits value="18"
		private string Quantity;
		public string quantity
		{
			get { return Quantity; }
			set
			{
				Quantity = value;
				OnPropertyChanged("quantity");
				if (Quantity == String.Empty)
				{
					unitNomenclature = String.Empty;
				}				
			}
		}

		//Наименование товаров по классификатору ТН ВЭД ЕАЭС(G 3/1)-
		private string TnvedName;

		public string tnvedName
		{
			get { return TnvedName; }
			set
			{
				TnvedName = value;
				OnPropertyChanged("tnvedName");
			}
		}
		//Признак происхождения ТРУ (G 2)-
		private int TruOriginCode;
		public int truOriginCode
		{
			get { return TruOriginCode; }
			set
			{
				TruOriginCode = value;
				OnPropertyChanged("truOriginCode");
				if (truOriginCode == 6)
				{
					quantity = String.Empty;
				}
			}
		}

		//Размер оборота по реализации (облагаемый/необлагаемый оборот) (G 11)
		//fractionDigits value = "2", totalDigits value="17
		private float TurnoverSize;

		internal ProductV2 Clone()
		{
			ProductV2 clone = new ProductV2();
			clone.additional = this.Additional;
			clone.catalogTruId = this.catalogTruId;
			clone.description = this.description;
			clone.exciseAmount = this.exciseAmount;
			clone.exciseRate = this.exciseRate;
			clone.kpvedCode = this.kpvedCode;
			clone.ndsAmount = this.ndsAmount;
			clone.ndsRate = this.ndsRate;
			clone.priceWithoutTax = this.priceWithoutTax;
			clone.priceWithTax = this.priceWithTax;
			clone.productDeclaration = this.productDeclaration;
			clone.productNumberInDeclaration = this.productNumberInDeclaration;
			clone.quantity = this.quantity;
			clone.rowNumber = this.rowNumber;
			clone.tnvedName = this.tnvedName;
			clone.truOriginCode = this.truOriginCode;
			clone.turnoverSize = this.turnoverSize;
			clone.unitCode = this.unitCode;
			clone.unitNomenclature = this.unitNomenclature;
			clone.unitPrice = this.unitPrice;
			return clone;
		}

		public float turnoverSize
		{
			get { return TurnoverSize; }
			set
			{
				TurnoverSize = value;
				OnPropertyChanged("turnoverSize");
			}
		}
		//Код товара(ТНВД ЕАЭС) (G 4)
		//pattern value="[0-9]{1,10}
		private string UnitCode;
		public string unitCode
		{
			get { return UnitCode; }
			set
			{
				UnitCode = value;
				OnPropertyChanged("unitCode");
			}
		}

		//Ед.изм(G 5)
		private string UnitNomenclature;
		public string unitNomenclature
		{
			get { return UnitNomenclature; }
			set
			{
				UnitNomenclature = value;
				OnPropertyChanged("unitNomenclature");
			}
		}

		//Цена (тариф) за единицу ТРУ без косвенных налогов (G 7)
		//fractionDigits value="6", totalDigits value="18"
		private float? UnitPrice;
		public float? unitPrice
		{
			get { return UnitPrice; }
			set
			{
				UnitPrice = value;
				OnPropertyChanged("unitPrice");
			}
		}

		private int RowNumber;
		public int rowNumber
		{
			get { return RowNumber; }
			set
			{
				RowNumber = value;
				OnPropertyChanged("rowNumber");
			}
		}

		public ProductV2() { }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

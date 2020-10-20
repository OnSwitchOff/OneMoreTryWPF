using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OneMoreTryWPF.UserControls
{
	/// <summary>
	/// Interaction logic for ProductForm.xaml
	/// </summary>
	public partial class ProductForm : UserControl
	{
		public ProductForm()
		{
			InitializeComponent();
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			/*switch (cbTruOriginCode.Text)
			{
				case "1":
					tbDescription.Visibility = Visibility.Collapsed;
					tbTnvedName.Visibility = Visibility.Visible;
					tbUnitCode.Visibility = Visibility.Visible;
					tbUnitNomenclature.Visibility = Visibility.Visible;
					tbQuantity.Visibility = Visibility.Visible;
					tbUnitPrice.Visibility = Visibility.Visible;
					tbProductDeclaration.Visibility = Visibility.Visible;
					tbProductNumberInDeclaration.Visibility = Visibility.Visible;
					break;
				case "2":
					tbDescription.Visibility = Visibility.Collapsed;
					tbTnvedName.Visibility = Visibility.Visible;
					tbUnitCode.Visibility = Visibility.Visible;
					tbUnitNomenclature.Visibility = Visibility.Visible;
					tbQuantity.Visibility = Visibility.Visible;
					tbUnitPrice.Visibility = Visibility.Visible;
					tbProductDeclaration.Visibility = Visibility.Visible;
					tbProductNumberInDeclaration.Visibility = Visibility.Visible;
					break;
				case "3":
					tbDescription.Visibility = Visibility.Visible;
					tbTnvedName.Visibility = Visibility.Collapsed;
					tbUnitCode.Visibility = Visibility.Visible;
					tbUnitNomenclature.Visibility = Visibility.Visible;
					tbQuantity.Visibility = Visibility.Visible;
					tbUnitPrice.Visibility = Visibility.Visible;
					tbProductDeclaration.Visibility = Visibility.Visible;
					tbProductNumberInDeclaration.Visibility = Visibility.Collapsed;
					break;
				case "4":
					tbDescription.Visibility = Visibility.Visible;
					tbTnvedName.Visibility = Visibility.Collapsed;
					tbUnitCode.Visibility = Visibility.Visible;
					tbUnitNomenclature.Visibility = Visibility.Visible;
					tbQuantity.Visibility = Visibility.Visible;
					tbUnitPrice.Visibility = Visibility.Visible;
					tbProductDeclaration.Visibility = Visibility.Collapsed;
					tbProductNumberInDeclaration.Visibility = Visibility.Collapsed;
					break;
				case "5":
					tbDescription.Visibility = Visibility.Visible;
					tbTnvedName.Visibility = Visibility.Collapsed;
					tbUnitCode.Visibility = Visibility.Collapsed;
					tbUnitNomenclature.Visibility = Visibility.Visible;
					tbQuantity.Visibility = Visibility.Visible;
					tbUnitPrice.Visibility = Visibility.Visible;
					tbProductDeclaration.Visibility = Visibility.Collapsed;
					tbProductNumberInDeclaration.Visibility = Visibility.Collapsed;
					break;
				case "6":
					tbDescription.Visibility = Visibility.Visible;
					tbTnvedName.Visibility = Visibility.Collapsed;
					tbUnitCode.Visibility = Visibility.Collapsed;
					tbUnitNomenclature.Visibility = Visibility.Collapsed;
					tbQuantity.Visibility = Visibility.Collapsed;
					tbUnitPrice.Visibility = Visibility.Collapsed;
					tbProductDeclaration.Visibility = Visibility.Collapsed;
					tbProductNumberInDeclaration.Visibility = Visibility.Collapsed;
					break;
				default:
					break;
			}
			tbPriceWithoutTax.Visibility = Visibility.Visible;
			tbExciseRate.Visibility = Visibility.Visible;
			tbExciseAmount.Visibility = Visibility.Visible;
			tbTurnoverSize.Visibility = Visibility.Visible;
			tbNdsRate.Visibility = Visibility.Visible;
			tbNdsAmount.Visibility = Visibility.Visible;
			tbPriceWithTax.Visibility = Visibility.Visible;
			tbCatalogTruId.Visibility = Visibility.Visible;
			tbAdditional.Visibility = Visibility.Visible;*/
		}
	}
}

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
			switch (cbTruOriginCode.SelectedIndex)
			{
				case 0:
					tbDescription.Background = ((SolidColorBrush)this.TryFindResource("ignored"));// Background.Collapsed;
					tbTnvedName.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitCode.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitNomenclature.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbQuantity.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitPrice.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbProductDeclaration.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbProductNumberInDeclaration.Background = ((SolidColorBrush)this.TryFindResource("required"));
					break;
				case 1:
					tbDescription.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbTnvedName.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitCode.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitNomenclature.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbQuantity.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitPrice.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbProductDeclaration.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbProductNumberInDeclaration.Background = ((SolidColorBrush)this.TryFindResource("required"));
					break;
				case 2:
					tbDescription.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbTnvedName.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbUnitCode.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitNomenclature.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbQuantity.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitPrice.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbProductDeclaration.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbProductNumberInDeclaration.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					break;
				case 3:
					tbDescription.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbTnvedName.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbUnitCode.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitNomenclature.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbQuantity.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitPrice.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbProductDeclaration.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbProductNumberInDeclaration.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					break;
				case 4:
					tbDescription.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbTnvedName.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbUnitCode.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbUnitNomenclature.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbQuantity.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbUnitPrice.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbProductDeclaration.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbProductNumberInDeclaration.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					break;
				case 5:
					tbDescription.Background = ((SolidColorBrush)this.TryFindResource("required"));
					tbTnvedName.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbUnitCode.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbUnitNomenclature.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbQuantity.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbUnitPrice.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbProductDeclaration.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					tbProductNumberInDeclaration.Background = ((SolidColorBrush)this.TryFindResource("ignored"));
					break;
				default:
					break;
			}
			tbPriceWithoutTax.Background = ((SolidColorBrush)this.TryFindResource("required"));
			tbExciseRate.Background = ((SolidColorBrush)this.TryFindResource("required"));
			tbExciseAmount.Background = ((SolidColorBrush)this.TryFindResource("required"));
			tbTurnoverSize.Background = ((SolidColorBrush)this.TryFindResource("required"));
			tbNdsRate.Background = ((SolidColorBrush)this.TryFindResource("required"));
			tbNdsAmount.Background = ((SolidColorBrush)this.TryFindResource("required"));
			tbPriceWithTax.Background = ((SolidColorBrush)this.TryFindResource("required"));
			tbCatalogTruId.Background = ((SolidColorBrush)this.TryFindResource("required"));
			tbAdditional.Background = ((SolidColorBrush)this.TryFindResource("required"));
		}
	}
}

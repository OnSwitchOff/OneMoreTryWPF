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
	/// Interaction logic for ProductsDataGrid_SupperDesign.xaml
	/// </summary>
	public partial class ProductsDataGrid_SupperDesign : UserControl
	{
		public ProductsDataGrid_SupperDesign()
		{
			InitializeComponent();
		}		
	}

	public class Phone
	{
		public string Title { get; set; }
		public string Company { get; set; }
		public int Price { get; set; }
	}
}

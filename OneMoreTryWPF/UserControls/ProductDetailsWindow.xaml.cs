using OneMoreTryWPF.Models;
using OneMoreTryWPF.Models.ViewModels;
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
using System.Windows.Shapes;

namespace OneMoreTryWPF.UserControls
{
	/// <summary>
	/// Interaction logic for ProductDetailsWindow.xaml
	/// </summary>
	public partial class ProductDetailsWindow : Window
	{
		public ProductDetailsWindow(ProductV2 prod)
		{
			InitializeComponent();
			DataContext = new ProductDetailsViewModel(prod);
		}
	}
}

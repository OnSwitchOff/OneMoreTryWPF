using OneMoreTryWPF.Facades;
using OneMoreTryWPF.Models;
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
	/// Interaction logic for InvoicesManagerWindow.xaml
	/// </summary>
	public partial class InvoicesManagerWindow : Window
	{
		public InvoicesManagerWindow()
		{
			InitializeComponent();
			//InitDirectionComboxItems();			
			DataContext = new InvoicesManagerViewModel();
			SessionServiceOperationsFacade.StartSession();
		}

		private void Invoices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			
		}



		/*private void InitDirectionComboxItems()
		{
			direction.Items.Add("Входящие");
			direction.Items.Add("Исходящие");
		}*/
	}
}

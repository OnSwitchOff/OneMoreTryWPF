using OneMoreTryWPF.Facades;
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
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
			LoginViewModel LVM = new LoginViewModel();
			password.Password = SessionDataManagerFacade.LoadLastUserPassword();
			pin.Password = SessionDataManagerFacade.LoadLastAuthCertPin();
			DataContext = LVM;			
		}

		private void pin_PasswordChanged(object sender, RoutedEventArgs e)
		{
			var x = this.pin;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			SessionDataManagerFacade.setUserPassword(password.Password);
			if (SessionServiceOperationsFacade.StartSession())
			{
				SessionDataManagerFacade.SaveConfigKey("InvoiceKZ_lastUserPassword", SessionDataManagerFacade.Encrypt(password.Password));
				this.DialogResult = true;
			}				
		}
	}
}

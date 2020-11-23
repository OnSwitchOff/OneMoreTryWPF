using Microsoft.Win32;
using OneMoreTryWPF.Facades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace OneMoreTryWPF.Models.ViewModels
{
	public class LoginViewModel : INotifyPropertyChanged
	{
		private string authCertificatePath = SessionDataManagerFacade.LoadLastAuthCertPath();
		private bool isValidCert = false;
		private bool isFailedCert = true;
		private bool isValidPass = false;
		private string tin = String.Empty;
		
		public bool IsValidCert
		{
			get { return isValidCert; }
			set
			{
				isValidCert = value;
				IsFailedCert = !IsValidCert;
				OnPropertyChanged("IsValidCert");
				if (value)
				{
					//SessionDataManagerFacade.SaveConfigKey("InvoiceKZ_lastAuthCertPath", SessionDataManagerFacade.Encrypt(authCertificatePath));
					SessionDataManagerFacade.SaveConfigKey("InvoiceKZ_lastAuthCertPin", SessionDataManagerFacade.getAuthCertPin());
				}
			}
		}
		public bool IsFailedCert
		{
			get { return isFailedCert; }
			set
			{
				isFailedCert = value;
				OnPropertyChanged("IsFailedCert");
			}
		}
		public bool IsValidPass
		{
			get { return isValidPass; }
			set
			{
				isValidPass = value;
				OnPropertyChanged("IsValidPass");
			}
		}

		public string AuthCertificatePath
		{
			get { return authCertificatePath; }
			set
			{
				authCertificatePath = value;
				IsValidCert = false;
				OnPropertyChanged("AuthCertificatePath");
			}
		}

		public string Tin
		{
			get { return tin; }
			set
			{
				tin = value;
				OnPropertyChanged("Tin");
			}
		}

		

		public LoginViewModel()
		{

		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}


		private RelayCommand confirmCommand;
		public RelayCommand ConfirmCommand
		{
			get
			{
				return confirmCommand ??
				  (confirmCommand = new RelayCommand(obj =>
				  {
					  string pass = ((PasswordBox)obj).Password;
					  SessionDataManagerFacade.setAuthCertPin(pass);
					  Tin = GetIIN(pass);
					  SessionDataManagerFacade.setUserTin(Tin);
				  }));
			}
		}

		private string GetIIN(string pass)
		{
			string result = String.Empty;
			X509Certificate2 userAuthCertX509 = new X509Certificate2();
			try
			{
				userAuthCertX509.Import(System.IO.File.ReadAllBytes(AuthCertificatePath), pass, X509KeyStorageFlags.MachineKeySet);
				foreach (string set in userAuthCertX509.Subject.Split(','))
				{
					string[] value = set.Split('=');
					if (value[0].Trim() == "SERIALNUMBER")
					{
						result = value[1].Replace("IIN", String.Empty);
						IsValidCert = true;
						break;
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}			
			return result;
		}

		private RelayCommand browseCommand;
		public RelayCommand BrowseCommand
		{
			get
			{
				return browseCommand ??
				  (browseCommand = new RelayCommand(obj =>
				  {
					  OpenFileDialog();
					  SessionDataManagerFacade.setAuthCertPath(AuthCertificatePath);
				  }));
			}
		}

		/*private RelayCommand loginCommand;
		public RelayCommand LoginCommand
		{
			get
			{
				return loginCommand ??
				  (loginCommand = new RelayCommand(obj =>
				  {
					  
				  }));
			}
		}*/


		public bool OpenFileDialog()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Сертификат(*.p12)|*.p12";
			if (openFileDialog.ShowDialog() == true)
			{
				AuthCertificatePath = openFileDialog.FileName;
				return true;
			}
			return false;
		}
	}
}

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
	/// Interaction logic for ReasonWindow.xaml
	/// </summary>
	public partial class ReasonWindow : Window
	{
		public ReasonWindow(string str)
		{
			InitializeComponent();
			reason = str;
		}

		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		public string reason
		{
			get { return tbReason.Text; }
			set
			{
				tbReason.Text = value;
			}				
		}		
	}
}

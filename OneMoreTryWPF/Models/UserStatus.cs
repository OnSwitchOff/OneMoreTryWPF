using OneMoreTryWPF.ENUMs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class UserStatus : INotifyPropertyChanged
	{
		private object Type;
		public object type
		{
			get { return Type; }
			set
			{
				Type = value;
				OnPropertyChanged("type");
			}
		}

		private bool IsChecked;
		public bool isChecked
		{
			get { return IsChecked; }
			set
			{
				IsChecked = value;
				OnPropertyChanged("isChecked");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}

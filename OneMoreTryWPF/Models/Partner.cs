﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class Partner:INotifyPropertyChanged
	{
		private int id;
		public int Id
		{
			get { return id; }
			set
			{
				id = value;
				OnPropertyChanged("Id");
			}
		}

		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				OnPropertyChanged("Name");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

	}
}

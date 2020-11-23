using OneMoreTryWPF.Facades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class Object : INotifyPropertyChanged
	{
		private int id;
		public int Id
		{
			get { return id; }
			set
			{
				id = value;
				if (Id != 0)
				{

					Name = SessionDataManagerFacade.getObjects().Select(String.Format("ID={0} ", Id))[0]["Name"].ToString();

				}
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

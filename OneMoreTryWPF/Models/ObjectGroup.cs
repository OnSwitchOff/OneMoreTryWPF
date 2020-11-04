using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class ObjectGroup: INotifyPropertyChanged
	{
		private ObservableCollection<ObjectGroup> objectGr;
		public ObservableCollection<ObjectGroup> ObjectGr
			{
				get { return objectGr; }
				set
				{
					objectGr = value;
					OnPropertyChanged("ObjectGr");
				}
			}


	private ObservableCollection<Object> objects;
	public ObservableCollection<Object> Objects
		{
		get { return objects; }
		set
		{
			objects = value;
			OnPropertyChanged("Objects");
		}
	}

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

	private bool isSelected;
	public bool IsSelected
	{
		get { return isSelected; }
		set
		{
			isSelected = value;
			OnPropertyChanged("IsSelected");
		}
	}

	public ObjectGroup() { }

	public ObjectGroup(int _id, string _name)
	{
		ObjectGr = new ObservableCollection<ObjectGroup>();
		Objects = new ObservableCollection<Object>();
		Id = _id;
		Name = _name;
	}

	public event PropertyChangedEventHandler PropertyChanged;
	public void OnPropertyChanged(string prop = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
	}
}
}

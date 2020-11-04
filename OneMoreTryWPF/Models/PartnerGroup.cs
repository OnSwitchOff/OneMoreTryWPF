using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class PartnerGroup : INotifyPropertyChanged
	{
		private ObservableCollection<PartnerGroup> partnerGr;
		public ObservableCollection<PartnerGroup> PartnerGr
		{
			get { return partnerGr; }
			set
			{
				partnerGr = value;
				OnPropertyChanged("PartnerGr");
			}
		}


		private ObservableCollection<Partner> partners;
		public ObservableCollection<Partner> Partners
		{
			get { return partners; }
			set
			{
				partners = value;
				OnPropertyChanged("Partners");
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

		public PartnerGroup(){}

		public PartnerGroup(int _id, string _name)
		{
			PartnerGr = new ObservableCollection<PartnerGroup>();
			Partners = new ObservableCollection<Partner>();
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

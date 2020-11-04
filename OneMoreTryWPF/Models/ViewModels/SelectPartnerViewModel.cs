using Microinvest.Common;
using MicroinvestUtilityCenter;
using OneMoreTryWPF.Facades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OneMoreTryWPF.Models
{
	public class SelectPartnerViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<PartnerGroup> partnerGroups;
		public ObservableCollection<PartnerGroup> PartnerGroups
		{
			get { return partnerGroups; }
			set
			{
				partnerGroups = value;
				OnPropertyChanged("PartnerGroups");
			}
		}

		public ObservableCollection<Partner> partners;
		public ObservableCollection<Partner> Partners
		{
			get { return partners; }
			set
			{
				partners = value;
				OnPropertyChanged("Partners");
			}
		}

		private bool partnerIsSelected;
		public bool PartnerIsSelected
		{
			get { return partnerIsSelected; }
			set
			{
				partnerIsSelected = value;
				OnPropertyChanged("PartnerIsSelected");
			}
		}


		private Partner selectedPartner;
		public Partner SelectedPartner
		{
			get { return selectedPartner; }
			set
			{
				selectedPartner = value;
				if(value != null)
				{
					PartnerIsSelected = true;
				}
				OnPropertyChanged("SelectedPartner");
			}
		}
		private PartnerGroup selectedGroup;
		public PartnerGroup SelectedGroup
		{
			get { return selectedGroup; } //PartnerGroups.FirstOrDefault(i => i.IsSelected); } 
			set
			{
				selectedGroup = value;
				OnPropertyChanged("SelectedGroup");
			}
		}


		public SelectPartnerViewModel()
		{
			DataTable PartnerGroupsTable = GlobalData.WHPro.dbApp.ExecuteDataset(@"SELECT * From PartnersGroups").Tables[0];
			PartnerGroups = GetInnerGroups(PartnerGroupsTable,String.Empty);
			PartnerGroups.Insert(0, GetServiceGroup());
		}

		private PartnerGroup GetServiceGroup()
		{
			PartnerGroup serviceGroup = new PartnerGroup();
			string query = "Select * from PartnersGroups where Code ='-1'";
			DataRow[] groups = GlobalData.WHPro.dbApp.ExecuteDataset(query).Tables[0].Select();
			if (groups.Length == 1)
			{
				serviceGroup.Id = (int)groups[0]["ID"];
				serviceGroup.Name = groups[0]["Name"].ToString();
				serviceGroup.Partners = SessionDataManagerFacade.getPartnersByGroupId(serviceGroup.Id);
			}
			return serviceGroup;
		}

		private ObservableCollection<PartnerGroup> GetInnerGroups(DataTable partnerGroupsTable, string parametr)
		{
			ObservableCollection<PartnerGroup> result = new ObservableCollection<PartnerGroup>();
			parametr += "___";
			string query = String.Format("Select * from PartnersGroups where Code LIKE '{0}'", parametr);
			DataRow[] groups = GlobalData.WHPro.dbApp.ExecuteDataset(query).Tables[0].Select();
			if(groups.Length>0)
			{
				foreach (DataRow row in groups)
				{
					int groupId = (int)row["ID"];
					parametr = row["Code"].ToString();
					string groupName = row["Name"].ToString();
					PartnerGroup partnerGroup = new PartnerGroup(groupId,groupName);
					partnerGroup.Partners = SessionDataManagerFacade.getPartnersByGroupId(groupId);
					partnerGroup.PartnerGr = GetInnerGroups(partnerGroupsTable, parametr);
					result.Add(partnerGroup);
				}
			}
			return result;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		private RelayCommand setSelectedItemCommand;
		public RelayCommand SetSelectedItemCommand
		{
			get
			{
				return setSelectedItemCommand ??
				  (setSelectedItemCommand = new RelayCommand(obj =>
				  {
					  SelectedGroup = obj as PartnerGroup;
					  ObservableCollection<Partner> _partners = new ObservableCollection<Partner>();
					  SessionDataManagerFacade.FillPartnersByGroup(SelectedGroup,ref _partners);
					  Partners = _partners;
				  }));
			}
		}		
	}
}

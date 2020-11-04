using MicroinvestUtilityCenter;
using OneMoreTryWPF.Facades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class SelectObjectViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<ObjectGroup> objectGroups;
		public ObservableCollection<ObjectGroup> ObjectGroups
		{
			get { return objectGroups; }
			set
			{
				objectGroups = value;
				OnPropertyChanged("ObjectGroups");
			}
		}

		public ObservableCollection<Object> objects;
		public ObservableCollection<Object> Objects
		{
			get { return objects; }
			set
			{
				objects = value;
				OnPropertyChanged("Objects");
			}
		}

		private bool objectIsSelected;
		public bool ObjectIsSelected
		{
			get { return objectIsSelected; }
			set
			{
				objectIsSelected = value;
				OnPropertyChanged("ObjectIsSelected");
			}
		}

		private Object selectedObject;
		public Object SelectedObject
		{
			get { return selectedObject; }
			set
			{
				selectedObject = value;
				if (value != null)
				{
					ObjectIsSelected = true;
				}
				OnPropertyChanged("SelectedObject");
			}
		}


		private ObjectGroup selectedGroup;
		public ObjectGroup SelectedGroup
		{
			get { return selectedGroup; } //PartnerGroups.FirstOrDefault(i => i.IsSelected); } 
			set
			{
				selectedGroup = value;
				OnPropertyChanged("SelectedGroup");
			}
		}


		public SelectObjectViewModel()
		{
			DataTable ObjectGroupsTable = GlobalData.WHPro.dbApp.ExecuteDataset(@"SELECT * From ObjectsGroups").Tables[0];
			ObjectGroups = GetInnerGroups(ObjectGroupsTable, String.Empty);
			ObjectGroups.Insert(0, GetServiceGroup());
		}

		private ObjectGroup GetServiceGroup()
		{
			ObjectGroup serviceGroup = new ObjectGroup();
			string query = "Select * from ObjectsGroups where Code ='-1'";
			DataRow[] groups = GlobalData.WHPro.dbApp.ExecuteDataset(query).Tables[0].Select();
			if (groups.Length == 1)
			{
				serviceGroup.Id = (int)groups[0]["ID"];
				serviceGroup.Name = groups[0]["Name"].ToString();
				serviceGroup.Objects = SessionDataManagerFacade.getObjectsByGroupId(serviceGroup.Id);
			}
			return serviceGroup;
		}

		private ObservableCollection<ObjectGroup> GetInnerGroups(DataTable objectGroupsTable, string parametr)
		{
			ObservableCollection<ObjectGroup> result = new ObservableCollection<ObjectGroup>();
			parametr += "___";
			string query = String.Format("Select * from ObjectsGroups where Code LIKE '{0}'", parametr);
			DataRow[] groups = GlobalData.WHPro.dbApp.ExecuteDataset(query).Tables[0].Select();
			if (groups.Length > 0)
			{
				foreach (DataRow row in groups)
				{
					int groupId = (int)row["ID"];
					parametr = row["Code"].ToString();
					string groupName = row["Name"].ToString();
					ObjectGroup objectGroup = new ObjectGroup(groupId, groupName);
					objectGroup.Objects = SessionDataManagerFacade.getObjectsByGroupId(groupId);
					objectGroup.ObjectGr = GetInnerGroups(objectGroupsTable, parametr);
					result.Add(objectGroup);
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
					  SelectedGroup = obj as ObjectGroup;
					  ObservableCollection<Object> _objects = new ObservableCollection<Object>();
					  SessionDataManagerFacade.FillObjectsByGroup(SelectedGroup, ref _objects);
					  Objects = _objects;
				  }));
			}
		}
	}
}

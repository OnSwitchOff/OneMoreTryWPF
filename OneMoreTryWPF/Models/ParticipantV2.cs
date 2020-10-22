using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OneMoreTryWPF.Models
{
	public class ParticipantV2:INotifyPropertyChanged
	{
		//Информация по товарам (работам, услугам)+
		[XmlArrayItem(ElementName = "share")]
		public ObservableCollection<ProductShare> productShares { get; set; }

		//БИН реорганизованного лица (H 34.2)-
		private string ReorganizedTin;
		public string reorganizedTin
		{
			get { return ReorganizedTin; }
			set
			{
				ReorganizedTin = value;
				OnPropertyChanged("reorganizedTin");
			}
		}

		//ИИН/БИН участника совместной деятельности (H 34.1)+
		private string Tin;
		public string tin
		{
			get { return Tin; }
			set
			{
				Tin = value;
				OnPropertyChanged("tin");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
		public ParticipantV2() { }
	}
}

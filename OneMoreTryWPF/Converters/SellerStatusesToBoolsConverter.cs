using OneMoreTryWPF.ENUMs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OneMoreTryWPF.Converters
{
	public class SellerStatusesToBoolsConverter : IValueConverter
    {
        public object Convert(object statuses, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<bool> bools = new ObservableCollection<bool>();
            foreach (SellerType status in (SellerType[])Enum.GetValues(typeof(SellerType)))
			{
                bools.Add(((ObservableCollection<SellerType>)statuses).Contains(status));
			}
            return bools;
            //return  bools[(int)Enum.Parse(typeof(SellerType),parameter.ToString())];
        }

        public object ConvertBack(object bools, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<SellerType> statuses = new ObservableCollection<SellerType>();
			for (int i = 0; i < ((ObservableCollection<bool>)bools).Count; i++)
			{
                if(((ObservableCollection<bool>)bools)[i])
				{
                    statuses.Add((SellerType)i);
				}
			}
            return statuses;
        }
    }
}

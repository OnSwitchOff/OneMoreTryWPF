using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OneMoreTryWPF.Converters
{
	public class CodeToMeasureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = String.Empty;
            if(value!=null)
			switch(value.ToString())
            {
                case "796":
                    result = "шт.";
                    break;
                case "166":
                    result = "кг.";
                    break;
                case "868":
                    result = "бут.";
                    break;
                case "112":
                    result = "л.";
                    break;
                case "006":
                    result = "м.";
                    break;
                default:
                    result= "шт.";
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string code = "796";
            switch (value.ToString())
            {
                case "кг.":
                case "кг":
                    code = "166";
                    break;

                case "шт.":
                case "шт":
                    code = "796";
                    break;

                case "бут.":
                case "бут":
                    code = "868";
                    break;

                case "л.":
                case "л":
                    code = "112";
                    break;
                case "м.":
                case "м":
                    code = "006";
                    break;
            }
            return code;
        }
    }
}

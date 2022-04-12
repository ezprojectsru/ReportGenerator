using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using ReportGenerator.DataBase.Controls;

namespace ReportGenerator.DataBase.Converters
{
    public class UserIdFullNameConverter : IValueConverter
    {
        private UserControl _userControl = new UserControl();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            int index = System.Convert.ToInt32(value);
            string name = _userControl.GetFullNameById(index);
            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            string name = System.Convert.ToString(value);
            int index = _userControl.GetIddByFullName(name);
            return index;
        }
    }
}

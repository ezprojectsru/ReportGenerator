using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using ReportGenerator.DataBase.Controls;

namespace ReportGenerator.DataBase.Converters
{
    public class ProjectStatusConverter : IValueConverter
    {
        private ProjectStatusControl _projectStatusControl = new ProjectStatusControl();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            int index = System.Convert.ToInt32(value);
            string name = _projectStatusControl.GetNameById(index);
            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            string name = System.Convert.ToString(value);
            int index = _projectStatusControl.GetIdByName(name);
            return index;
        }
    }
}

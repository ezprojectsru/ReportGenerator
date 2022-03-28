using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using ReportGenerator.DataBase.Controls;

namespace ReportGenerator.DataBase.Converters
{
    public class TaskTypeIdConverter : IValueConverter
    {
        private TaskTypeControl _taskTypeControl = new TaskTypeControl();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            int index = System.Convert.ToInt32(value);
            string shortName = _taskTypeControl.GetShortNameById(index);
            return shortName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            string shortName = System.Convert.ToString(value);
            int index = _taskTypeControl.GetIdByShortName(shortName);
            return index;
        }
    }
}

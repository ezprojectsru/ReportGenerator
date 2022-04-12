using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;

namespace ReportGenerator.DataBase.Converters
{
    public class TaskTypeConverter : IValueConverter
    {
        
            private TaskTypeControl _taskTypeControl = new TaskTypeControl();
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is null) return null;
                TaskType tt = _taskTypeControl.GetTaskTypeById(System.Convert.ToInt32(value));
                return tt;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is null) return null;
                TaskType tt = value as TaskType;
                int id = tt.id;

                return id;
            }
        
    }
}

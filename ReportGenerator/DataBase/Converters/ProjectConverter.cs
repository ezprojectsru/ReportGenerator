using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;

namespace ReportGenerator.DataBase.Converters
{
    public class ProjectConverter : IValueConverter
    {
        private ProjectControl _projectControl = new ProjectControl();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            Project project = _projectControl.GetProjectById(System.Convert.ToInt32(value));
            return project;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            Project project = value as Project;
            int id = project.Id;
            
            return id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;

namespace ReportGenerator.DataBase.Converters
{
    public class ProjectStatusListConverter : IValueConverter
    {
        private ProjectStatusControl _projectStatusControl = new ProjectStatusControl();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            ProjectStatus ps = _projectStatusControl.GetProjectStatusById(System.Convert.ToInt32(value));
            return ps;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            ProjectStatus ps = value as ProjectStatus;
            int id = ps.Id;

            return id;
        }
    }
}

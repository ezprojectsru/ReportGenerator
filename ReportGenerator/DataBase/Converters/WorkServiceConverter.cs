using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;

namespace ReportGenerator.DataBase.Converters
{
    public class WorkServiceConverter : IValueConverter
    {
        private WorkServiceControl _workServiceControl = new WorkServiceControl();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            WorkService ws = _workServiceControl.GetServiceById(System.Convert.ToInt32(value));
            return ws;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            WorkService ws = value as WorkService;
            int id = ws.ID;

            return id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using ReportGenerator.DataBase.Models;

namespace ReportGenerator.DataBase.Converters
{
    public class DetailSourceConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var masterRowId = values[0];
            var childData = (DataTable)values[1];
            var childView = new DataView(childData);
            childView.RowFilter = string.Format("projectId = '{0}'", masterRowId.ToString());
            return childView;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

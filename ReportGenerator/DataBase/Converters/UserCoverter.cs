using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using ReportGenerator.DataBase.Controls;
using ReportGenerator.DataBase.Models;

namespace ReportGenerator.DataBase.Converters
{
    public class UserCoverter : IValueConverter
    {
        private UserControl _userControl = new UserControl();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            User user = _userControl.GetUserById(System.Convert.ToInt32(value));
            return user;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            User user = value as User;
            int id = user.id;

            return id;
        }
    }
}

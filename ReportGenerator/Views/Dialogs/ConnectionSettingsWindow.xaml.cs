using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ReportGenerator.ViewModels;

namespace ReportGenerator.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ConnectionSettingsWindow.xaml
    /// </summary>
    public partial class ConnectionSettingsWindow : Window
    {
        public ConnectionSettingsWindowVM ConnectionSettingsWindowVM { get; }
        public ConnectionSettingsWindow(ConnectionSettingsWindowVM connectionSettingsWindowVM)
        {
            ConnectionSettingsWindowVM = connectionSettingsWindowVM;
            InitializeComponent();
        }
    }
}

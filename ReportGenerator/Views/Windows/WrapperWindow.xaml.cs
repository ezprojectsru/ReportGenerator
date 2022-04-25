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

namespace ReportGenerator.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для WrapperWindow.xaml
    /// </summary>
    public partial class WrapperWindow : Window
    {
        public WrapperWindowVM WrapperWindowVM { get; }
        public WrapperWindow(WrapperWindowVM wrapperWindowVM)
        {
            WrapperWindowVM = wrapperWindowVM;
            InitializeComponent();
        }
        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReportGenerator.ViewModels;

namespace ReportGenerator.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public ProjectPageVM ProjectPageVM { get; }
        public ProjectPage(ProjectPageVM projectPageVM)
        {
            ProjectPageVM = projectPageVM;
            InitializeComponent();
        }
    }
}

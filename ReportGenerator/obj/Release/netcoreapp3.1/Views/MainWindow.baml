   M S B A M L   `   `   `  ����   ReportGenerator&     ReportGenerator.Views.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Z VDevExpress.Mvvm.UI, Version=21.1.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  
MainWindow�� � DataContext ��    	VmLocator �� Source# [   �� Path$ MainWindowViewModel�.���  � ColumnDefinitions �� $	��200 ��� $��* �.+��� $��0�����#008db1�� $��0�� �� Orientation$ Vertical=�.+��� $	��200��$��76��$��0,10,0,0q�$��Center=�$��../Resources/logo.png���� $
��15,0q�$
��Left=�$��14(�$
��Bold"�
�����������  �� UpdateSourceTrigger$ PropertyChanged=�$ FullName��� $
��Auto��$
��15,0q�$
��Left=�$��14(�
�������$
��Wrap=����� $ PropertyChanged=�$ Departament��� $
��15,0q�$
��Left=�$��14(�
�������$
��Wrap=����� $ PropertyChanged=�$
 Role��� $��10q�$ 
Horizontal=�.+��� $��80��$��5q�$��10q�$
��Left=�$��Профиль���� $��80��$��5q�$��10q�$��Right=�$��
Выйти���� $��10q��� $	��150��$��5q�$��10q�$��
Планы������ *OpenPlansPage+�� $	��150��$��5q�$��10q�$��Отчеты���� $	��150��$��5q�$��10q�$��Задачи���� $	��150��$��5q�$��10q�$��Управление������ *OpenAppManagerPage+ � $��1��  � NavigationUIVisibility$ Hidden=����� *CurrentPage+
   M S B A M L   `   `   `  ����  ReportGenerator&     ReportGenerator.Views.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Z VDevExpress.Mvvm.UI, Version=21.1.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  
MainWindow��5       � DataContext 5
      ��    	VmLocator �� Source# [   �� Path$ MainWindowViewModel�.��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ���#008db16$   �� 5   
   $��0��6    �� Orientation$ Vertical=�6%   .+�5      �� $	��200��5      $��76��5      $��0,10,0,0q�5      $��Center=�5      $��../Resources/logo.png��5      �� 5      $
��15,0q�5      $
��Left=�5      $��14(�5      $
��Bold"�5      
���������5!      ��  �� UpdateSourceTrigger$ PropertyChanged=�$ FullName��� 5#      $
��Auto��5$      $
��15,0q�5%      $
��Left=�5&      $��14(�5'      
�������$
��Wrap=�5*      ��5)      �� $ PropertyChanged=�$ Departament�5*      �� 5,      $
��15,0q�5-      $
��Left=�5.      $��14(�5/      
�������$
��Wrap=�52      ��51      �� $ PropertyChanged=�$
 Role�52      �� 54      $��10q�6   $ 
Horizontal=�6%   .+�56      �� $��80��57      $��5q�58      $��10q�59      $
��Left=�5:      $��Профиль��5;      �� 5<      $��80��5=      $��5q�5>      $��10q�5?      $��Right=�5@      $��
Выйти��5A      5C      �� 5D      $��10q�6   �� 5E      $	��150��5F      $��5q�5G      $��10q�5H      $��
Планы��5J      ��5I      �� *OpenPlansPage+5J      �� 5K      $	��150��5L      $��5q�5M      $��10q�5N      $��Отчеты��5O      �� 5P      $	��150��5Q      $��5q�5R      $��10q�5S      $��Задачи��5T      �� 5U      $	��150��5V      $��5q�5W      $��10q�5X      $��Управление��5Z      ��5Y      �� *OpenAppManagerPage+5Z      5[       � 5^   
   $��1��5_        � NavigationUIVisibility$ Hidden=�5a      ��5`      �� *CurrentPage+5a      5b      5c      
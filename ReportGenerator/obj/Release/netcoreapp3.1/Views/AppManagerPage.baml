   M S B A M L   `   `   `  ����   ReportGenerator*    $ReportGenerator.Views.AppManagerPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Z VDevExpress.Mvvm.UI, Version=21.1.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       N� Title$  AppManagerPage�� � DataContext ��    	VmLocator �� Source# [   �� Path$ AppManagerPageViewModel�.��� .+��� 
��������� $��10q�.@��� -    ��Name$ settings������ $��3q�)'Настройки приложения���  � ColumnDefinitions �� $��2* ��� $��2* �.+��� $	��250��$��5,5,5,0q�$	��Top=�$.��(Подключение к серверу��.���� $	��300��$��5q�$��Center=� �� Orientation$ Vertical=�.+��� $��Адрес сервера���� �� $,��&Название базы данных���� �� $%��Имя пользователя���� �� $��Пароль���� �� $��30q�$��Center=�$
��Bold"�$6��0== Поля пока не привязаны! ==���� $��1��$��10q�$��5q�$��Right=�$��Bottom=�$��Сохранить���� -   $ users������ $��3q�Пользователи���� $��10q�.+��� .+��� $��Right=�$ 
Horizontal=�.+��� $��5q�$��5q�$��+ Создать������ *OpenCreateNewUserWindow+�� $��5q�$��5q�$ ��Редактировать������ *OpenEditSelectedUserWindow+�� $��5q�$��5q�$��Удалить������ *DeleteSelectedUser+�� $��10q����� *Users+ �� SelectedItem �� *UserSelected+ �� View �� .P��� 	 �� Width$	 0��$��ID��
 �� DisplayMemberBinding
 �� *Id+�� $		 100��$��
Логин��
 �� *
Username+�� $	 70��$%��Дата регистрации��
 �� *Сreate_Date+�� $		 120��$��ФИО��
 �� *
FullName+�� $		 100��$��Email��
 �� *Email+�� $		 100��$��Должность��
 �� *Role+�� $		 100��$��
Отдел��
 �� *Departament+�� $	 50��$��Сектор��
 �� *Sector+�� $		 100��$��Группа��
 �� *Group+�� -   $ departaments������ $��3q�Отделы���� $��10q�.+��� $��Right=�$ 
Horizontal=�.+��� $��5q�$��5q�$��+ Создать������ * OpenCreateNewDepartamentWindow+�� $��5q�$��5q�$ ��Редактировать������ *#!OpenEditSelectedDepartamentWindow+�� $��5q�$��5q�$��Удалить������ *DeleteSelectedDepartament+�� $��10q����� *ListDepartaments+ �� *DepartamentSelected+ �� .P��� $	 0��$��ID��
 �� *id+�� $		 150��$��Название��
 �� *name+�� $		 150��$��Сокращение��
 �� *	shortName+�� $		 200��$��Описание��
 �� *description+�� $��	0,100,0,0q�$��Center=�$��18(�$
��Bold"�$e��_=>Тут будет управление Типами с привязкой к Отделам=>��$
��Wrap=��� -   $ tables������ $��3q�Таблицы���� $��	0,100,0,0q�$��Center=�$��18(�$
��Bold"�$e��_=>Тут будет управление Секторами, Ролями и Группами=>��$
��Wrap=�
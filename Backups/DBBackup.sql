USE [rg_maindb]
GO
/****** Object:  Table [dbo].[departaments]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departaments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[shortName] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_departaments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[plans]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plans](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[startDate] [datetime] NOT NULL,
	[finishDate] [datetime] NOT NULL,
	[responsibleId] [int] NOT NULL,
	[directorId] [int] NOT NULL,
	[comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_plans] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reports]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reports](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[createDate] [datetime] NOT NULL,
	[planId] [int] NOT NULL,
	[actualIntensity] [int] NOT NULL,
	[actualCompletion] [int] NOT NULL,
 CONSTRAINT [PK_reports] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sectors]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sectors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_sectors] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[taskLists]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[taskLists](
	[id] [int] NOT NULL,
	[taskId] [int] NOT NULL,
	[planId] [int] NOT NULL,
 CONSTRAINT [PK_taskLists] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tasks]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[planId] [int] NOT NULL,
	[priority] [int] NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[intensity] [int] NOT NULL,
	[startcompletion] [int] NOT NULL,
	[plancompletion] [int] NOT NULL,
	[comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_tasks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[types]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[shortName] [nvarchar](50) NOT NULL,
	[departamentId] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 21.03.2022 11:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[create_date] [datetime2](7) NOT NULL,
	[fullName] [nvarchar](max) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[departamentId] [int] NOT NULL,
	[roleId] [int] NOT NULL,
	[sectorId] [int] NOT NULL,
	[groupId] [int] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[departaments] ON 

INSERT [dbo].[departaments] ([id], [name], [shortName], [description]) VALUES (3, N'Нет отдела', N'Нет отдела', NULL)
INSERT [dbo].[departaments] ([id], [name], [shortName], [description]) VALUES (4, N'Отдел информационных технологий', N'ОИТ', NULL)
SET IDENTITY_INSERT [dbo].[departaments] OFF
GO
SET IDENTITY_INSERT [dbo].[groups] ON 

INSERT [dbo].[groups] ([id], [name], [description]) VALUES (1, N'Нет группы', NULL)
INSERT [dbo].[groups] ([id], [name], [description]) VALUES (2, N'Общее руководство', NULL)
INSERT [dbo].[groups] ([id], [name], [description]) VALUES (3, N'Группа разработки программного обеспечения', NULL)
SET IDENTITY_INSERT [dbo].[groups] OFF
GO
SET IDENTITY_INSERT [dbo].[plans] ON 

INSERT [dbo].[plans] ([id], [name], [startDate], [finishDate], [responsibleId], [directorId], [comment]) VALUES (1, N'Report Generator Project', CAST(N'2022-03-14T12:32:06.427' AS DateTime), CAST(N'2022-03-18T16:32:06.427' AS DateTime), 1, 3, N'Старт работы над новым проектом')
INSERT [dbo].[plans] ([id], [name], [startDate], [finishDate], [responsibleId], [directorId], [comment]) VALUES (2, N'Книги C# .Net', CAST(N'2022-03-14T12:00:06.427' AS DateTime), CAST(N'2022-03-18T16:30:06.427' AS DateTime), 1, 1, N'Список книг для чтения на неделю')
INSERT [dbo].[plans] ([id], [name], [startDate], [finishDate], [responsibleId], [directorId], [comment]) VALUES (3, N'Dapper', CAST(N'2022-03-14T12:00:06.427' AS DateTime), CAST(N'2022-03-18T16:32:06.427' AS DateTime), 1, 1, N'Изучть принципы работы с фреймворком')
INSERT [dbo].[plans] ([id], [name], [startDate], [finishDate], [responsibleId], [directorId], [comment]) VALUES (4, N'Архитектура Report Generator', CAST(N'2022-03-14T12:00:06.427' AS DateTime), CAST(N'2022-03-18T16:32:06.427' AS DateTime), 1, 1, N'Проработать первую итерацию архитектуры')
INSERT [dbo].[plans] ([id], [name], [startDate], [finishDate], [responsibleId], [directorId], [comment]) VALUES (5, N'Рефакторинг RG', CAST(N'2022-03-21T00:00:00.000' AS DateTime), CAST(N'2022-03-25T00:00:00.000' AS DateTime), 1, 1, N'Улучшить код')
SET IDENTITY_INSERT [dbo].[plans] OFF
GO
SET IDENTITY_INSERT [dbo].[roles] ON 

INSERT [dbo].[roles] ([id], [name]) VALUES (3, N'Нет должности')
INSERT [dbo].[roles] ([id], [name]) VALUES (4, N'Администратор')
INSERT [dbo].[roles] ([id], [name]) VALUES (5, N'Менеджер')
INSERT [dbo].[roles] ([id], [name]) VALUES (6, N'Сотрудник')
INSERT [dbo].[roles] ([id], [name]) VALUES (7, N'Практикант')
SET IDENTITY_INSERT [dbo].[roles] OFF
GO
SET IDENTITY_INSERT [dbo].[sectors] ON 

INSERT [dbo].[sectors] ([id], [name], [description]) VALUES (1, N'Нет сектора', NULL)
INSERT [dbo].[sectors] ([id], [name], [description]) VALUES (2, N'Информационная безопасность', N'Сектор информационной безопасности')
SET IDENTITY_INSERT [dbo].[sectors] OFF
GO
SET IDENTITY_INSERT [dbo].[tasks] ON 

INSERT [dbo].[tasks] ([id], [name], [planId], [priority], [type], [intensity], [startcompletion], [plancompletion], [comment]) VALUES (3, N'Report Generator. Реализация каркаса нового приложения', 1, 1, N'ТП', 8, 10, 100, N'Реализовать каркас нового приложения согласно собственным представлениям о необходимых ему свойствах')
INSERT [dbo].[tasks] ([id], [name], [planId], [priority], [type], [intensity], [startcompletion], [plancompletion], [comment]) VALUES (4, N'Report Generator. Реализация формы настрочных параметров', 1, 3, N'ПР', 8, 0, 50, N'Начать реализацию требования согласно ТЗ')
INSERT [dbo].[tasks] ([id], [name], [planId], [priority], [type], [intensity], [startcompletion], [plancompletion], [comment]) VALUES (5, N'Report Generator. Реализация главной формы (просмотр задач, понедельный календарь)', 1, 4, N'ПР', 0, 0, 10, N'Начать реализацию требования согласно ТЗ, если останется время')
INSERT [dbo].[tasks] ([id], [name], [planId], [priority], [type], [intensity], [startcompletion], [plancompletion], [comment]) VALUES (6, N'Изучение литературы по разработке на C#/.Net', 1, 2, N'ДР', 4, 0, 0, N'Изучение литературы - планомерное или по необходимости')
INSERT [dbo].[tasks] ([id], [name], [planId], [priority], [type], [intensity], [startcompletion], [plancompletion], [comment]) VALUES (7, N'Резерв времени', 1, 5, N'ТП', 4, 0, 0, N'1ч на планирование и отчетность, остальное проектные задачи или изучение литературы.')
INSERT [dbo].[tasks] ([id], [name], [planId], [priority], [type], [intensity], [startcompletion], [plancompletion], [comment]) VALUES (10, N'Книга 1', 2, 2, N'ДР', 2, 0, 10, N'Первая книга')
INSERT [dbo].[tasks] ([id], [name], [planId], [priority], [type], [intensity], [startcompletion], [plancompletion], [comment]) VALUES (16, N'Книга 2', 2, 2, N'ДР', 2, 0, 15, N'Тест')
INSERT [dbo].[tasks] ([id], [name], [planId], [priority], [type], [intensity], [startcompletion], [plancompletion], [comment]) VALUES (17, N'Изучить основы', 3, 1, N'ТП', 3, 0, 50, N'Тест Dapper')
SET IDENTITY_INSERT [dbo].[tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[types] ON 

INSERT [dbo].[types] ([id], [name], [shortName], [departamentId], [description]) VALUES (1, N'Поддержка', N'ТП', 4, N'Любые вопросы требующие оперативных изменений продуктов и модулей')
INSERT [dbo].[types] ([id], [name], [shortName], [departamentId], [description]) VALUES (2, N'Проектная деятельность', N'ПР', 4, N'Все что относится к выпуску продуктов и модулей')
INSERT [dbo].[types] ([id], [name], [shortName], [departamentId], [description]) VALUES (3, N'Непроектная деятельность', N'НП', 4, N'Консультации сотрудников внутри и вне ОИТ не относящийся непосредственно к ПР')
INSERT [dbo].[types] ([id], [name], [shortName], [departamentId], [description]) VALUES (4, N'Другое', N'ДР', 4, N'Все остальное, в том числе производственные совещания, общая отчетность, обучение')
SET IDENTITY_INSERT [dbo].[types] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [username], [password], [create_date], [fullName], [email], [departamentId], [roleId], [sectorId], [groupId]) VALUES (1, N'user1', N'12345', CAST(N'2022-03-15T00:00:00.0000000' AS DateTime2), N'Иванов Иван', N'newtest1@mail.ru', 4, 4, 1, 3)
INSERT [dbo].[users] ([id], [username], [password], [create_date], [fullName], [email], [departamentId], [roleId], [sectorId], [groupId]) VALUES (3, N'user3', N'12345', CAST(N'2022-03-15T15:35:06.4266667' AS DateTime2), N'Петров Петр', N'test3@mail.ru', 4, 4, 1, 2)
INSERT [dbo].[users] ([id], [username], [password], [create_date], [fullName], [email], [departamentId], [roleId], [sectorId], [groupId]) VALUES (4, N'user2', N'12345', CAST(N'2022-03-15T15:45:06.4266667' AS DateTime2), N'Сидоров Аркадий', N'test2@mail.ru', 4, 4, 1, 3)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[reports] ADD  CONSTRAINT [DF_reports_actualIntensity]  DEFAULT ((0)) FOR [actualIntensity]
GO
ALTER TABLE [dbo].[reports] ADD  CONSTRAINT [DF_reports_actualCompletion]  DEFAULT ((100)) FOR [actualCompletion]
GO
ALTER TABLE [dbo].[tasks] ADD  CONSTRAINT [DF_tasks_name]  DEFAULT ('Без названия') FOR [name]
GO
ALTER TABLE [dbo].[tasks] ADD  CONSTRAINT [DF_tasks_priority]  DEFAULT ((1)) FOR [priority]
GO
ALTER TABLE [dbo].[tasks] ADD  CONSTRAINT [DF_tasks_type]  DEFAULT ('Нет') FOR [type]
GO
ALTER TABLE [dbo].[tasks] ADD  CONSTRAINT [DF_tasks_intensity]  DEFAULT ((0)) FOR [intensity]
GO
ALTER TABLE [dbo].[tasks] ADD  CONSTRAINT [DF_tasks_startcompletion]  DEFAULT ((0)) FOR [startcompletion]
GO
ALTER TABLE [dbo].[tasks] ADD  CONSTRAINT [DF_tasks_plancompletion]  DEFAULT ((100)) FOR [plancompletion]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_fullName]  DEFAULT ('нет данных') FOR [fullName]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_email]  DEFAULT ('нет данных') FOR [email]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_departamentId]  DEFAULT ((1)) FOR [departamentId]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_roleId]  DEFAULT ((1)) FOR [roleId]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_sectorId]  DEFAULT ((1)) FOR [sectorId]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_groupId]  DEFAULT ((1)) FOR [groupId]
GO
ALTER TABLE [dbo].[plans]  WITH CHECK ADD  CONSTRAINT [FK_plans_users] FOREIGN KEY([directorId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[plans] CHECK CONSTRAINT [FK_plans_users]
GO
ALTER TABLE [dbo].[plans]  WITH CHECK ADD  CONSTRAINT [FK_plans_users1] FOREIGN KEY([responsibleId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[plans] CHECK CONSTRAINT [FK_plans_users1]
GO
ALTER TABLE [dbo].[reports]  WITH CHECK ADD  CONSTRAINT [FK_reports_plans] FOREIGN KEY([planId])
REFERENCES [dbo].[plans] ([id])
GO
ALTER TABLE [dbo].[reports] CHECK CONSTRAINT [FK_reports_plans]
GO
ALTER TABLE [dbo].[tasks]  WITH CHECK ADD  CONSTRAINT [FK_tasks_plans] FOREIGN KEY([planId])
REFERENCES [dbo].[plans] ([id])
GO
ALTER TABLE [dbo].[tasks] CHECK CONSTRAINT [FK_tasks_plans]
GO
ALTER TABLE [dbo].[types]  WITH CHECK ADD  CONSTRAINT [FK_types_departaments] FOREIGN KEY([departamentId])
REFERENCES [dbo].[departaments] ([id])
GO
ALTER TABLE [dbo].[types] CHECK CONSTRAINT [FK_types_departaments]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_departaments] FOREIGN KEY([departamentId])
REFERENCES [dbo].[departaments] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_departaments]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_groups] FOREIGN KEY([groupId])
REFERENCES [dbo].[groups] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_groups]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_roles] FOREIGN KEY([roleId])
REFERENCES [dbo].[roles] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_roles]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_sectors] FOREIGN KEY([sectorId])
REFERENCES [dbo].[sectors] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_sectors]
GO

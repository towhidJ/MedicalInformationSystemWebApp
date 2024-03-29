USE [master]
GO
/****** Object:  Database [MedicalInformationSystem]    Script Date: 10/28/2019 12:20:56 AM ******/
CREATE DATABASE [MedicalInformationSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MedicalInformationSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.JOYA\MSSQL\DATA\MedicalInformationSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MedicalInformationSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.JOYA\MSSQL\DATA\MedicalInformationSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MedicalInformationSystem] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MedicalInformationSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MedicalInformationSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MedicalInformationSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MedicalInformationSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MedicalInformationSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MedicalInformationSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [MedicalInformationSystem] SET  MULTI_USER 
GO
ALTER DATABASE [MedicalInformationSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MedicalInformationSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MedicalInformationSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MedicalInformationSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MedicalInformationSystem] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MedicalInformationSystem', N'ON'
GO
ALTER DATABASE [MedicalInformationSystem] SET QUERY_STORE = OFF
GO
USE [MedicalInformationSystem]
GO
/****** Object:  Table [dbo].[AdminTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminTB](
	[AdminId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_AdminTB] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppointmentTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentTB](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Type] [varchar](50) NULL,
	[DoctorId] [int] NOT NULL,
	[AppointmentDate] [date] NOT NULL,
	[AppointmentNumber] [varchar](100) NULL,
	[AppointmentFee] [float] NULL,
	[Age] [int] NULL,
 CONSTRAINT [PK_AppointmentTB] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[BillNo] [varchar](50) NOT NULL,
	[DoctorFee] [float] NOT NULL,
	[MedicalFee] [float] NOT NULL,
	[MedicineFee] [float] NOT NULL,
	[Testfee] [float] NULL,
	[TotalAmmount] [float] NOT NULL,
 CONSTRAINT [PK_BillTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DepartmentTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_DepartmentTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DesignationTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DesignationTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DesignationName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_DesignationTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorTB](
	[DoctorId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[SpealizationId] [int] NOT NULL,
	[DesignationId] [int] NOT NULL,
	[RoleId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[Address] [varchar](150) NULL,
	[Phone] [varchar](14) NULL,
	[Dob] [date] NOT NULL,
	[Gender] [varchar](6) NOT NULL,
	[ImagePath] [varchar](250) NULL,
	[Joined] [date] NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[VisitingTimeStart] [varchar](50) NULL,
	[VisitingTimeEnd] [varchar](50) NULL,
	[VisitDay] [varchar](200) NULL,
 CONSTRAINT [PK_DoctorTB] PRIMARY KEY CLUSTERED 
(
	[DoctorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NurseTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NurseTB](
	[NurseId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Address] [varchar](150) NULL,
	[Phone] [varchar](14) NOT NULL,
	[Dob] [date] NOT NULL,
	[Qualification] [varchar](150) NOT NULL,
	[Joined] [date] NOT NULL,
	[ImagePath] [varchar](250) NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](300) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Gender] [varchar](6) NOT NULL,
 CONSTRAINT [PK_NurseTB] PRIMARY KEY CLUSTERED 
(
	[NurseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientReport]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientReport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[PrescribeMedecine] [varchar](400) NULL,
	[PrecribeSurgery] [varchar](50) NULL,
 CONSTRAINT [PK_PatientReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Age] [int] NOT NULL,
	[Address] [varchar](50) NULL,
	[AdmitDate] [date] NOT NULL,
	[WardId] [int] NOT NULL,
	[SeatId] [int] NOT NULL,
	[Problem] [varchar](250) NULL,
	[ImagePath] [varchar](150) NULL,
	[Gender] [varchar](7) NOT NULL,
 CONSTRAINT [PK_PatientTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrescribeTestTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrescribeTestTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[RefferDoctorId] [int] NULL,
	[TestName] [varchar](200) NOT NULL,
 CONSTRAINT [PK_PrescribeTestTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceptionTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceptionTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Phone] [varchar](50) NULL,
	[Address] [varchar](150) NOT NULL,
	[Dob] [date] NOT NULL,
	[Gender] [varchar](10) NULL,
	[ImagePath] [varchar](250) NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](300) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Joined] [date] NOT NULL,
 CONSTRAINT [PK_ReceptionTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_RoleTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeatTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WardId] [int] NOT NULL,
	[SeatNo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SeatTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpealizationTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpealizationTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Spealization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Dob] [date] NOT NULL,
	[Joined] [date] NULL,
	[Address] [varchar](250) NULL,
	[ImagePath] [varchar](250) NULL,
	[RoleId] [int] NOT NULL,
	[Phone] [varchar](14) NOT NULL,
	[Gender] [varchar](7) NOT NULL,
 CONSTRAINT [PK_StaffTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestRepaortTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestRepaortTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Report] [varchar](50) NULL,
	[TestId] [int] NULL,
 CONSTRAINT [PK_TestRepaortTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestDate] [date] NOT NULL,
	[DeliveryDate] [date] NOT NULL,
	[PrescribeTestId] [int] NOT NULL,
	[TestFee] [float] NOT NULL,
 CONSTRAINT [PK_TestTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WardTB]    Script Date: 10/28/2019 12:20:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WardTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WardNo] [varchar](50) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[SeatQuentity] [int] NOT NULL,
	[AvailableSeat] [int] NOT NULL,
	[TotalSeat] [int] NOT NULL,
 CONSTRAINT [PK_WardTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AdminTB] ON 

INSERT [dbo].[AdminTB] ([AdminId], [Name], [Email], [Password], [RoleId]) VALUES (1, N'Towhid', N'admin@admin.com', N'MTIzNDU2', 1)
SET IDENTITY_INSERT [dbo].[AdminTB] OFF
SET IDENTITY_INSERT [dbo].[AppointmentTB] ON 

INSERT [dbo].[AppointmentTB] ([AppointmentId], [Name], [Type], [DoctorId], [AppointmentDate], [AppointmentNumber], [AppointmentFee], [Age]) VALUES (4, N'Joya', NULL, 2003, CAST(N'2019-10-28' AS Date), N'Med-20191000', 500, 21)
INSERT [dbo].[AppointmentTB] ([AppointmentId], [Name], [Type], [DoctorId], [AppointmentDate], [AppointmentNumber], [AppointmentFee], [Age]) VALUES (5, N'Shahin', NULL, 2003, CAST(N'2019-10-28' AS Date), N'Med-20191001', NULL, 22)
INSERT [dbo].[AppointmentTB] ([AppointmentId], [Name], [Type], [DoctorId], [AppointmentDate], [AppointmentNumber], [AppointmentFee], [Age]) VALUES (7, N'Henry mart', N'r', 2003, CAST(N'2019-10-28' AS Date), N'Med-20191002', NULL, 23)
INSERT [dbo].[AppointmentTB] ([AppointmentId], [Name], [Type], [DoctorId], [AppointmentDate], [AppointmentNumber], [AppointmentFee], [Age]) VALUES (8, N'Abdul', NULL, 2003, CAST(N'2019-10-28' AS Date), N'Med-20191003', NULL, 20)
INSERT [dbo].[AppointmentTB] ([AppointmentId], [Name], [Type], [DoctorId], [AppointmentDate], [AppointmentNumber], [AppointmentFee], [Age]) VALUES (9, N'Shahin', NULL, 6, CAST(N'2019-10-28' AS Date), N'Med-20191000', NULL, NULL)
INSERT [dbo].[AppointmentTB] ([AppointmentId], [Name], [Type], [DoctorId], [AppointmentDate], [AppointmentNumber], [AppointmentFee], [Age]) VALUES (10, N'Shahin', NULL, 2003, CAST(N'2019-10-28' AS Date), N'Med-20191004', NULL, 22)
INSERT [dbo].[AppointmentTB] ([AppointmentId], [Name], [Type], [DoctorId], [AppointmentDate], [AppointmentNumber], [AppointmentFee], [Age]) VALUES (11, N'Joya', NULL, 2003, CAST(N'2019-10-29' AS Date), N'Med-20191000', NULL, 22)
INSERT [dbo].[AppointmentTB] ([AppointmentId], [Name], [Type], [DoctorId], [AppointmentDate], [AppointmentNumber], [AppointmentFee], [Age]) VALUES (12, N'Towhid', N'Eye ', 2003, CAST(N'2019-10-30' AS Date), N'Med-20191000', 500, NULL)
INSERT [dbo].[AppointmentTB] ([AppointmentId], [Name], [Type], [DoctorId], [AppointmentDate], [AppointmentNumber], [AppointmentFee], [Age]) VALUES (13, N'Towhid', N'Eye', 2003, CAST(N'2019-10-29' AS Date), N'Med-20191001', 500, 22)
SET IDENTITY_INSERT [dbo].[AppointmentTB] OFF
SET IDENTITY_INSERT [dbo].[DepartmentTB] ON 

INSERT [dbo].[DepartmentTB] ([Id], [DepartmentName]) VALUES (1, N'Medicine')
SET IDENTITY_INSERT [dbo].[DepartmentTB] OFF
SET IDENTITY_INSERT [dbo].[DesignationTB] ON 

INSERT [dbo].[DesignationTB] ([Id], [DesignationName]) VALUES (1, N'MBBS')
INSERT [dbo].[DesignationTB] ([Id], [DesignationName]) VALUES (2, N'MBBS,FCPS')
SET IDENTITY_INSERT [dbo].[DesignationTB] OFF
SET IDENTITY_INSERT [dbo].[DoctorTB] ON 

INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (4, 1, 1, 2, 2, N'Towhid', N'shikalbaha', N'01861053900', CAST(N'1998-03-19' AS Date), N'Male', N'1208988376Information-systems-life-cycle-development-phase-information.jpg', CAST(N'2018-03-04' AS Date), N'towhid@gmail.com', N'123456', NULL, NULL, NULL)
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (5, 1, 1, 2, 2, N'Abdul', N'shikalbaha', N'01861053900', CAST(N'1998-03-26' AS Date), N'Male', N'150463676746-467204_horse-sketch-shower-curtain-clipart.png', CAST(N'2018-03-06' AS Date), N'abdul@abdul.com', N'123456', N'9:10 PM', N'9:10 PM', NULL)
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (6, 1, 1, 1, 2, N'Towhid', N'shikalbaha', N'01861053900', CAST(N'1998-03-04' AS Date), N'Male', N'962983482doctor.jpg', CAST(N'2018-03-04' AS Date), N'towhidul@gmail.com', N'123', N'10:36 PM', N'11:36 PM', NULL)
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (1002, 1, 2, 2, 2, N'Towhid', N'shikalbaha', N'01861053900', CAST(N'2019-09-26' AS Date), N'Male', N'63709467269967664_1333876583441516_19658692379017216_n.jpg', CAST(N'2019-09-20' AS Date), N'admina@admin.com', N'123456', N'10:35 PM', N'10:35 PM', NULL)
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (2002, 1, 2, 2, 2, N'Rani Das', N'Patiya', N'01215553900', CAST(N'2019-09-06' AS Date), N'Male', N'90834457246-467204_horse-sketch-shower-curtain-clipart.png', CAST(N'2019-09-11' AS Date), N'ranidas@joyamedical.c', N'123456', N'9:10 PM', N'10:10 PM', NULL)
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (2003, 1, 2, 2, 2, N'Febric', N'Califonia', N'01861053900', CAST(N'2019-10-05' AS Date), N'Male', N'8163999655-things-your-doctor-really-wants-to-say-to-you-but-wont.png', CAST(N'2019-10-28' AS Date), N'ebric@jmmch.com', N'123456', N'8:00 PM', N'10:00 PM', NULL)
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (2004, 1, 2, 2, 2, N'Lubina carl', N'German', N'0165454545', CAST(N'1986-10-15' AS Date), N'Female', N'1776432481d9561549922f127b9443d47b94ae69ed.png', CAST(N'2019-10-12' AS Date), N'lubina@jmmch.com', N'123456', N'10:00 AM', N'12:00 PM', NULL)
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (2006, 1, 2, 2, 2, N'Henry Mart', N'nowkali', N'01521212121', CAST(N'1996-04-03' AS Date), N'Male', N'767148984better-care-doctor.png', CAST(N'2019-10-02' AS Date), N'henrymart@jmmch.com', N'123456', N'5:00 PM', N'8:00 PM', NULL)
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (2007, 1, 2, 2, 2, N'Maria Crist', N'nowkali', N'01333333333', CAST(N'1992-10-02' AS Date), N'Female', N'2053422582default_2441.png', CAST(N'2019-10-01' AS Date), N'mariachist@jmmch.com', N'123456', N'12:57 PM', N'12:57 PM', N'MONDAY')
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (2008, 1, 1, 1, 2, N'Rani Das', N'Patiya', N'01861053900', CAST(N'2019-10-30' AS Date), N'Female', N'profile.png', CAST(N'2019-10-19' AS Date), N'ranidas@joya.com', N'123456', NULL, NULL, N'SUNDAY')
INSERT [dbo].[DoctorTB] ([DoctorId], [DepartmentId], [SpealizationId], [DesignationId], [RoleId], [Name], [Address], [Phone], [Dob], [Gender], [ImagePath], [Joined], [Email], [Password], [VisitingTimeStart], [VisitingTimeEnd], [VisitDay]) VALUES (2009, 1, 2, 2, 2, N'Kabila Khan', N'Nowkhali', N'01201201201', CAST(N'1991-10-05' AS Date), N'Male', N'profile.png', CAST(N'2019-10-31' AS Date), N'kabila@jmmch.com', N'123456', N'2:58 PM', N'2:57 PM', N'Sun,Mon,Thu')
SET IDENTITY_INSERT [dbo].[DoctorTB] OFF
SET IDENTITY_INSERT [dbo].[NurseTB] ON 

INSERT [dbo].[NurseTB] ([NurseId], [Name], [Address], [Phone], [Dob], [Qualification], [Joined], [ImagePath], [Email], [Password], [RoleId], [Gender]) VALUES (1, N'Rani Das', N'Patiya', N'01215553900', CAST(N'2019-09-13' AS Date), N'Diploma in Nurse', CAST(N'2019-09-10' AS Date), N'184870420069967664_1333876583441516_19658692379017216_n.jpg', N'ranidas@joyamedical.com', N'MTIzNDU2', 3, N'Female')
INSERT [dbo].[NurseTB] ([NurseId], [Name], [Address], [Phone], [Dob], [Qualification], [Joined], [ImagePath], [Email], [Password], [RoleId], [Gender]) VALUES (2, N'Rani Das', N'Patiya', N'01215553900', CAST(N'1986-10-23' AS Date), N'Diploma in Nurse', CAST(N'2019-10-12' AS Date), N'profile.png', N'ranidas@joyamedical.comm', N'MTIzNDU2', 3, N'Female')
INSERT [dbo].[NurseTB] ([NurseId], [Name], [Address], [Phone], [Dob], [Qualification], [Joined], [ImagePath], [Email], [Password], [RoleId], [Gender]) VALUES (4, N'Abdul', N'Patiya', N'01215553900', CAST(N'2019-10-01' AS Date), N'Diploma in Nurse', CAST(N'2019-10-26' AS Date), N'profile.png', N'abdul@ab.com', N'MTIzNDU2', 3, N'Female')
SET IDENTITY_INSERT [dbo].[NurseTB] OFF
SET IDENTITY_INSERT [dbo].[PatientTB] ON 

INSERT [dbo].[PatientTB] ([Id], [Name], [Age], [Address], [AdmitDate], [WardId], [SeatId], [Problem], [ImagePath], [Gender]) VALUES (5, N'Abdul', 21, N'shikalbaha', CAST(N'2019-10-09' AS Date), 5, 34, N'Sick', N'profile.png', N'Male')
INSERT [dbo].[PatientTB] ([Id], [Name], [Age], [Address], [AdmitDate], [WardId], [SeatId], [Problem], [ImagePath], [Gender]) VALUES (6, N'a', 124, N'shikalbaha', CAST(N'2019-10-09' AS Date), 1, 35, N'Sick', N'profile.png', N'Male')
SET IDENTITY_INSERT [dbo].[PatientTB] OFF
SET IDENTITY_INSERT [dbo].[ReceptionTB] ON 

INSERT [dbo].[ReceptionTB] ([Id], [Name], [Phone], [Address], [Dob], [Gender], [ImagePath], [Email], [Password], [RoleId], [Joined]) VALUES (1, N'Suman', N'01215553900', N'Patiya', CAST(N'2019-10-12' AS Date), N'Male', N'161227464646-467204_horse-sketch-shower-curtain-clipart.png', N'suman@joyamedical.com', N'123456', 4, CAST(N'2019-10-15' AS Date))
SET IDENTITY_INSERT [dbo].[ReceptionTB] OFF
SET IDENTITY_INSERT [dbo].[RoleTB] ON 

INSERT [dbo].[RoleTB] ([Id], [Role]) VALUES (1, N'Admin')
INSERT [dbo].[RoleTB] ([Id], [Role]) VALUES (2, N'Doctor')
INSERT [dbo].[RoleTB] ([Id], [Role]) VALUES (3, N'Nurse')
INSERT [dbo].[RoleTB] ([Id], [Role]) VALUES (4, N'Reception')
INSERT [dbo].[RoleTB] ([Id], [Role]) VALUES (5, N'Staff')
SET IDENTITY_INSERT [dbo].[RoleTB] OFF
SET IDENTITY_INSERT [dbo].[SeatTB] ON 

INSERT [dbo].[SeatTB] ([Id], [WardId], [SeatNo]) VALUES (34, 5, N'M-1')
INSERT [dbo].[SeatTB] ([Id], [WardId], [SeatNo]) VALUES (35, 1, N'M-111')
INSERT [dbo].[SeatTB] ([Id], [WardId], [SeatNo]) VALUES (42, 3, N'M-2')
SET IDENTITY_INSERT [dbo].[SeatTB] OFF
SET IDENTITY_INSERT [dbo].[SpealizationTB] ON 

INSERT [dbo].[SpealizationTB] ([Id], [Type]) VALUES (1, N'Medicine')
INSERT [dbo].[SpealizationTB] ([Id], [Type]) VALUES (2, N'Surgon')
SET IDENTITY_INSERT [dbo].[SpealizationTB] OFF
SET IDENTITY_INSERT [dbo].[StaffTB] ON 

INSERT [dbo].[StaffTB] ([Id], [Name], [Dob], [Joined], [Address], [ImagePath], [RoleId], [Phone], [Gender]) VALUES (2, N'Abdul', CAST(N'2019-10-28' AS Date), CAST(N'2019-10-30' AS Date), N'Patiya', N'167953766746-467204_horse-sketch-shower-curtain-clipart.png', 5, N'0186105390', N'Male')
INSERT [dbo].[StaffTB] ([Id], [Name], [Dob], [Joined], [Address], [ImagePath], [RoleId], [Phone], [Gender]) VALUES (3, N'Towhid', CAST(N'2019-10-19' AS Date), CAST(N'2019-10-31' AS Date), N'Patiya', N'profile.png', 1, N'018610539000', N'Female')
SET IDENTITY_INSERT [dbo].[StaffTB] OFF
SET IDENTITY_INSERT [dbo].[WardTB] ON 

INSERT [dbo].[WardTB] ([Id], [WardNo], [DepartmentId], [SeatQuentity], [AvailableSeat], [TotalSeat]) VALUES (1, N'MED-101', 1, 0, 0, 100)
INSERT [dbo].[WardTB] ([Id], [WardNo], [DepartmentId], [SeatQuentity], [AvailableSeat], [TotalSeat]) VALUES (2, N'MED-102', 1, 0, 0, 100)
INSERT [dbo].[WardTB] ([Id], [WardNo], [DepartmentId], [SeatQuentity], [AvailableSeat], [TotalSeat]) VALUES (3, N'MED-103', 1, 1, 1, 100)
INSERT [dbo].[WardTB] ([Id], [WardNo], [DepartmentId], [SeatQuentity], [AvailableSeat], [TotalSeat]) VALUES (4, N'MED-104', 1, 50, 50, 50)
INSERT [dbo].[WardTB] ([Id], [WardNo], [DepartmentId], [SeatQuentity], [AvailableSeat], [TotalSeat]) VALUES (5, N'MED-105', 1, 1, 0, 1)
SET IDENTITY_INSERT [dbo].[WardTB] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [DepartmentName_DepartmentTB]    Script Date: 10/28/2019 12:20:57 AM ******/
ALTER TABLE [dbo].[DepartmentTB] ADD  CONSTRAINT [DepartmentName_DepartmentTB] UNIQUE NONCLUSTERED 
(
	[DepartmentName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [DesignationName__DesignationTB]    Script Date: 10/28/2019 12:20:57 AM ******/
ALTER TABLE [dbo].[DesignationTB] ADD  CONSTRAINT [DesignationName__DesignationTB] UNIQUE NONCLUSTERED 
(
	[DesignationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailUnique_DoctorTB]    Script Date: 10/28/2019 12:20:57 AM ******/
ALTER TABLE [dbo].[DoctorTB] ADD  CONSTRAINT [EmailUnique_DoctorTB] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailUnique_NurseTB]    Script Date: 10/28/2019 12:20:57 AM ******/
ALTER TABLE [dbo].[NurseTB] ADD  CONSTRAINT [EmailUnique_NurseTB] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailUnique_ReceptionTB]    Script Date: 10/28/2019 12:20:57 AM ******/
ALTER TABLE [dbo].[ReceptionTB] ADD  CONSTRAINT [EmailUnique_ReceptionTB] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdminTB]  WITH CHECK ADD  CONSTRAINT [FK_AdminTB_RoleTB] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleTB] ([Id])
GO
ALTER TABLE [dbo].[AdminTB] CHECK CONSTRAINT [FK_AdminTB_RoleTB]
GO
ALTER TABLE [dbo].[AppointmentTB]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentTB_DoctorTB] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[DoctorTB] ([DoctorId])
GO
ALTER TABLE [dbo].[AppointmentTB] CHECK CONSTRAINT [FK_AppointmentTB_DoctorTB]
GO
ALTER TABLE [dbo].[BillTB]  WITH CHECK ADD  CONSTRAINT [FK_BillTB_BillTB] FOREIGN KEY([Id])
REFERENCES [dbo].[BillTB] ([Id])
GO
ALTER TABLE [dbo].[BillTB] CHECK CONSTRAINT [FK_BillTB_BillTB]
GO
ALTER TABLE [dbo].[BillTB]  WITH CHECK ADD  CONSTRAINT [FK_BillTB_PatientTB] FOREIGN KEY([PatientId])
REFERENCES [dbo].[PatientTB] ([Id])
GO
ALTER TABLE [dbo].[BillTB] CHECK CONSTRAINT [FK_BillTB_PatientTB]
GO
ALTER TABLE [dbo].[DoctorTB]  WITH CHECK ADD  CONSTRAINT [FK_DoctorTB_DepartmentTB] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[DepartmentTB] ([Id])
GO
ALTER TABLE [dbo].[DoctorTB] CHECK CONSTRAINT [FK_DoctorTB_DepartmentTB]
GO
ALTER TABLE [dbo].[DoctorTB]  WITH CHECK ADD  CONSTRAINT [FK_DoctorTB_DesignationTB] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[DesignationTB] ([Id])
GO
ALTER TABLE [dbo].[DoctorTB] CHECK CONSTRAINT [FK_DoctorTB_DesignationTB]
GO
ALTER TABLE [dbo].[DoctorTB]  WITH CHECK ADD  CONSTRAINT [FK_DoctorTB_RoleTB] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleTB] ([Id])
GO
ALTER TABLE [dbo].[DoctorTB] CHECK CONSTRAINT [FK_DoctorTB_RoleTB]
GO
ALTER TABLE [dbo].[DoctorTB]  WITH CHECK ADD  CONSTRAINT [FK_DoctorTB_Spealization] FOREIGN KEY([SpealizationId])
REFERENCES [dbo].[SpealizationTB] ([Id])
GO
ALTER TABLE [dbo].[DoctorTB] CHECK CONSTRAINT [FK_DoctorTB_Spealization]
GO
ALTER TABLE [dbo].[NurseTB]  WITH CHECK ADD  CONSTRAINT [FK_NurseTB_RoleTB] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleTB] ([Id])
GO
ALTER TABLE [dbo].[NurseTB] CHECK CONSTRAINT [FK_NurseTB_RoleTB]
GO
ALTER TABLE [dbo].[PatientReport]  WITH CHECK ADD  CONSTRAINT [FK_PatientReport_PatientTB] FOREIGN KEY([PatientId])
REFERENCES [dbo].[PatientTB] ([Id])
GO
ALTER TABLE [dbo].[PatientReport] CHECK CONSTRAINT [FK_PatientReport_PatientTB]
GO
ALTER TABLE [dbo].[PatientTB]  WITH CHECK ADD  CONSTRAINT [FK_PatientTB_SeatTB] FOREIGN KEY([SeatId])
REFERENCES [dbo].[SeatTB] ([Id])
GO
ALTER TABLE [dbo].[PatientTB] CHECK CONSTRAINT [FK_PatientTB_SeatTB]
GO
ALTER TABLE [dbo].[PatientTB]  WITH CHECK ADD  CONSTRAINT [FK_PatientTB_WardTB] FOREIGN KEY([WardId])
REFERENCES [dbo].[WardTB] ([Id])
GO
ALTER TABLE [dbo].[PatientTB] CHECK CONSTRAINT [FK_PatientTB_WardTB]
GO
ALTER TABLE [dbo].[PrescribeTestTB]  WITH CHECK ADD  CONSTRAINT [FK_PrescribeTestTB_DoctorTB] FOREIGN KEY([RefferDoctorId])
REFERENCES [dbo].[DoctorTB] ([DoctorId])
GO
ALTER TABLE [dbo].[PrescribeTestTB] CHECK CONSTRAINT [FK_PrescribeTestTB_DoctorTB]
GO
ALTER TABLE [dbo].[PrescribeTestTB]  WITH CHECK ADD  CONSTRAINT [FK_PrescribeTestTB_PatientTB] FOREIGN KEY([PatientId])
REFERENCES [dbo].[PatientTB] ([Id])
GO
ALTER TABLE [dbo].[PrescribeTestTB] CHECK CONSTRAINT [FK_PrescribeTestTB_PatientTB]
GO
ALTER TABLE [dbo].[ReceptionTB]  WITH CHECK ADD  CONSTRAINT [FK_ReceptionTB_RoleTB] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleTB] ([Id])
GO
ALTER TABLE [dbo].[ReceptionTB] CHECK CONSTRAINT [FK_ReceptionTB_RoleTB]
GO
ALTER TABLE [dbo].[SeatTB]  WITH CHECK ADD  CONSTRAINT [FK_SeatTB_WardTB] FOREIGN KEY([WardId])
REFERENCES [dbo].[WardTB] ([Id])
GO
ALTER TABLE [dbo].[SeatTB] CHECK CONSTRAINT [FK_SeatTB_WardTB]
GO
ALTER TABLE [dbo].[StaffTB]  WITH CHECK ADD  CONSTRAINT [FK_StaffTB_RoleTB] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleTB] ([Id])
GO
ALTER TABLE [dbo].[StaffTB] CHECK CONSTRAINT [FK_StaffTB_RoleTB]
GO
ALTER TABLE [dbo].[TestRepaortTB]  WITH CHECK ADD  CONSTRAINT [FK_TestRepaortTB_TestTB] FOREIGN KEY([TestId])
REFERENCES [dbo].[TestTB] ([Id])
GO
ALTER TABLE [dbo].[TestRepaortTB] CHECK CONSTRAINT [FK_TestRepaortTB_TestTB]
GO
ALTER TABLE [dbo].[WardTB]  WITH CHECK ADD  CONSTRAINT [FK_WardTB_DepartmentTB] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[DepartmentTB] ([Id])
GO
ALTER TABLE [dbo].[WardTB] CHECK CONSTRAINT [FK_WardTB_DepartmentTB]
GO
USE [master]
GO
ALTER DATABASE [MedicalInformationSystem] SET  READ_WRITE 
GO

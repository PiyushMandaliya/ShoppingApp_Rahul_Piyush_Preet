USE [master]
GO
/****** Object:  Database [shoppingappdb]    Script Date: 3/21/2021 11:05:31 AM ******/
CREATE DATABASE [shoppingappdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'shoppingappdb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\shoppingappdb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'shoppingappdb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\shoppingappdb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [shoppingappdb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [shoppingappdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [shoppingappdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [shoppingappdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [shoppingappdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [shoppingappdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [shoppingappdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [shoppingappdb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [shoppingappdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [shoppingappdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [shoppingappdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [shoppingappdb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [shoppingappdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [shoppingappdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [shoppingappdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [shoppingappdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [shoppingappdb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [shoppingappdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [shoppingappdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [shoppingappdb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [shoppingappdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [shoppingappdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [shoppingappdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [shoppingappdb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [shoppingappdb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [shoppingappdb] SET  MULTI_USER 
GO
ALTER DATABASE [shoppingappdb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [shoppingappdb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [shoppingappdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [shoppingappdb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [shoppingappdb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [shoppingappdb] SET QUERY_STORE = OFF
GO
USE [shoppingappdb]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 3/21/2021 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Id] [bigint] IDENTITY(10001,1) NOT NULL,
	[DateCreated] [datetime2](3) NOT NULL,
	[DateModified] [datetime2](3) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Firstname] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[Salt] [varbinary](max) NOT NULL,
	[Hash] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 3/21/2021 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime2](3) NOT NULL,
	[DateModified] [datetime2](3) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[ItemCount] [int] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/21/2021 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [bigint] IDENTITY(10001,1) NOT NULL,
	[DateCreated] [datetime2](3) NOT NULL,
	[DateModified] [datetime2](3) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 3/21/2021 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [bigint] IDENTITY(10001,1) NOT NULL,
	[DateCreated] [datetime2](3) NOT NULL,
	[DateModified] [datetime2](3) NOT NULL,
	[CategoryId] [bigint] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[InventoryCount] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/21/2021 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(10001,1) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[SecurityQuestion] [nvarchar](300) NOT NULL,
	[Answer] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](10) NOT NULL,
	[Salt] [varbinary](max) NOT NULL,
	[Hash] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [DateCreated], [DateModified], [Name]) VALUES (10002, CAST(N'2021-03-19T02:55:57.9300000' AS DateTime2), CAST(N'2021-03-19T02:55:57.9300000' AS DateTime2), N'Books')
INSERT [dbo].[Category] ([Id], [DateCreated], [DateModified], [Name]) VALUES (10003, CAST(N'2021-03-19T02:56:16.9400000' AS DateTime2), CAST(N'2021-03-19T02:56:16.9400000' AS DateTime2), N'Electronic')
INSERT [dbo].[Category] ([Id], [DateCreated], [DateModified], [Name]) VALUES (10004, CAST(N'2021-03-21T14:44:29.2160000' AS DateTime2), CAST(N'2021-03-21T14:44:29.2160000' AS DateTime2), N'sports')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [DateCreated], [DateModified], [CategoryId], [Title], [Description], [Price], [InventoryCount]) VALUES (10001, CAST(N'2021-03-19T04:26:58.9600000' AS DateTime2), CAST(N'2021-03-21T04:06:40.8650000' AS DateTime2), 10003, N'iWatch', N'iWatch series 6.', CAST(150.00 AS Decimal(10, 2)), 15)
INSERT [dbo].[Products] ([Id], [DateCreated], [DateModified], [CategoryId], [Title], [Description], [Price], [InventoryCount]) VALUES (10002, CAST(N'2021-03-19T04:34:14.5960000' AS DateTime2), CAST(N'2021-03-21T14:45:04.3040000' AS DateTime2), 10003, N'iPhone X', N'want the height of the DataGrid to be independent of how many rows it contains, to remain the same height it is in the design view. The user may change th', CAST(1100.00 AS Decimal(10, 2)), 5)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [DateCreated], [DateModified], [FirstName], [LastName], [UserName], [SecurityQuestion], [Answer], [Type], [Salt], [Hash]) VALUES (10001, CAST(N'2021-03-21T03:42:09.3695522' AS DateTime2), CAST(N'2021-03-21T03:42:09.3695522' AS DateTime2), N'Piyush', N'Mandaliya', N'imomis77', N'In which Year did you graduate from High School', N'2011', N'user', 0x86A94C10C102EE886ABAAF7BF9938F3EAD75752D432C8A59A3DED28BBF13400E, 0x1613C1EFFC14FC11F9C82B4CED6C7D3E8A685206C510D401D5A578682D2882C2)
INSERT [dbo].[Users] ([Id], [DateCreated], [DateModified], [FirstName], [LastName], [UserName], [SecurityQuestion], [Answer], [Type], [Salt], [Hash]) VALUES (10002, CAST(N'2021-03-21T03:45:54.3930533' AS DateTime2), CAST(N'2021-03-21T03:45:54.3930533' AS DateTime2), N'Preet', N'Kansara', N'preet77', N'What was your first car?', N'bmw', N'user', 0x29B7B1608946630A4862D4B4D5C2597B5FB859625F8F548E6611A85439AAB476, 0x6D062827787ED9124574BE6F7F5BD2386E464146B43A2A4331132502985C7738)
INSERT [dbo].[Users] ([Id], [DateCreated], [DateModified], [FirstName], [LastName], [UserName], [SecurityQuestion], [Answer], [Type], [Salt], [Hash]) VALUES (10003, CAST(N'2021-03-21T04:08:13.8325475' AS DateTime2), CAST(N'2021-03-21T04:08:13.8325475' AS DateTime2), N'Jared', N'chevalier', N'jared', N'In which Year did you graduate from High School', N'isi', N'admin', 0x1F0E5766A933BA1F7A01E3DE8BFDE52201C6F8757AB1041C98B3D13CF483AF30, 0xCB47C50A32891FC72537A8F2D71A8B3A46570623E4C184B8CC1DFB33C5E31052)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK__Cart__ProductId__4AB81AF0] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK__Cart__ProductId__4AB81AF0]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK__Products__catego__4316F928] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK__Products__catego__4316F928]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Products] FOREIGN KEY([Id])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Products]
GO
USE [master]
GO
ALTER DATABASE [shoppingappdb] SET  READ_WRITE 
GO

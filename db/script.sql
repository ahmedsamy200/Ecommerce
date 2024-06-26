USE [master]
GO
/****** Object:  Database [Ecommerce]    Script Date: 2/5/2023 5:31:56 PM ******/
CREATE DATABASE [Ecommerce]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Ecommerce', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Ecommerce.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Ecommerce_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Ecommerce_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Ecommerce] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Ecommerce].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Ecommerce] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Ecommerce] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Ecommerce] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Ecommerce] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Ecommerce] SET ARITHABORT OFF 
GO
ALTER DATABASE [Ecommerce] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Ecommerce] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Ecommerce] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Ecommerce] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Ecommerce] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Ecommerce] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Ecommerce] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Ecommerce] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Ecommerce] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Ecommerce] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Ecommerce] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Ecommerce] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Ecommerce] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Ecommerce] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Ecommerce] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Ecommerce] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Ecommerce] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Ecommerce] SET RECOVERY FULL 
GO
ALTER DATABASE [Ecommerce] SET  MULTI_USER 
GO
ALTER DATABASE [Ecommerce] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Ecommerce] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Ecommerce] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Ecommerce] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Ecommerce] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Ecommerce', N'ON'
GO
USE [Ecommerce]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 2/5/2023 5:31:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[cartID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[productID] [int] NULL,
	[quantity] [int] NULL,
	[savedForLater] [bit] NULL,
	[timeAdded] [datetime] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[cartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2/5/2023 5:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[categoryID] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](max) NULL,
	[categoryDescription] [nvarchar](max) NULL,
	[categoryImg] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[categoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2/5/2023 5:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customerID] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](50) NULL,
	[lastName] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[mobile] [nvarchar](50) NULL,
	[image] [nvarchar](50) NULL,
	[country] [nvarchar](50) NULL,
	[city] [nvarchar](50) NULL,
	[state] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[registrationDate] [datetime] NULL,
	[roleID] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[customerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 2/5/2023 5:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[orderDetailsID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[price] [decimal](18, 2) NULL,
	[quntity] [int] NULL,
	[total] [decimal](18, 2) NULL,
	[orderID] [int] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[orderDetailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2/5/2023 5:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[orderID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[status] [bit] NULL,
	[shippingCountry] [nvarchar](50) NULL,
	[shippingCity] [nvarchar](50) NULL,
	[shippingState] [nvarchar](50) NULL,
	[orderPhone] [nvarchar](50) NULL,
	[orderDate] [datetime] NULL,
	[IsDelivered] [bit] NULL,
	[totalPrice] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/5/2023 5:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[productID] [int] IDENTITY(1,1) NOT NULL,
	[prodName] [nvarchar](max) NULL,
	[prodDescription] [nvarchar](max) NULL,
	[price] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[inStock] [int] NULL,
	[img] [nvarchar](max) NULL,
	[isFeature] [bit] NULL,
	[categoryID] [int] NULL,
	[rank] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductReview]    Script Date: 2/5/2023 5:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductReview](
	[reviewID] [int] IDENTITY(1,1) NOT NULL,
	[productID] [int] NULL,
	[userID] [int] NULL,
	[commentDate] [datetime] NULL,
	[comment] [nvarchar](max) NULL,
	[rating] [tinyint] NULL,
 CONSTRAINT [PK_ProductReview] PRIMARY KEY CLUSTERED 
(
	[reviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2/5/2023 5:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[roleID] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[roleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([cartID], [userID], [productID], [quantity], [savedForLater], [timeAdded]) VALUES (11023, 1, 2, 1, 1, CAST(N'2021-09-20 19:14:41.683' AS DateTime))
INSERT [dbo].[Cart] ([cartID], [userID], [productID], [quantity], [savedForLater], [timeAdded]) VALUES (12023, 1, 1, 1, 1, CAST(N'2021-10-10 17:10:58.267' AS DateTime))
INSERT [dbo].[Cart] ([cartID], [userID], [productID], [quantity], [savedForLater], [timeAdded]) VALUES (12027, 6002, 1, 1, 1, CAST(N'2023-01-21 00:18:12.567' AS DateTime))
INSERT [dbo].[Cart] ([cartID], [userID], [productID], [quantity], [savedForLater], [timeAdded]) VALUES (12028, 6002, 2, 1, 0, CAST(N'2023-01-21 00:18:14.080' AS DateTime))
INSERT [dbo].[Cart] ([cartID], [userID], [productID], [quantity], [savedForLater], [timeAdded]) VALUES (13052, 7002, 3, 1, 1, CAST(N'2023-02-05 16:31:15.837' AS DateTime))
SET IDENTITY_INSERT [dbo].[Cart] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([categoryID], [categoryName], [categoryDescription], [categoryImg]) VALUES (1, N'الساعات', N'مجموعه من افضل الساعات', N'clockCat.jpg')
INSERT [dbo].[Categories] ([categoryID], [categoryName], [categoryDescription], [categoryImg]) VALUES (2, N'الحقائب', N'مجموعه من افضل الساعات', N'clockCat.jpg')
INSERT [dbo].[Categories] ([categoryID], [categoryName], [categoryDescription], [categoryImg]) VALUES (3, N'تشيرتات', N'مجموعه من افضل الساعات', N'clockCat.jpg')
INSERT [dbo].[Categories] ([categoryID], [categoryName], [categoryDescription], [categoryImg]) VALUES (4, N'بناطيل', N'مجموعه من افضل الساعات', N'clockCat.jpg')
INSERT [dbo].[Categories] ([categoryID], [categoryName], [categoryDescription], [categoryImg]) VALUES (5, N'احذيه', N'مجموعه من افضل الساعات', N'clockCat.jpg')
INSERT [dbo].[Categories] ([categoryID], [categoryName], [categoryDescription], [categoryImg]) VALUES (6, N'جواكيت', N'مجموعه من افضل الجواكت', NULL)
INSERT [dbo].[Categories] ([categoryID], [categoryName], [categoryDescription], [categoryImg]) VALUES (1006, N'هواتف', N'مجموعه من افضل الساعات', NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (1, N'احمد', N'سامي', N'ahmed@gmail.com', N'123', N'01029405663', N'5454.PNG', N'مصر', N'المنوفيه', N'شبين الكوم', N'مصر - المنوفيه -شبين الكوم', NULL, 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (2, N'ابراهيم', N'سامي', N'ss@gmail.com', N'1234', N'01234567898', N'aa.jpg', NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (3, N'علي', N'وليد', N'ssa@gmail.com', N'111', N'01245678987', N'aas.jpg', NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (4, N'اسلام', N'محمد', N'ssb@gmail.com', N'123456', N'01452389798', N'aab.jpg', NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (5, N'محمد', N'علي', N'ssc@gmail.com', N'111111', N'01123455678', N'aac.jpg', NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (6, N'فتحي', N'سعيد', N'ssca@gmail.com', N'102121', N'01234564564', N'aacs.jpg', NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (1002, N'احمد', N'هندي', N'a', N'123', N'01212121212', NULL, NULL, NULL, NULL, NULL, CAST(N'2021-04-13 22:47:07.157' AS DateTime), 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (2002, N'احمد', N'احمد', N'Test@gmail.com', N'123', N'01021202121', NULL, NULL, NULL, NULL, NULL, CAST(N'2021-04-14 01:41:02.093' AS DateTime), 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (2003, N'محمد', N'علي ', N'aa', N'aa', N'01245785454', N'download99.jpg', N'مصر', N'المنوفيه', N'قويسنا', N'مصر-المنوفيه-قويسنا', CAST(N'2021-04-15 01:14:46.697' AS DateTime), 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (3004, N'cc', N'cc', N'ccc', N'ccc', N'010101010', NULL, NULL, NULL, NULL, NULL, CAST(N'2021-04-23 01:46:13.803' AS DateTime), 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (3005, N'محمد', N'عبد السلام', N'moda@gmail.com', N'123', N'123', NULL, N'مصر', N'الغربيه', N'بني سويف', N'مصر-الغربيه-بني سويف', CAST(N'2021-04-23 13:53:28.347' AS DateTime), 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (4002, N'ahmed', N'samy', N'As', N'12', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-04-26 23:04:47.597' AS DateTime), 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (5002, N'admin', NULL, N'admin', N'admin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (6002, N'ahmed', N'samy', N'user@user.com', N'user', N'01029405663', NULL, NULL, NULL, NULL, NULL, CAST(N'2023-01-20 22:24:04.127' AS DateTime), 2)
INSERT [dbo].[Customer] ([customerID], [firstName], [lastName], [email], [password], [mobile], [image], [country], [city], [state], [address], [registrationDate], [roleID]) VALUES (7002, N'محمد', N'علي', N'mohamed@gmail.com', N'123', N'010111111', NULL, NULL, NULL, NULL, NULL, CAST(N'2023-02-04 08:06:25.417' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([orderDetailsID], [ProductID], [price], [quntity], [total], [orderID]) VALUES (4015, 3, CAST(126.00 AS Decimal(18, 2)), 2, CAST(252.00 AS Decimal(18, 2)), 4010)
INSERT [dbo].[OrderDetails] ([orderDetailsID], [ProductID], [price], [quntity], [total], [orderID]) VALUES (4016, 6, CAST(63.00 AS Decimal(18, 2)), 2, CAST(126.00 AS Decimal(18, 2)), 4010)
INSERT [dbo].[OrderDetails] ([orderDetailsID], [ProductID], [price], [quntity], [total], [orderID]) VALUES (4017, 3, CAST(126.00 AS Decimal(18, 2)), 3, CAST(264.60 AS Decimal(18, 2)), 4011)
INSERT [dbo].[OrderDetails] ([orderDetailsID], [ProductID], [price], [quntity], [total], [orderID]) VALUES (4018, 3, CAST(126.00 AS Decimal(18, 2)), 2, CAST(176.40 AS Decimal(18, 2)), 4012)
INSERT [dbo].[OrderDetails] ([orderDetailsID], [ProductID], [price], [quntity], [total], [orderID]) VALUES (4019, 2, CAST(129.35 AS Decimal(18, 2)), 2, CAST(258.70 AS Decimal(18, 2)), 4013)
INSERT [dbo].[OrderDetails] ([orderDetailsID], [ProductID], [price], [quntity], [total], [orderID]) VALUES (4020, 3, CAST(126.00 AS Decimal(18, 2)), 2, CAST(252.00 AS Decimal(18, 2)), 4013)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([orderID], [userID], [status], [shippingCountry], [shippingCity], [shippingState], [orderPhone], [orderDate], [IsDelivered], [totalPrice]) VALUES (4010, 1, 1, N'مصر', N'مصر', N'مصر', N'11', CAST(N'2023-01-21 18:51:11.913' AS DateTime), 1, CAST(378.00 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([orderID], [userID], [status], [shippingCountry], [shippingCity], [shippingState], [orderPhone], [orderDate], [IsDelivered], [totalPrice]) VALUES (4011, 1, 0, N'12', N'12', N'12', N'12', CAST(N'2023-01-21 18:56:46.033' AS DateTime), 1, CAST(264.60 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([orderID], [userID], [status], [shippingCountry], [shippingCity], [shippingState], [orderPhone], [orderDate], [IsDelivered], [totalPrice]) VALUES (4012, 1, NULL, N'21', N'212', N'1212', N'11', CAST(N'2023-01-21 18:58:12.303' AS DateTime), 0, CAST(176.40 AS Decimal(18, 2)))
INSERT [dbo].[Orders] ([orderID], [userID], [status], [shippingCountry], [shippingCity], [shippingState], [orderPhone], [orderDate], [IsDelivered], [totalPrice]) VALUES (4013, 1, 0, N'egypt', N'aa', N'aa', N'0101010', CAST(N'2023-01-21 19:39:47.297' AS DateTime), 0, CAST(510.70 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([productID], [prodName], [prodDescription], [price], [discount], [inStock], [img], [isFeature], [categoryID], [rank]) VALUES (1, N'ساعه يد حديثه', N'ســـــاعــــة أنيقــة مـــزودة بتوقيــت مـزدوج مــزودة بـشــاشـة سوداء خفية تعرض التاريخ ومواقيت الصلاة. خصائص الساعة: مواقيت الصلاة في جميع مدن العالم بوصلة الكترونية لتحديد اتجاه القبلة تقويم هجري و ميلادي لغة عربية أو انجليزية مقاومة للماء

', CAST(150.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 33, N'download6.jpg', 1, 1, 3)
INSERT [dbo].[Product] ([productID], [prodName], [prodDescription], [price], [discount], [inStock], [img], [isFeature], [categoryID], [rank]) VALUES (2, N'حقيبه جلد', N'ســـــاعــــة أنيقــة مـــزودة بتوقيــت مـزدوج مــزودة بـشــاشـة سوداء خفية تعرض التاريخ ومواقيت الصلاة. خصائص الساعة: مواقيت الصلاة في جميع مدن العالم بوصلة الكترونية لتحديد اتجاه القبلة تقويم هجري و ميلادي لغة عربية أو انجليزية مقاومة للماء

', CAST(130.00 AS Decimal(18, 2)), CAST(0.50 AS Decimal(18, 2)), 12, N'download1.jpg', 1, 2, 15)
INSERT [dbo].[Product] ([productID], [prodName], [prodDescription], [price], [discount], [inStock], [img], [isFeature], [categoryID], [rank]) VALUES (3, N'جاكيت جينز', N'ســـــاعــــة أنيقــة مـــزودة بتوقيــت مـزدوج مــزودة بـشــاشـة سوداء خفية تعرض التاريخ ومواقيت الصلاة. خصائص الساعة: مواقيت الصلاة في جميع مدن العالم بوصلة الكترونية لتحديد اتجاه القبلة تقويم هجري و ميلادي لغة عربية أو انجليزية مقاومة للماء

', CAST(180.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), 7, N'download2.jpg', 1, 6, 11)
INSERT [dbo].[Product] ([productID], [prodName], [prodDescription], [price], [discount], [inStock], [img], [isFeature], [categoryID], [rank]) VALUES (4, N'ساعه سوداء', N'ســـــاعــــة أنيقــة مـــزودة بتوقيــت مـزدوج مــزودة بـشــاشـة سوداء خفية تعرض التاريخ ومواقيت الصلاة. خصائص الساعة: مواقيت الصلاة في جميع مدن العالم بوصلة الكترونية لتحديد اتجاه القبلة تقويم هجري و ميلادي لغة عربية أو انجليزية مقاومة للماء

', CAST(80.00 AS Decimal(18, 2)), CAST(22.00 AS Decimal(18, 2)), 11, N'download3.jpg', 1, 1, 2)
INSERT [dbo].[Product] ([productID], [prodName], [prodDescription], [price], [discount], [inStock], [img], [isFeature], [categoryID], [rank]) VALUES (5, N'حقيبه ظهر', N'ســـــاعــــة أنيقــة مـــزودة بتوقيــت مـزدوج مــزودة بـشــاشـة سوداء خفية تعرض التاريخ ومواقيت الصلاة. خصائص الساعة: مواقيت الصلاة في جميع مدن العالم بوصلة الكترونية لتحديد اتجاه القبلة تقويم هجري و ميلادي لغة عربية أو انجليزية مقاومة للماء

', CAST(120.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 14, N'download4.jpg', 1, 2, 12)
INSERT [dbo].[Product] ([productID], [prodName], [prodDescription], [price], [discount], [inStock], [img], [isFeature], [categoryID], [rank]) VALUES (6, N'تيشيرت كاروهات', N'ســـــاعــــة أنيقــة مـــزودة بتوقيــت مـزدوج مــزودة بـشــاشـة سوداء خفية تعرض التاريخ ومواقيت الصلاة. خصائص الساعة: مواقيت الصلاة في جميع مدن العالم بوصلة الكترونية لتحديد اتجاه القبلة تقويم هجري و ميلادي لغة عربية أو انجليزية مقاومة للماء

', CAST(70.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 12, N'download5.jpg', 1, 3, 1)
INSERT [dbo].[Product] ([productID], [prodName], [prodDescription], [price], [discount], [inStock], [img], [isFeature], [categoryID], [rank]) VALUES (7, N'بنطلون ييكا', N'ســـــاعــــة أنيقــة مـــزودة بتوقيــت مـزدوج مــزودة بـشــاشـة سوداء خفية تعرض التاريخ ومواقيت الصلاة. خصائص الساعة: مواقيت الصلاة في جميع مدن العالم بوصلة الكترونية لتحديد اتجاه القبلة تقويم هجري و ميلادي لغة عربية أو انجليزية مقاومة للماء

', CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 8, N'download7.jpg', 1, 4, 6)
INSERT [dbo].[Product] ([productID], [prodName], [prodDescription], [price], [discount], [inStock], [img], [isFeature], [categoryID], [rank]) VALUES (1007, N'هاتف سامسونج', N'ســـــاعــــة أنيقــة مـــزودة بتوقيــت مـزدوج مــزودة بـشــاشـة سوداء خفية تعرض التاريخ ومواقيت الصلاة. خصائص الساعة: مواقيت الصلاة في جميع مدن العالم بوصلة الكترونية لتحديد اتجاه القبلة تقويم هجري و ميلادي لغة عربية أو انجليزية مقاومة للماء', CAST(180.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 12, N'prod4.png', 0, 1006, 3)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[ProductReview] ON 

INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (1, 1, 1, CAST(N'2021-01-26 00:00:00.000' AS DateTime), N'رائع جدا', 4)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (2, 1, 2, CAST(N'2021-01-26 00:00:00.000' AS DateTime), N'جميل', 3)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (3, 1, 3, NULL, NULL, 4)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (4, 1, 4, NULL, NULL, 5)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (5, 1, 5, NULL, NULL, 4)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (10, 1, 6, NULL, NULL, 1)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (11, 2, 1, NULL, NULL, 5)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (12, 3, 1, NULL, NULL, 5)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (13, 3, 2, NULL, NULL, 4)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (14, 4, 1, NULL, NULL, 3)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (15, 4, 2, NULL, NULL, 5)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (16, 4, 3, NULL, NULL, 3)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (1016, 5, 1, NULL, NULL, 2)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (1017, 6, 2, NULL, NULL, 3)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (3016, 4, 1002, CAST(N'2021-04-15 00:45:21.150' AS DateTime), N'جيد', 1)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (3017, 1, 2003, CAST(N'2021-04-15 01:15:08.263' AS DateTime), N'سئ', 1)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (4016, 3, 2003, CAST(N'2021-04-24 01:39:00.067' AS DateTime), N'جيد', 2)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (4017, 2, 4002, CAST(N'2021-04-26 23:09:55.133' AS DateTime), N'جيد', 3)
INSERT [dbo].[ProductReview] ([reviewID], [productID], [userID], [commentDate], [comment], [rating]) VALUES (5016, 4, 7002, CAST(N'2023-02-05 10:04:38.393' AS DateTime), N'جيد جدا', 4)
SET IDENTITY_INSERT [dbo].[ProductReview] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([roleID], [roleName]) VALUES (1, N'admin')
INSERT [dbo].[Roles] ([roleID], [roleName]) VALUES (2, N'user')
SET IDENTITY_INSERT [dbo].[Roles] OFF
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Customer] FOREIGN KEY([userID])
REFERENCES [dbo].[Customer] ([customerID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Customer]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Product] FOREIGN KEY([productID])
REFERENCES [dbo].[Product] ([productID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Product]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([roleID])
REFERENCES [dbo].[Roles] ([roleID])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([orderID])
REFERENCES [dbo].[Orders] ([orderID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([productID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Categories] FOREIGN KEY([categoryID])
REFERENCES [dbo].[Categories] ([categoryID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Categories]
GO
ALTER TABLE [dbo].[ProductReview]  WITH CHECK ADD  CONSTRAINT [FK_ProductReview_Product] FOREIGN KEY([productID])
REFERENCES [dbo].[Product] ([productID])
GO
ALTER TABLE [dbo].[ProductReview] CHECK CONSTRAINT [FK_ProductReview_Product]
GO
ALTER TABLE [dbo].[ProductReview]  WITH CHECK ADD  CONSTRAINT [FK_ProductReview_Users] FOREIGN KEY([userID])
REFERENCES [dbo].[Customer] ([customerID])
GO
ALTER TABLE [dbo].[ProductReview] CHECK CONSTRAINT [FK_ProductReview_Users]
GO
USE [master]
GO
ALTER DATABASE [Ecommerce] SET  READ_WRITE 
GO

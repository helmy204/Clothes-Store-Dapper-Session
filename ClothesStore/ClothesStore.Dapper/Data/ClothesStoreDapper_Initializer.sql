CREATE DATABASE [ClothesStoreDapper]

USE [ClothesStoreDapper]
GO

/****** Object:  Table [dbo].[Category]    Script Date: 7/27/2019 1:51:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO Category VALUES ('Trousers')
INSERT INTO Category VALUES ('Jeans')
INSERT INTO Category VALUES ('Jacket')
INSERT INTO Category VALUES ('Coat')
INSERT INTO Category VALUES ('Dress')
INSERT INTO Category VALUES ('Shoes')
INSERT INTO Category VALUES ('Shorts')
INSERT INTO Category VALUES ('Shirts')
INSERT INTO Category VALUES ('Blouses')
INSERT INTO Category VALUES ('Skirts')


USE [ClothesStoreDapper]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 7/28/2019 1:17:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Product] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Product]
GO

--1 Trousers
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Twill trousers Skinny fit',1)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Wool suit trousers',1)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Cotton chinos Slim Fit',1)
--2 Jeans
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Skinny Fit Biker Jeans',2)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Skinny Jeans',2)
--3 Jacket
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Double-breasted wool jacket',3)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Wool jacket',3)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Nylon-blend bomber jacket',3)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Pile hooded jacket',3)
--4 Coat
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Checked cotton trenchcoat',4)
--5 Dress
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Wide tiered dress',5)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Long bandeau dress',5)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'V-neck dress',5)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'A-line dress',5)
--6 Shoes
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Fully-fashioned trainers',6)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Fully-fashioned trainers',6)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Loafers',6)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Derby shoes',6)
--7 Shorts
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Suit shorts',7)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Patterned swim shorts',7)
--8 Shirts
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Linen-blend shirt Regular Fit',8)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Cotton shirt Regular Fit',8)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Organza shirt',8)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Grandad shirt Regular Fit',8)
--9 Blouses
INSERT INTO Product ([Name],CategoryId) VALUES ( 'V-neck blouse',9)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'V-neck tie-hem blouse',9)
--10 Skirts
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Skirt with broderie anglaise',10)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Striped tiered skirt',10)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Denim skirt',10)
INSERT INTO Product ([Name],CategoryId) VALUES ( 'Draped jersey skirt',10)
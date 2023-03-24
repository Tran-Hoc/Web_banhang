create database Web_Banhang;

use Web_Banhang;

-- for seo 
create table Category(
	Id int identity(1,1) primary key,
	Title nvarchar(150),
	Description nvarchar(150),
	Position int,
	SeoTitle nvarchar(250),
	SeoDesciption nvarchar(250),
	SeoKeyWords nvarchar(250),
	CreateDate datetime,
	CreateBy nvarchar(150),
	ModifiedDate datetime,
	ModifierBy nvarchar(150)
) 
go

create table Adv(
	Id int identity(1,1) primary key,
	Title nvarchar(150),
	Description nvarchar(150),
	Image nvarchar(500),
	Type int,
	Link nvarchar(500),
	CreateDate datetime,
	CreateBy nvarchar(150),
	ModifiedDate datetime,
	ModifierBy nvarchar(150)
) 
go

create table Contact(
	Id int identity(1,1) primary key,
	Name nvarchar(150),
	Website nvarchar(150),
	Email nvarchar(150),
	Message nvarchar(150),
	IsRead bit,
	CreateDate datetime,
	CreateBy nvarchar(150),
	ModifiedDate datetime,
	ModifierBy nvarchar(150)
) 
go

-- for product
create table ProductCategory(
	Id int identity(1,1) primary key,
	Title nvarchar(150),
	Description nvarchar(150),
	Icon nvarchar(150),
	CreateDate datetime,
	CreateBy nvarchar(150),
	ModifiedDate datetime,
	ModifierBy nvarchar(150)
) 
go

create table Product (
	Id int identity(1,1) primary key,
	Title nvarchar(250),
	ProductCategoryID int,
	Description nvarchar(500),
	Detail nvarchar(max),
	Image nvarchar(500),
	SeoTitle nvarchar(250),
	SeoDesciption nvarchar(250),
	SeoKeyWords nvarchar(250),
	Price decimal(18,2),
	PriceScale decimal(18,2),
	Quantity int,
	CreateDate datetime,
	CreateBy nvarchar(150),
	ModifiedDate datetime,
	ModifierBy nvarchar(150)
) 
go

create table New(
	Id int identity(1,1) primary key,
	Title nvarchar(250),
	CategoryID int,
	Description nvarchar(500),
	Detail nvarchar(max),
	Image nvarchar(500),
	CreateDate datetime,
	CreateBy nvarchar(150),
	ModifiedDate datetime,
	ModifierBy nvarchar(150)
) 
go
 
create table Post (
	Id int identity(1,1) primary key,
	Title nvarchar(250),
	CategoryID int,
	Description nvarchar(500),
	Detail nvarchar(max),
	Image nvarchar(500),
	SeeTitle nvarchar(250),
	SeeDecription nvarchar(500),
	SeeKeywords nvarchar(250),
	CreateDate datetime,
	CreateBy nvarchar(150),
	ModifiedDate datetime,
	ModifierBy nvarchar(150)
) 
go

create table Order_(
	Id int identity(1,1) primary key,
	Code nvarchar(50),
	CustomerName nvarchar(150),
	Phone nvarchar(15),
	Address nvarchar(500),
	TotalAmount decimal(18,0),
	Quantity int,
	CreatedDate datetime,
	CreatedBy nvarchar(150),
	ModifiedDate datetime,
	ModifierBy nvarchar(150),
)
go

create table OrderDetail(
	Id int identity(1,1) primary key,
	OrderId int,
	ProductId int,
	Price decimal(18,0),
	Quantity int,
)
go

create table Subcribe(
	Id int identity(1,1) primary key,
	Email nvarchar(150),
	CreateDate datetime
)
go

create table SystemSetting(
	SettingKey nvarchar(50) primary key,
	SettingValue nvarchar(max),
	SettingDescription nvarchar(250)
)
go
 
 alter table OrderDetail
 add constraint Fk_OrderDetail_Product 
 foreign key  (ProductId)
 references Product(Id)	

  alter table OrderDetail
 add constraint Fk_OrderDetail_Order_
 foreign key  (OrderId)
 references Order_(Id)	

 alter table Product 
 add constraint Fk_Product_ProductCategory
 foreign key (ProductCategoryID)
 references ProductCategory(Id)

 alter table Post 
 add constraint Fk_Post_Category
 foreign key (CategoryID)
 references Category(Id)
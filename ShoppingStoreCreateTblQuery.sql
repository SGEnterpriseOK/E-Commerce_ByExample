
go
use dbMyOnlineShopping
go

CREATE TABLE Tbl_Category(

CategoryId int identity primary key,
CategoryName varchar(500) unique,
IsActive bit null,
IsDelete bit null

)

CREATE TABLE Tbl_Product
(
ProductId int identity primary key,
ProductName varchar (500) unique,
CategoryId int,
IsActive bit null,
IsDelete bit null,
CreatedDate datetime null,
ModifiedDate datetime null,
Description datetime null,
ProductImage varchar,
IsFeatured bit null,
Quantity int,
foreign key (CategoryId) references Tbl_Category(CategoryId)

)

CREATE TABLE Tbl_CartStatus
(
CartStatusId int identity primary key,
CartStatus varchar(500)
)

CREATE TABLE Tbl_Members
(
MemberId int identity primary key,
FirstName varchar(200),
LastName varchar(200) unique,
EmailId varchar(200) unique,
Password varchar(500),
IsActive bit null,
IsDelete bit null,
CreatedOn datetime,
ModifiedOn datetime
)

CREATE TABLE Tbl_ShippingDetail
(
ShippingDetailId int identity primary key,
MemberId int,
Adress varchar (500),
City varchar (500),
Province varchar(500),
Country varchar(50),


PostalCode varchar (50),
OrderId int,
AmountPaid decimal,
PaymentType varchar (50)
foreign key (MemberId) references Tbl_Members(MemberId)
)

CREATE TABLE Tbl_Roles
(
RoleId int identity primary key,
RoleName varchar(200)
)

CREATE TABLE Tbl_Cart
(
CartId int identity primary key,
ProductId int,
MemberId int,
CartStatusId int,
foreign key (ProductId) references Tbl_Product(ProductId)
)

CREATE TABLE Tbl_MemberRole
(
MemberRoleId int identity primary key,
MemberId int,

RoleId int
)

CREATE TABLE Tbl_SlideImage
(
SlideId int identity primary key,
SlideTitle varchar(500),
SlideImage varchar(MAX)
)
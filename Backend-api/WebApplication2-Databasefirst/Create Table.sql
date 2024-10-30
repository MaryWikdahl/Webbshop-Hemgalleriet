-- Ta bort Products först
DROP TABLE Products;

-- Ta bort Categories efter Products
DROP TABLE Categories;

-- Ta bort Images sist
DROP TABLE Images;


--CREATE TABLE Images(
--Id int not null identity primary key,
--Encoded varchar(MAX) not null
--)
CREATE TABLE Images (
   Id int NOT NULL IDENTITY PRIMARY KEY,
   ImageData VARBINARY(MAX) NOT NULL
);
GO
CREATE TABLE Categories(
Id int not null identity primary key,
Name varchar(MAX) not null
)
GO

CREATE TABLE Products(
Id int not null identity primary key,
CreatedDate DateTime not null,
Name  nvarchar(50) not null,
Description varchar(100) not null,
Author nvarchar(50) not null,
Price decimal not null,
Active bit not null,
Sold  bit not null,
CategoriesId int not null references Categories(Id),
ImagesId int not null references Images(Id),    
)
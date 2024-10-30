
--Insert Image
DECLARE @ImageData VARBINARY(MAX)
SELECT @ImageData = BulkColumn
FROM OPENROWSET(BULK 'C:\Users\mary_\Desktop\Tester\Webbshop\Backend-api\WebApplication2-Databasefirst\Images\Coollamp.jpg', SINGLE_BLOB) AS x;

INSERT INTO Images (ImageData)
VALUES (@ImageData);

--insert categories 

INSERT INTO Categories (Name)
VALUES ('Övrigt');

--Insert Product

INSERT INTO Products (CreatedDate, Name, Description, Author, Price, Active, Sold, CategoriesId, ImagesId)
VALUES (GETDATE(), 'Coollamp', 'Lampa 1950tal', 'Okänd', 399.99, 1, 0, 3, 3);
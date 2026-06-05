CREATE DATABASE ECOMMERCE_ASSIGNMENT_DB;
GO

USE ECOMMERCE_ASSIGNMENT_DB;
GO

CREATE TABLE Customer(
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    MobileNo BIGINT NOT NULL,
    City VARCHAR(100) NOT NULL,
    Address VARCHAR(200),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Seller(
    SellerId INT IDENTITY(1,1) PRIMARY KEY,
    SellerName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    MobileNo BIGINT NOT NULL,
    City VARCHAR(100) NOT NULL,
    Rating DECIMAL(2,1),
    IsActive BIT DEFAULT 1
);

CREATE TABLE Product(
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Price DECIMAL(10,2) NOT NULL CHECK(Price > 0),
    StockQuantity INT NOT NULL CHECK(StockQuantity >= 0),
    SellerId INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(SellerId) REFERENCES Seller(SellerId)
);

CREATE TABLE Orders(
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    OrderStatus VARCHAR(50) DEFAULT 'Pending',
    PaymentMode VARCHAR(50) NOT NULL,
    DeliveryCity VARCHAR(100) NOT NULL,
    FOREIGN KEY(CustomerId) REFERENCES Customer(CustomerId)
);

CREATE TABLE OrderItem(
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL CHECK(Quantity > 0),
    UnitPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY(OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY(ProductId) REFERENCES Product(ProductId)
);
INSERT INTO Customer(CustomerName,Email,MobileNo,City,Address)
VALUES
('Arun','arun@gmail.com',9876543210,'Chennai','Anna Nagar'),
('Bala','bala@yahoo.com',9876543211,'Bangalore','MG Road'),
('Charu','charu@gmail.com',9876543212,'Hyderabad','Kukatpally'),
('Divya','divya@gmail.com',9876543213,'Chennai','T Nagar'),
('Anitha','anitha@gmail.com',9876543214,'Mumbai','Andheri');

INSERT INTO Seller(SellerName,Email,MobileNo,City,Rating)
VALUES
('Tech Seller','tech@gmail.com',9000011111,'Chennai',4.5),
('Mobile World','mobile@gmail.com',9000022222,'Bangalore',4.2),
('Laptop Hub','laptop@gmail.com',9000033333,'Hyderabad',4.8),
('Fashion Mart','fashion@gmail.com',9000044444,'Mumbai',4.0);

INSERT INTO Product(ProductName,Category,Price,StockQuantity,SellerId)
VALUES
('iPhone 15','Mobile',75000,5,2),
('Samsung Phone','Mobile',55000,8,2),
('Dell Laptop','Laptop',60000,12,3),
('HP Laptop','Laptop',52000,6,3),
('Bluetooth Speaker','Electronics',3000,20,1),
('Smart Watch','Electronics',8000,15,1),
('Men Shirt','Fashion',1200,30,4),
('Unused Product','Fashion',999,10,4);

INSERT INTO Orders(CustomerId,PaymentMode,DeliveryCity)
VALUES
(1,'UPI','Chennai'),
(2,'Card','Bangalore'),
(3,'COD','Hyderabad'),
(4,'UPI','Chennai'),
(5,'Card','Mumbai');

INSERT INTO OrderItem(OrderId,ProductId,Quantity,UnitPrice)
VALUES
(1,1,1,75000),
(1,5,2,3000),
(2,3,1,60000),
(2,6,1,8000),
(3,2,1,55000),
(3,7,3,1200),
(4,4,1,52000),
(4,5,1,3000),
(5,6,2,8000),
(5,7,2,1200);
UPDATE Customer SET City='Coimbatore' WHERE CustomerId=5;

UPDATE Product SET Price=58000 WHERE ProductId=2;

UPDATE Orders SET OrderStatus='Delivered' WHERE OrderId IN (1,2);

DELETE FROM Product WHERE ProductName='Unused Product';

SELECT * FROM Customer;
SELECT * FROM Seller;
SELECT * FROM Product;
SELECT * FROM Orders;
SELECT * FROM OrderItem;

SELECT * FROM Customer WHERE City='Chennai';
SELECT * FROM Customer WHERE City<>'Chennai';
SELECT * FROM Product WHERE Price>50000;
SELECT * FROM Product WHERE Price BETWEEN 10000 AND 60000;

SELECT * FROM Product WHERE Category IN ('Mobile','Laptop');

SELECT * FROM Customer WHERE CustomerName LIKE 'A%';

SELECT * FROM Customer WHERE Email LIKE '%gmail%';

SELECT * FROM Product WHERE ProductName LIKE '%Phone%';

SELECT * FROM Orders WHERE OrderStatus='Delivered';

SELECT * FROM Product WHERE StockQuantity<10;

SELECT * FROM Customer WHERE MobileNo IS NOT NULL;

SELECT * FROM Product WHERE Price NOT BETWEEN 10000 AND 50000;

SELECT * FROM Customer WHERE City IN ('Chennai','Bangalore');

SELECT * FROM Customer WHERE City='Chennai' AND IsActive=1;

SELECT * FROM Customer WHERE City<>'Hyderabad';
SELECT City, COUNT(*) AS TotalCustomers
FROM Customer
GROUP BY City;


SELECT Category, COUNT(*) AS TotalProducts
FROM Product
GROUP BY Category;


SELECT Category, SUM(StockQuantity) AS TotalStock
FROM Product
GROUP BY Category;

SELECT Category, MAX(Price) AS MaxPrice
FROM Product
GROUP BY Category;

SELECT Category, MIN(Price) AS MinPrice
FROM Product
GROUP BY Category;

SELECT Category, AVG(Price) AS AvgPrice
FROM Product
GROUP BY Category;

SELECT c.CustomerName, SUM(oi.Quantity * oi.UnitPrice) AS TotalOrderAmount
FROM Customer c
JOIN Orders o ON c.CustomerId=o.CustomerId
JOIN OrderItem oi ON o.OrderId=oi.OrderId
GROUP BY c.CustomerName;

SELECT p.ProductName, SUM(oi.Quantity * oi.UnitPrice) AS TotalSales
FROM Product p
JOIN OrderItem oi ON p.ProductId=oi.ProductId
GROUP BY p.ProductName;

SELECT p.ProductName, SUM(oi.Quantity) AS TotalQuantitySold
FROM Product p
JOIN OrderItem oi ON p.ProductId=oi.ProductId
GROUP BY p.ProductName;

SELECT Category, COUNT(*) AS ProductCount
FROM Product
GROUP BY Category
HAVING COUNT(*)>1;

SELECT c.CustomerName, SUM(oi.Quantity * oi.UnitPrice) AS TotalAmount
FROM Customer c
JOIN Orders o ON c.CustomerId=o.CustomerId
JOIN OrderItem oi ON o.OrderId=oi.OrderId
GROUP BY c.CustomerName
HAVING SUM(oi.Quantity * oi.UnitPrice)>50000;

SELECT s.SellerName, COUNT(p.ProductId) AS TotalProducts
FROM Seller s
JOIN Product p ON s.SellerId=p.SellerId
GROUP BY s.SellerName;

SELECT s.SellerName, SUM(oi.Quantity * oi.UnitPrice) AS TotalSales
FROM Seller s
JOIN Product p ON s.SellerId=p.SellerId
JOIN OrderItem oi ON p.ProductId=oi.ProductId
GROUP BY s.SellerName;

SELECT OrderStatus, COUNT(*) AS OrderCount
FROM Orders
GROUP BY OrderStatus;

SELECT City, COUNT(*) AS CustomerCount
FROM Customer
GROUP BY City
ORDER BY CustomerCount DESC;
SELECT * FROM Product ORDER BY Price ASC;

SELECT * FROM Product ORDER BY Price DESC;

SELECT * FROM Customer ORDER BY City ASC, CustomerName ASC;

SELECT * FROM Orders ORDER BY OrderDate DESC;

SELECT * FROM Product ORDER BY Category ASC, Price DESC;

SELECT TOP 3 * FROM Product ORDER BY Price DESC;

SELECT TOP 5 * FROM Orders ORDER BY OrderDate DESC;

SELECT * FROM Customer ORDER BY IsActive DESC, CustomerName ASC;

SELECT o.*, c.CustomerName, c.Email
FROM Orders o
INNER JOIN Customer c ON o.CustomerId=c.CustomerId;

SELECT p.*, s.SellerName
FROM Product p
INNER JOIN Seller s ON p.SellerId=s.SellerId;

SELECT oi.*, p.ProductName
FROM OrderItem oi
INNER JOIN Product p ON oi.ProductId=p.ProductId;

SELECT c.CustomerName, o.OrderId, p.ProductName, s.SellerName,
       oi.Quantity, oi.UnitPrice,
       oi.Quantity * oi.UnitPrice AS TotalAmount
FROM Customer c
JOIN Orders o ON c.CustomerId=o.CustomerId
JOIN OrderItem oi ON o.OrderId=oi.OrderId
JOIN Product p ON oi.ProductId=p.ProductId
JOIN Seller s ON p.SellerId=s.SellerId;

SELECT c.CustomerName, o.OrderId, o.OrderStatus
FROM Customer c
LEFT JOIN Orders o ON c.CustomerId=o.CustomerId;

SELECT o.OrderId, c.CustomerName
FROM Customer c
RIGHT JOIN Orders o ON c.CustomerId=o.CustomerId;

SELECT c.CustomerName, o.OrderId
FROM Customer c
FULL OUTER JOIN Orders o ON c.CustomerId=o.CustomerId;

SELECT c.CustomerName, p.ProductName
FROM Customer c
CROSS JOIN Product p;

SELECT c.*
FROM Customer c
LEFT JOIN Orders o ON c.CustomerId=o.CustomerId
WHERE o.OrderId IS NULL;

SELECT p.*
FROM Product p
LEFT JOIN OrderItem oi ON p.ProductId=oi.ProductId
WHERE oi.OrderItemId IS NULL;

SELECT s.SellerName, p.ProductName, p.Category, p.Price
FROM Seller s
JOIN Product p ON s.SellerId=p.SellerId;

SELECT c.CustomerName, p.ProductName, oi.Quantity
FROM Customer c
JOIN Orders o ON c.CustomerId=o.CustomerId
JOIN OrderItem oi ON o.OrderId=oi.OrderId
JOIN Product p ON oi.ProductId=p.ProductId;

SELECT o.OrderId, SUM(oi.Quantity * oi.UnitPrice) AS OrderTotal
FROM Orders o
JOIN OrderItem oi ON o.OrderId=oi.OrderId
GROUP BY o.OrderId;

SELECT s.SellerName, SUM(oi.Quantity * oi.UnitPrice) AS TotalSales
FROM Seller s
JOIN Product p ON s.SellerId=p.SellerId
JOIN OrderItem oi ON p.ProductId=oi.ProductId
GROUP BY s.SellerName;

SELECT p.ProductName, SUM(oi.Quantity) AS TotalSalesQuantity
FROM Product p
JOIN OrderItem oi ON p.ProductId=oi.ProductId
GROUP BY p.ProductName;
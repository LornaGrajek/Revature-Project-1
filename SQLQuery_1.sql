CREATE DATABASE RRDB;

ALTER DATABASE RRDB
SET AUTO_CLOSE OFF;

-- USE RRDB  INSTEAD OF MASTER
USE RRDB;

--drop table Review;
--drop table Restaurant;

CREATE TABLE Restaurant(
    Id INT PRIMARY KEY IDENTITY(1, 1),
    Name NVARCHAR(450) NOT NULL UNIQUE,
    City NVARCHAR(100),
    State NVARCHAR(50)
);
CREATE TABLE Review(
    Id INT PRIMARY KEY IDENTITY(1, 1),
    Rating INT NOT NULL,
    Note NTEXT, 
    RestaurantId INT FOREIGN KEY REFERENCES Restaurant(Id) NOT NULL
);

INSERT INTO Restaurant (Name, City, State) VALUES 
('Salt and Straw', 'Portland', 'OR'),
('Papagayo', 'Leucadia', 'CA'),
('Del Taco', 'Los Angeles', 'CA');

insert into Review (Rating, RestaurantId, Note) Values (5, 1, 'Amazing Icecream');

delete from Review where Id = 1;
-- delete all reviews using truncate
truncate table Review;

select * from Restaurant;
select * from Review;

SELECT * FROM Restaurant WHERE Id = 6;

--delete anythng that has a keyword in a column keyword 'like' searches for partial matches
delete FROM Restaurant WHERE Name like '%Taco';

--update an already entered record like this::
update Restaurant
set City = 'Seattle', State = 'WA'
where Name = 'Salt and Straw';


-----------------------------------------------------my project down here
create DATABASE UFO;
ALTER DATABASE UFO
SET AUTO_CLOSE OFF;

drop table Customer;
drop table Orders;
drop table StoreFront;
DROP TABLE Product;
drop table Inventory;
drop table LineItem;

CREATE TABLE Customer(
    CustomerId INT PRIMARY KEY,
    UserName NVARCHAR(450) NOT NULL UNIQUE,
    PassWord NVARCHAR(100) not NULL
);
-- ALTER TABLE table_name
-- ADD column_name datatype;
create table StoreFront (
    StoreId int primary key,
    Name NVARCHAR(max),
    Address NVARCHAR(max),
);
INSERT INTO StoreFront(StoreId, Name, Address) VALUES
(1, 'Area 51', 'Earth'),
(2, 'Proxima B', 'Alpha Centauri')

create table Orders (
    OrderId int primary key,
    CustomerId int FOREIGN KEY REFERENCES Customer(CustomerId),
    StoreId int FOREIGN key REFERENCES StoreFront(StoreID),
    Total INT,
    OrderDate Date default GETDATE()
);
--alter table StoreFront
--drop column OrderId;
create table Product(
    ProductID int primary key identity ( 1, 1 ),
    Name NVARCHAR(450) not null,
    Description NVARCHAR(MAX),
    Price int
);
INSERT INTO Product(Name, Description, Price) VALUES
('Element 115', 'A super heavy element and super efficient fuel source for all crafts!', 500),
('Craft Camoflager 2.0', 'Disguise your craft from prying eyes! 3 Modalities: Weather balloon, Swamp Gas, Invisible', 8000),
('The Pleidaian Probe', 'Best probe this side of the black hole!', 250),
('Abduction Ray Beam', 'Adjusts to 3 sizes: Human, Cow, or Car', 2000),
('Warp Speed Boosters', 'You dont want to take the long way home, do you?', 30000),
('Phaser', 'Set it to Stun!', 300)
create table LineItem(
    ItemId int primary key IDENTITY (1, 1),
    Product int FOREIGN KEY REFERENCES Product(ProductID),
    OrderId int FOREIGN KEY REFERENCES Orders(OrderId),
    Quantity int
);
create table Inventory(
    InventoryID int primary key IDENTITY (1, 1),
    StoreId int FOREIGN KEY REFERENCES StoreFront(StoreId),
    ProductId int FOREIGN KEY REFERENCES Product(ProductID),
    Quantity int
);
SELECT p.ProductID, p.Name, p.Description, p.Price AS ProductID, i.ProductId AS InventoryProductId, i.StoreId, i.Quantity 
FROM Product p
INNER JOIN Inventory i
ON p.ProductID = i.ProductId

INSERT INTO Inventory(StoreId, ProductId, Quantity) VALUES
(1, 1, 12), (1, 2, 4), (1, 3, 5), (1, 4, 2), (2, 5, 4), (2, 6, 8), (2, 1, 20)

SELECT * FROM StoreFront;
SELECT * FROM Customer;
SELECT * FROM Product;
SELECT * FROM Inventory;
SELECT * FROM Orders;
SELECT * FROM LineItem;
--SELECT CustomerId FROM Customer WHERE CustomerId = 903;

--truncate table Orders;
truncate table LineItem;
truncate table Inventory;
--delete from StoreFront where StoreId = 9;
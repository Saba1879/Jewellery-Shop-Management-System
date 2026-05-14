	--Create database Jewelery_Shop

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    FullName VARCHAR(100) NOT NULL,
    Role VARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Staff')),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-------------------------------------------------

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName VARCHAR(100) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Email VARCHAR(100),
    Address VARCHAR(255),
    RegistrationDate DATETIME DEFAULT GETDATE()
);

--------------------------------------------------

CREATE TABLE JewelleryItems (
    ItemID INT PRIMARY KEY IDENTITY(1,1),
    ItemName VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Material VARCHAR(50) NOT NULL,
    Purity VARCHAR(20),
    Weight DECIMAL(10,2) NOT NULL,
    PurchasePrice DECIMAL(10,2) NOT NULL,
    SellingPrice DECIMAL(10,2) NOT NULL,
    QuantityInStock INT NOT NULL DEFAULT 0,
    Description VARCHAR(255),
    AddedDate DATETIME DEFAULT GETDATE()
);
--------------------------------------------------


CREATE TABLE Sales (
    SaleID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    UserID INT NOT NULL,
    SaleDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    Discount DECIMAL(10,2) DEFAULT 0,
    FinalAmount DECIMAL(10,2) NOT NULL,
    PaymentMethod VARCHAR(50),
    InvoiceNo VARCHAR(50) UNIQUE,

    CONSTRAINT FK_Sales_Customers
        FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),

    CONSTRAINT FK_Sales_Users
        FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-------------------------------------------

CREATE TABLE SaleDetails (
    SaleDetailID INT PRIMARY KEY IDENTITY(1,1),
    SaleID INT NOT NULL,
    ItemID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    SubTotal DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_SaleDetails_Sales
        FOREIGN KEY (SaleID) REFERENCES Sales(SaleID),

    CONSTRAINT FK_SaleDetails_Items
        FOREIGN KEY (ItemID) REFERENCES JewelleryItems(ItemID)
);
----------------------------------------------

CREATE TABLE Inventory (
    InventoryID INT PRIMARY KEY IDENTITY(1,1),
    ItemID INT NOT NULL,
    StockIn INT DEFAULT 0,
    StockOut INT DEFAULT 0,
    RemainingStock INT NOT NULL,
    LastUpdated DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Inventory_Items
        FOREIGN KEY (ItemID) REFERENCES JewelleryItems(ItemID)
);

CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName VARCHAR(100) NOT NULL,
    Phone VARCHAR(20),
    Address VARCHAR(255)
);

INSERT INTO Users (Username, PasswordHash, FullName, Role)
VALUES
('admin',  'admin123',  'System Admin',      'Admin'),
('staff1', 'staff123',  'Ayesha Khan',       'Staff'),
('staff2', 'staff456',  'Ali Raza',          'Staff'),
('staff3', 'staff789',  'Sara Ahmed',        'Staff'),
('staff4', 'staff111',  'Usman Malik',       'Staff'),
('staff5', 'staff222',  'Hina Noor',         'Staff'),
('staff6', 'staff333',  'Maham Fatima',      'Staff');


-- =========================================
-- 2. CUSTOMERS TABLE (7 records)
-- =========================================
INSERT INTO Customers (CustomerName, Phone, Email, Address)
VALUES
('Sana Ahmed',   '03001234567', 'sana@gmail.com',   'Lahore'),
('Ali Khan',     '03111222333', 'ali@gmail.com',    'Karachi'),
('Hina Noor',    '03222333444', 'hina@gmail.com',   'Islamabad'),
('Usman Tariq',  '03333444555', 'usman@gmail.com',  'Rawalpindi'),
('Maham Fatima', '03444555666', 'maham@gmail.com',  'Faisalabad'),
('Ahmed Raza',   '03555666777', 'ahmed@gmail.com',  'Multan'),
('Laiba Khan',   '03666777888', 'laiba@gmail.com',  'Peshawar');

-- =========================================
-- 3. JEWELLERY ITEMS TABLE (7 records)
-- =========================================
INSERT INTO JewelleryItems
(ItemName, Category, Material, Purity, Weight, PurchasePrice, SellingPrice, QuantityInStock, Description)
VALUES
('Gold Ring',         'Ring',      'Gold',    '22K',     5.50,  45000,  55000, 10, 'Wedding gold ring'),
('Diamond Necklace',  'Necklace',  'Diamond', '24K',    12.00, 150000, 180000,  5, 'Luxury diamond necklace'),
('Silver Bracelet',   'Bracelet',  'Silver',  '925',     8.00,  10000,  15000, 20, 'Stylish silver bracelet'),
('Gold Earrings',     'Earrings',  'Gold',    '18K',     4.00,  25000,  32000, 15, 'Elegant earrings'),
('Pearl Pendant',     'Pendant',   'Pearl',   'Premium', 3.50,  12000,  18000,  8, 'White pearl pendant'),
('Gold Chain',        'Chain',     'Gold',    '22K',    10.50,  70000,  85000,  7, 'Classic gold chain'),
('Silver Anklet',     'Anklet',    'Silver',  '925',     6.20,   9000,  13000, 12, 'Traditional anklet');

-- =========================================
-- 4. INVENTORY TABLE (7 records)
-- =========================================
INSERT INTO Inventory (ItemID, StockIn, StockOut, RemainingStock)
VALUES
(1, 10, 2,  8),
(2,  5, 1,  4),
(3, 20, 5, 15),
(4, 15, 3, 12),
(5,  8, 2,  6),
(6,  7, 1,  6),
(7, 12, 4,  8);

-- =========================================
-- 5. SALES TABLE (7 records)
-- =========================================
INSERT INTO Sales
(CustomerID, UserID, SaleDate, TotalAmount, Discount, FinalAmount, PaymentMethod, InvoiceNo)
VALUES
(1, 1, GETDATE(),  87000,  5000,  82000, 'Cash', 'INV001'),
(2, 2, GETDATE(), 180000, 10000, 170000, 'Card', 'INV002'),
(3, 3, GETDATE(),  15000,     0,  15000, 'Cash', 'INV003'),
(4, 4, GETDATE(),  32000,  2000,  30000, 'Cash', 'INV004'),
(5, 5, GETDATE(),  18000,  1000,  17000, 'Card', 'INV005'),
(6, 6, GETDATE(),  85000,  5000,  80000, 'Cash', 'INV006'),
(7, 7, GETDATE(),  13000,     0,  13000, 'Cash', 'INV007');

-- =========================================
-- 6. SALE DETAILS TABLE (7+ records)
-- =========================================
INSERT INTO SaleDetails
(SaleID, ItemID, Quantity, UnitPrice, SubTotal)
VALUES
(1, 1, 1,  55000,  55000),
(1, 4, 1,  32000,  32000),
(2, 2, 1, 180000, 180000),
(3, 3, 1,  15000,  15000),
(4, 4, 1,  32000,  32000),
(5, 5, 1,  18000,  18000),
(6, 6, 1,  85000,  85000),
(7, 7, 1,  13000,  13000);

-- =========================================
-- 7. SUPPLIERS TABLE (7 records)
-- =========================================
INSERT INTO Suppliers (SupplierName, Phone, Address)
VALUES
('Gold House Traders',  '03001112222', 'Lahore'),
('Diamond World',       '03112223344', 'Karachi'),
('Silver Point',        '03223334455', 'Islamabad'),
('Pearl Palace',        '03334445566', 'Rawalpindi'),
('Luxury Gems',         '03445556677', 'Faisalabad'),
('Royal Jewels Supply', '03556667788', 'Multan'),
('Star Metals',         '03667778899', 'Peshawar');







-------------------------------------------------------------
---STORED PROCEDURE------

--> INSERT JEWELLERY

CREATE PROCEDURE sp_InsertJewellery
@ItemName VARCHAR(100),
@Category VARCHAR(50),
@Material VARCHAR(50),
@Purity VARCHAR(20),
@Weight DECIMAL(10,2),
@PurchasePrice DECIMAL(10,2),
@SellingPrice DECIMAL(10,2),
@Quantity INT,
@SupplierID INT
AS
BEGIN
INSERT INTO JewelleryItems
VALUES (@ItemName,@Category,@Material,@Purity,@Weight,@PurchasePrice,@SellingPrice,@Quantity,@SupplierID)
END



---------------------------------------------
Create PROCEDURE sp_InsertJewellery
@ItemName VARCHAR(100),
@Category VARCHAR(50),
@Material VARCHAR(50),
@Purity VARCHAR(20),
@Weight DECIMAL(10,2),
@PurchasePrice DECIMAL(10,2),
@SellingPrice DECIMAL(10,2),
@Quantity INT
AS
BEGIN
INSERT INTO JewelleryItems
(
    ItemName,
    Category,
    Material,
    Purity,
    Weight,
    PurchasePrice,
    SellingPrice,
    QuantityInStock
)
VALUES
(
    @ItemName,
    @Category,
    @Material,
    @Purity,
    @Weight,
    @PurchasePrice,
    @SellingPrice,
    @Quantity
)
END

--> UPDATE JEWELLERY
drop procedure sp_UpdateJewellery
Create PROCEDURE sp_UpdateJewellery
@ItemID INT,
@SellingPrice DECIMAL(10,2),
@Quantity INT
AS
BEGIN
UPDATE JewelleryItems
SET SellingPrice = @SellingPrice,
    QuantityInStock = @Quantity
WHERE ItemID = @ItemID
END

-->DELETE JEWELLERY
drop procedure sp_DeleteJewellery
Create PROCEDURE sp_DeleteJewellery
@ItemID INT
AS
BEGIN
DELETE FROM JewelleryItems
WHERE ItemID = @ItemID
END

---------------------------------------------------
--FUNCTION--

CREATE FUNCTION fn_Total
(@qty INT, @price DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
RETURN @qty*@price
END

-------------------


--TRIGGER--
CREATE TRIGGER trg_UpdateStock
ON SaleDetails
AFTER INSERT
AS
BEGIN
UPDATE JewelleryItems
SET QuantityInStock = QuantityInStock - inserted.Quantity
FROM JewelleryItems
JOIN inserted ON JewelleryItems.ItemID = inserted.ItemID
END


--------------------
INSERT INTO Customers (CustomerName, Phone)
VALUES ('Walk-in Customer', '0000000000');




SELECT 
    s.SaleID,
    s.InvoiceNo,
    s.SaleDate,
    s.TotalAmount,
    s.FinalAmount,
    
    sd.ItemID,
    sd.Quantity,
    sd.UnitPrice,
    sd.SubTotal,
    
    j.ItemName

FROM Sales s
INNER JOIN SaleDetails sd ON s.SaleID = sd.SaleID
INNER JOIN JewelleryItems j ON sd.ItemID = j.ItemID

WHERE s.SaleID = @SaleID





-------------------------------------------

CREATE PROCEDURE CalculateDiscount
    @SaleID INT,
    @DiscountPercent DECIMAL(5,2)
AS
BEGIN
    DECLARE @Total DECIMAL(10,2)
    DECLARE @DiscountAmount DECIMAL(10,2)
    DECLARE @FinalAmount DECIMAL(10,2)

    SELECT @Total = TotalAmount 
    FROM Sales 
    WHERE SaleID = @SaleID
    SET @DiscountAmount = (@Total * @DiscountPercent) / 100
    SET @FinalAmount = @Total - @DiscountAmount
    UPDATE Sales
    SET 
        Discount = @DiscountAmount,
        FinalAmount = @FinalAmount
    WHERE SaleID = @SaleID
END
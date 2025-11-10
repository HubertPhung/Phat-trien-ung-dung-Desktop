
SET NOCOUNT ON;

IF DB_ID(N'RestaurantManagement') IS NULL
BEGIN
    PRINT N'Creating database [RestaurantManagement]...';
    CREATE DATABASE [RestaurantManagement];
END
GO

USE [RestaurantManagement];
GO

-- ===============================================
-- Drop existing tables (for re-run safety)
-- ===============================================
IF OBJECT_ID(N'dbo.BillDetails', N'U') IS NOT NULL DROP TABLE dbo.BillDetails;
IF OBJECT_ID(N'dbo.Bills', N'U') IS NOT NULL DROP TABLE dbo.Bills;
IF OBJECT_ID(N'dbo.Tables', N'U') IS NOT NULL DROP TABLE dbo.Tables;
IF OBJECT_ID(N'dbo.AccountRoles', N'U') IS NOT NULL DROP TABLE dbo.AccountRoles;
IF OBJECT_ID(N'dbo.Roles', N'U') IS NOT NULL DROP TABLE dbo.Roles;
IF OBJECT_ID(N'dbo.Accounts', N'U') IS NOT NULL DROP TABLE dbo.Accounts;
IF OBJECT_ID(N'dbo.Food', N'U') IS NOT NULL DROP TABLE dbo.Food;
IF OBJECT_ID(N'dbo.Category', N'U') IS NOT NULL DROP TABLE dbo.Category;
GO

-- ===============================================
-- Category
-- ===============================================
CREATE TABLE dbo.Category
(
    ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Category PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    [Type] INT NOT NULL CONSTRAINT CK_Category_Type CHECK ([Type] IN (0,1))
);
GO

-- ===============================================
-- Food
-- ===============================================
CREATE TABLE dbo.Food
(
    ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Food PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Unit NVARCHAR(50) NULL,
    CategoryID INT NOT NULL,
    Price INT NOT NULL CONSTRAINT DF_Food_Price DEFAULT(0),
    Notes NVARCHAR(4000) NULL,
    CONSTRAINT FK_Food_Category FOREIGN KEY (CategoryID) REFERENCES dbo.Category(ID)
);
GO

-- ===============================================
-- Tables
-- ===============================================
CREATE TABLE dbo.Tables
(
    ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Tables PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Tables_IsActive DEFAULT(1),
    Notes NVARCHAR(500) NULL
);
GO

-- ===============================================
-- Accounts
-- ===============================================
CREATE TABLE dbo.Accounts
(
    ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Accounts PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    DisplayName NVARCHAR(200) NOT NULL,
    Password NVARCHAR(256) NOT NULL,
    [Group] NVARCHAR(50) NULL,
    Email NVARCHAR(100) NULL,
    Tel NVARCHAR(50) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Accounts_IsActive DEFAULT(1)
);
GO

-- ===============================================
-- Roles
-- ===============================================
CREATE TABLE dbo.Roles
(
    ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Roles PRIMARY KEY,
    RoleName NVARCHAR(100) NOT NULL UNIQUE,
    [Description] NVARCHAR(200) NULL
);
GO

-- ===============================================
-- AccountRoles
-- ===============================================
CREATE TABLE dbo.AccountRoles
(
    AccountID INT NOT NULL,
    RoleID INT NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_AccountRoles_IsActive DEFAULT(1),
    CONSTRAINT PK_AccountRoles PRIMARY KEY (AccountID, RoleID),
    CONSTRAINT FK_AccountRoles_Accounts FOREIGN KEY (AccountID) REFERENCES dbo.Accounts(ID),
    CONSTRAINT FK_AccountRoles_Roles FOREIGN KEY (RoleID) REFERENCES dbo.Roles(ID)
);
GO

-- ===============================================
-- Bills
-- ===============================================
CREATE TABLE dbo.Bills
(
    ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Bills PRIMARY KEY,
    TableID INT NOT NULL,
    CheckIn DATETIME NOT NULL CONSTRAINT DF_Bills_CheckIn DEFAULT(GETDATE()),
    CheckOut DATETIME NULL,
    DiscountPercent INT NOT NULL CONSTRAINT DF_Bills_Discount DEFAULT(0),
    IsPaid BIT NOT NULL CONSTRAINT DF_Bills_IsPaid DEFAULT(0),
    StaffID INT NULL,
    Notes NVARCHAR(500) NULL,
    CONSTRAINT FK_Bills_Tables FOREIGN KEY (TableID) REFERENCES dbo.Tables(ID),
    CONSTRAINT FK_Bills_Accounts FOREIGN KEY (StaffID) REFERENCES dbo.Accounts(ID)
);
GO

-- ===============================================
-- BillDetails
-- ===============================================
CREATE TABLE dbo.BillDetails
(
    ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_BillDetails PRIMARY KEY,
    BillID INT NOT NULL,
    FoodID INT NOT NULL,
    Quantity INT NOT NULL CONSTRAINT DF_BillDetails_Quantity DEFAULT(1),
    UnitPrice INT NOT NULL,
    Notes NVARCHAR(500) NULL,
    CONSTRAINT FK_BillDetails_Bills FOREIGN KEY (BillID) REFERENCES dbo.Bills(ID) ON DELETE CASCADE,
    CONSTRAINT FK_BillDetails_Food FOREIGN KEY (FoodID) REFERENCES dbo.Food(ID)
);
GO

-- ===============================================
-- Seed sample data
-- ===============================================
INSERT INTO dbo.Category (Name, [Type]) VALUES
(N'Thức uống',0),
(N'Đồ ăn',1),
(N'Khai vị',1),
(N'Tráng miệng',1);
GO

DECLARE @DrinkId INT = (SELECT ID FROM dbo.Category WHERE Name = N'Thức uống');
DECLARE @FoodId INT = (SELECT ID FROM dbo.Category WHERE Name = N'Đồ ăn');
DECLARE @AppetId INT = (SELECT ID FROM dbo.Category WHERE Name = N'Khai vị');
DECLARE @DessId INT = (SELECT ID FROM dbo.Category WHERE Name = N'Tráng miệng');

INSERT INTO dbo.Food (Name, Unit, CategoryID, Price, Notes) VALUES
(N'Cà phê đá', N'Ly', @DrinkId,20000, N''),
(N'Trà đào', N'Ly', @DrinkId,30000, N''),
(N'Cơm gà', N'Phần', @FoodId,45000, N''),
(N'Bún bò', N'Tô', @FoodId,40000, N''),
(N'Gỏi cuốn', N'Phần', @AppetId,25000, N''),
(N'Bánh flan', N'Phần', @DessId,15000, N'');
GO

INSERT INTO dbo.Tables (Name, Notes) VALUES
(N'Bàn1', N'Gần cửa sổ'),
(N'Bàn2', N'Gần quầy'),
(N'Bàn3', N'Sân vườn'),
(N'Bàn4', N'Tầng2'),
(N'Bàn5', N'Phòng VIP');
GO

INSERT INTO dbo.Accounts (UserName, DisplayName, Password, [Group], Email, Tel) VALUES
(N'admin', N'Quản trị', N'123456', N'Admin', N'admin@example.com', N'0900000001'),
(N'staff1', N'Nhân viên1', N'123456', N'Staff', N'staff1@example.com', N'0900000002'),
(N'staff2', N'Nhân viên2', N'123456', N'Staff', N'staff2@example.com', N'0900000003');
GO

INSERT INTO dbo.Roles (RoleName, [Description]) VALUES
(N'ManageTables', N'Quản lý bàn'),
(N'ManageBills', N'Quản lý hóa đơn'),
(N'ManageFoods', N'Quản lý món ăn'),
(N'ManageAccounts', N'Quản lý tài khoản');
GO

INSERT INTO dbo.AccountRoles (AccountID, RoleID)
SELECT a.ID, r.ID FROM dbo.Accounts a CROSS JOIN dbo.Roles r WHERE a.UserName = N'admin';

INSERT INTO dbo.AccountRoles (AccountID, RoleID)
SELECT a.ID, r.ID FROM dbo.Accounts a 
JOIN dbo.Roles r ON r.RoleName IN (N'ManageBills', N'ManageFoods')
WHERE a.UserName IN (N'staff1', N'staff2');
GO

-- ===============================================
-- Stored Procedures
-- ===============================================
IF OBJECT_ID('dbo.InsertFood','P') IS NOT NULL DROP PROCEDURE dbo.InsertFood;
GO
CREATE PROCEDURE dbo.InsertFood
    @id INT OUTPUT,
    @name NVARCHAR(1000),
    @unit NVARCHAR(100),
    @categoryID INT,
    @price INT,
    @notes NVARCHAR(3000)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Food (Name, Unit, CategoryID, Price, Notes)
    VALUES (@name, @unit, @categoryID, @price, @notes);
    SELECT @id = SCOPE_IDENTITY();
END
GO

IF OBJECT_ID('dbo.UpdateFood','P') IS NOT NULL DROP PROCEDURE dbo.UpdateFood;
GO
CREATE PROCEDURE dbo.UpdateFood
    @id INT,
    @name NVARCHAR(1000),
    @unit NVARCHAR(100),
    @categoryID INT,
    @price INT,
    @notes NVARCHAR(3000)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Food
    SET Name = @name,
        Unit = @unit,
        CategoryID = @categoryID,
        Price = @price,
        Notes = @notes
    WHERE ID = @id;
END
GO

PRINT N'RestaurantManagement database successfully created with CategoryID schema and seed data.';
GO

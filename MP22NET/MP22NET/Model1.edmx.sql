
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/23/2015 00:27:30
-- Generated from EDMX file: C:\Users\Zorglub\Documents\GitHub\LicornShop\MP22NET\MP22NET\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MP22NET];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CategoryProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_CategoryProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_SectionProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_SectionProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeSection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sections] DROP CONSTRAINT [FK_EmployeeSection];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckoutEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Checkouts] DROP CONSTRAINT [FK_CheckoutEmployee];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Sections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sections];
GO
IF OBJECT_ID(N'[dbo].[Checkouts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Checkouts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Brand] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Quantity] int  NOT NULL,
    [Category_Id] int  NOT NULL,
    [Section_Id] int  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [Icon] nvarchar(max)  NULL
);
GO

-- Creating table 'Sections'
CREATE TABLE [dbo].[Sections] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Benefit] float  NOT NULL,
    [s_left] int  NOT NULL,
    [s_top] int  NOT NULL,
    [Employee_Id] int  NOT NULL
);
GO

-- Creating table 'Checkouts'
CREATE TABLE [dbo].[Checkouts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Benefit] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Carts] int  NOT NULL,
    [c_left] int  NOT NULL,
    [c_top] int  NOT NULL,
    [Employee_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sections'
ALTER TABLE [dbo].[Sections]
ADD CONSTRAINT [PK_Sections]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Checkouts'
ALTER TABLE [dbo].[Checkouts]
ADD CONSTRAINT [PK_Checkouts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Category_Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_CategoryProduct]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProduct'
CREATE INDEX [IX_FK_CategoryProduct]
ON [dbo].[Products]
    ([Category_Id]);
GO

-- Creating foreign key on [Section_Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_SectionProduct]
    FOREIGN KEY ([Section_Id])
    REFERENCES [dbo].[Sections]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SectionProduct'
CREATE INDEX [IX_FK_SectionProduct]
ON [dbo].[Products]
    ([Section_Id]);
GO

-- Creating foreign key on [Employee_Id] in table 'Sections'
ALTER TABLE [dbo].[Sections]
ADD CONSTRAINT [FK_EmployeeSection]
    FOREIGN KEY ([Employee_Id])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSection'
CREATE INDEX [IX_FK_EmployeeSection]
ON [dbo].[Sections]
    ([Employee_Id]);
GO

-- Creating foreign key on [Employee_Id] in table 'Checkouts'
ALTER TABLE [dbo].[Checkouts]
ADD CONSTRAINT [FK_CheckoutEmployee]
    FOREIGN KEY ([Employee_Id])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckoutEmployee'
CREATE INDEX [IX_FK_CheckoutEmployee]
ON [dbo].[Checkouts]
    ([Employee_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
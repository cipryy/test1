
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/17/2018 12:11:28
-- Generated from EDMX file: C:\Users\Student.STEPIT\Documents\Visual Studio 2015\Projects\TowerDefense\TowerDefense\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EnemyEnemyType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Enemies] DROP CONSTRAINT [FK_EnemyEnemyType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Enemies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Enemies];
GO
IF OBJECT_ID(N'[dbo].[EnemyTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnemyTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Enemies'
CREATE TABLE [dbo].[Enemies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Level] int  NOT NULL,
    [HP] int  NOT NULL,
    [Speed] int  NOT NULL,
    [Type_Id] int  NOT NULL
);
GO

-- Creating table 'EnemyTypes'
CREATE TABLE [dbo].[EnemyTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Towers'
CREATE TABLE [dbo].[Towers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Level] int  NOT NULL,
    [Cost] int  NOT NULL,
    [Attack] int  NOT NULL,
    [Speed] int  NOT NULL,
    [Range] int  NOT NULL,
    [Type_Id] int  NOT NULL
);
GO

-- Creating table 'TowerTypes'
CREATE TABLE [dbo].[TowerTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Worlds'
CREATE TABLE [dbo].[Worlds] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Sequence] int  NOT NULL,
    [X] int  NOT NULL,
    [Y] int  NOT NULL,
    [Type_Id] int  NOT NULL
);
GO

-- Creating table 'CellTypes'
CREATE TABLE [dbo].[CellTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id], [Level] in table 'Enemies'
ALTER TABLE [dbo].[Enemies]
ADD CONSTRAINT [PK_Enemies]
    PRIMARY KEY CLUSTERED ([Id], [Level] ASC);
GO

-- Creating primary key on [Id] in table 'EnemyTypes'
ALTER TABLE [dbo].[EnemyTypes]
ADD CONSTRAINT [PK_EnemyTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Level] in table 'Towers'
ALTER TABLE [dbo].[Towers]
ADD CONSTRAINT [PK_Towers]
    PRIMARY KEY CLUSTERED ([Id], [Level] ASC);
GO

-- Creating primary key on [Id] in table 'TowerTypes'
ALTER TABLE [dbo].[TowerTypes]
ADD CONSTRAINT [PK_TowerTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Sequence] in table 'Worlds'
ALTER TABLE [dbo].[Worlds]
ADD CONSTRAINT [PK_Worlds]
    PRIMARY KEY CLUSTERED ([Id], [Sequence] ASC);
GO

-- Creating primary key on [Id] in table 'CellTypes'
ALTER TABLE [dbo].[CellTypes]
ADD CONSTRAINT [PK_CellTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Type_Id] in table 'Towers'
ALTER TABLE [dbo].[Towers]
ADD CONSTRAINT [FK_TowerTypeAssociation]
    FOREIGN KEY ([Type_Id])
    REFERENCES [dbo].[TowerTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TowerTypeAssociation'
CREATE INDEX [IX_FK_TowerTypeAssociation]
ON [dbo].[Towers]
    ([Type_Id]);
GO

-- Creating foreign key on [Type_Id] in table 'Enemies'
ALTER TABLE [dbo].[Enemies]
ADD CONSTRAINT [FK_EnemyEnemyType1]
    FOREIGN KEY ([Type_Id])
    REFERENCES [dbo].[EnemyTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EnemyEnemyType1'
CREATE INDEX [IX_FK_EnemyEnemyType1]
ON [dbo].[Enemies]
    ([Type_Id]);
GO

-- Creating foreign key on [Type_Id] in table 'Worlds'
ALTER TABLE [dbo].[Worlds]
ADD CONSTRAINT [FK_WorldCellType]
    FOREIGN KEY ([Type_Id])
    REFERENCES [dbo].[CellTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorldCellType'
CREATE INDEX [IX_FK_WorldCellType]
ON [dbo].[Worlds]
    ([Type_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
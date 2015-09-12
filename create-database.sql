USE [master]
CREATE DATABASE [CarsDb]

USE [CarsDb]
CREATE TABLE [dbo].[Cars] (
    [Id]    INT            NOT NULL IDENTITY,
    [Model] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
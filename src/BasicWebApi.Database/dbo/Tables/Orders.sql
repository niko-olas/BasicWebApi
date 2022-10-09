CREATE TABLE [dbo].[Orders] (
    [Id]     UNIQUEIDENTIFIER NOT NULL,
    [ORD_DT] DATETIME         NOT NULL,
    [STATUS] NCHAR (10)       NOT NULL,
    [LastModifiedDate] DATETIME NULL, 
    [TotalPrice] MONEY NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

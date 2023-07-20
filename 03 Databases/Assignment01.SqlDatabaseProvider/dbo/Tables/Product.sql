CREATE TABLE [dbo].[Product] (
    [ProductId]    INT          NOT NULL,
    [CategoryId]   INT          NULL,
    [ProductName]  VARCHAR (40) NOT NULL,
    [Weight]       VARCHAR (20) NOT NULL,
    [UnitPrice]    MONEY        NOT NULL,
    [UnitsInStock] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([CategoryId]) ON DELETE CASCADE
);


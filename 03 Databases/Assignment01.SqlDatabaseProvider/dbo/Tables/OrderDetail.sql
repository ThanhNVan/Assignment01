CREATE TABLE [dbo].[OrderDetail] (
    [OrderId]   INT        NOT NULL,
    [ProductId] INT        NOT NULL,
    [UnitPrice] MONEY      NOT NULL,
    [Quantity]  INT        NOT NULL,
    [Discount]  FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC, [ProductId] ASC),
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId]) ON DELETE CASCADE,
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ProductId]) ON DELETE CASCADE
);


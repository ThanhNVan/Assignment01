CREATE TABLE [dbo].[Order] (
    [OrderId]      INT      NOT NULL,
    [MemberId]     INT      NULL,
    [OrderDate]    DATETIME NOT NULL,
    [RequiredDate] DATETIME NULL,
    [ShippedDate]  DATETIME NULL,
    [Freight]      MONEY    NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC),
    FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Member] ([MemberId]) ON DELETE CASCADE
);


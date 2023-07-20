CREATE TABLE [dbo].[Member] (
    [MemberId]    INT           NOT NULL,
    [Email]       VARCHAR (100) NOT NULL,
    [CompanyName] VARCHAR (40)  NOT NULL,
    [City]        VARCHAR (15)  NOT NULL,
    [Country]     VARCHAR (15)  NOT NULL,
    [Password]    VARCHAR (30)  NOT NULL,
    PRIMARY KEY CLUSTERED ([MemberId] ASC)
);


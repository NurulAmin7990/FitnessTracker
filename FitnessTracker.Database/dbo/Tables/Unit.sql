CREATE TABLE [dbo].[Unit] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [UnitType]    NVARCHAR (10)  NOT NULL,
    [Description] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


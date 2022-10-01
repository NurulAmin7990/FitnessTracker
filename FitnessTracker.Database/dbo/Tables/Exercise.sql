CREATE TABLE [dbo].[Exercise] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [DateCreated]  DATETIME2 (7)  NOT NULL,
    [ExerciseName] NVARCHAR (255) NOT NULL,
    [IsActive]     BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


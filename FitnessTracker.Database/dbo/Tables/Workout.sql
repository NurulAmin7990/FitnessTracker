CREATE TABLE [dbo].[Workout] (
    [ID]          INT            NOT NULL,
    [DateCreated] DATETIME2 (7)  NOT NULL,
    [ExerciseID]  INT            NOT NULL,
    [BodyPartID]  INT            NOT NULL,
    [Sets]        INT            NULL,
    [Reps]        NVARCHAR (255) NULL,
    [Weights]     DECIMAL (18)   NULL,
    [TimeLength]  ROWVERSION     NULL,
    [UnitID]      INT            NULL,
    [Notes]       NVARCHAR (255) NULL,
    CONSTRAINT [PK_Workout] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Workout_BodyPart] FOREIGN KEY ([BodyPartID]) REFERENCES [dbo].[BodyPart] ([ID]),
    CONSTRAINT [FK_Workout_Exercise] FOREIGN KEY ([ExerciseID]) REFERENCES [dbo].[Exercise] ([ID]),
    CONSTRAINT [FK_Workout_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Unit] ([ID])
);


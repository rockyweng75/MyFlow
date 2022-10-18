CREATE TABLE [dbo].[Job]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [StageId] INT NULL, 
    [OrderId] INT NULL, 
    [JobType] INT NULL, 
    [JobClass] NVARCHAR(MAX) NULL, 
    [JobParam] NVARCHAR(MAX) NULL, 
)

create SEQUENCE [dbo].[JobSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[Job]
ADD CONSTRAINT [JobId]
DEFAULT (NEXT VALUE FOR [dbo].[JobSeq]) FOR [Id]

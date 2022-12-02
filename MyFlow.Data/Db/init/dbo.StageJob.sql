CREATE TABLE [dbo].[StageJob]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [StageId] INT NULL, 
    [OrderId] INT NULL, 
    [JobType] INT NULL, 
    [JobClass] NVARCHAR(MAX) NULL, 
    [JobParam] NVARCHAR(MAX) NULL, 
)

create SEQUENCE [dbo].[StageJobSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[StageJob]
ADD CONSTRAINT [StageJobId]
DEFAULT (NEXT VALUE FOR [dbo].[StageJobSeq]) FOR [Id]

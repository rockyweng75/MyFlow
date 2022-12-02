CREATE TABLE [dbo].[ActionJob]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ActionId] INT NULL, 
    [OrderId] INT NULL, 
    [JobType] INT NULL, 
    [JobClass] NVARCHAR(MAX) NULL, 
    [JobParam] NVARCHAR(MAX) NULL, 
)

create SEQUENCE [dbo].[ActionJobSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[ActionJob]
ADD CONSTRAINT [ActionJobId]
DEFAULT (NEXT VALUE FOR [dbo].[ActionJobSeq]) FOR [Id]

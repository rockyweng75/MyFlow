CREATE TABLE [dbo].[Stage]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FlowId] INT NULL, 
    [OrderId] INT NULL, 
    [StageName] NVARCHAR(MAX) NULL, 
    [StageEname] NVARCHAR(MAX) NULL, 
    [Deadline] NVARCHAR(MAX) NULL, 
    [Target] NVARCHAR(MAX) NULL, 
    [TargetParams] NVARCHAR(MAX) NULL
)

create SEQUENCE [dbo].[StageSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[Stage]
ADD CONSTRAINT [StageId]
DEFAULT (NEXT VALUE FOR [dbo].[StageSeq]) FOR [Id]
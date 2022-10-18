CREATE TABLE [dbo].[JobLog]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FlowId] INT NULL, 
    [StageId] INT NULL, 
    [JobId] INT NULL,
    [ApplyId] INT NULL,
    [ApprId] INT NULL,
    [Success] INT NULL,
    [Message] NVARCHAR(MAX) NULL,
    [CreatedDate] DateTime,
    [ReexecuteUser] NVARCHAR(50) NULL,
    [ReexecuteDate] DateTime,
    [FlowName] NVARCHAR(MAX) NULL,
    [StageName] NVARCHAR(MAX) NULL,
    [JobClass] NVARCHAR(MAX) NULL,
    [ApplyUser] NVARCHAR(MAX) NULL,
    [ApproveUser] NVARCHAR(MAX) NULL,
)

create SEQUENCE [dbo].[JobLogSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[JobLog]
ADD CONSTRAINT [JobLogId]
DEFAULT (NEXT VALUE FOR [dbo].[JobLogSeq]) FOR [Id]


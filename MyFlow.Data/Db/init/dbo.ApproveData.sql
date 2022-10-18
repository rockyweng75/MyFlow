CREATE TABLE [dbo].[ApproveData]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FlowId] INT NULL, 
    [StageId] INT NULL, 
    [ApplyId] INT NULL, 
    [FlowName] NVARCHAR(MAX) NULL,
    [StageName] NVARCHAR(MAX) NULL,
    [UserId] NVARCHAR(200) NULL, 
    [UserName] NVARCHAR(500) NULL,
    [DeptCode] NVARCHAR(100) NULL, 
    [DeptName] NVARCHAR(100) NULL, 
    [JobCode] NVARCHAR(50) NULL,
    [JobName] NVARCHAR(100) NULL,
    [StatusCode] NVARCHAR(100) NULL,
    [FormData] NVARCHAR(MAX) NULL,
    [CreatedDate] DATETIME NULL,
    [SubmitDate] DATETIME NULL,
    [CloseDate] DATETIME NULL,
    [Deadline] DATETIME NULL,
)

create SEQUENCE [dbo].[ApproveDataSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[ApproveData]
ADD CONSTRAINT [ApproveDataId]
DEFAULT (NEXT VALUE FOR [dbo].[ApproveDataSeq]) FOR [Id]


   
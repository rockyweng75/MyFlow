CREATE TABLE [dbo].[ApplyData]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FlowId] INT NULL, 
    [StatusCode] INT NULL, 
    [ApplyUser] NVARCHAR(100), 
    [ApplyName] NVARCHAR(100) NULL, 
    [DeptCode] NVARCHAR(50) NULL, 
    [DeptName] NVARCHAR(100) NULL, 
    [JobCode] NVARCHAR(50) NULL,
    [JobName] NVARCHAR(100) NULL,
    [Tag] NVARCHAR(MAX) NULL,
    [Title] NVARCHAR(MAX) NULL,
    [FormData] NVARCHAR(MAX) NULL,
    [CreatedDate] DATETIME NULL,
    [UpdatedDate] DATETIME NULL,
)

create SEQUENCE [dbo].[ApplyDataSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[ApplyData]
ADD CONSTRAINT [ApplyDataId]
DEFAULT (NEXT VALUE FOR [dbo].[ApplyDataSeq]) FOR [Id]

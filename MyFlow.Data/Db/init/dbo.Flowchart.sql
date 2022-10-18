CREATE TABLE [dbo].[Flowchart]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FlowName] NVARCHAR(MAX) NULL, 
    [FlowEname] NVARCHAR(MAX) NULL, 
    [FlowType] INT NULL, 
    [AdminUser] NVARCHAR(MAX) NULL, 
    [Target] NVARCHAR(MAX) NULL, 
    [Close] INT NULL, 
    [TagFormat] NVARCHAR(MAX) NULL, 
    [TitleFormat] NVARCHAR(MAX) NULL
)

create SEQUENCE [dbo].[FlowchartSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[Flowchart]
ADD CONSTRAINT [FlowchartId]
DEFAULT (NEXT VALUE FOR [dbo].[FlowchartSeq]) FOR [Id]


CREATE TABLE [dbo].[Switch]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FlowId] INT NULL, 
    [OrderId] INT NULL, 
    [StageId] INT NULL, 
    [NextStageId] INT NULL, 
    [ActionType] INT NULL, 
    [ActionClass] NVARCHAR(MAX) NULL, 
    [DecisionClass] NVARCHAR(MAX) NULL, 
)

create SEQUENCE [dbo].[SwitchSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[Switch]
ADD CONSTRAINT [SwitchId]
DEFAULT (NEXT VALUE FOR [dbo].[SwitchSeq]) FOR [Id]

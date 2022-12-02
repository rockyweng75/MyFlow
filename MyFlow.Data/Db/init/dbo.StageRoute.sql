CREATE TABLE [dbo].[StageRoute]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FlowId] INT NULL, 
    [StageId] INT NULL, 
    [NextStageId] INT NULL, 
    [ActionType] INT NULL, 
    [RouteName] NVARCHAR(MAX) NULL, 
    [SwitchClass] NVARCHAR(MAX) NULL, 
)

create SEQUENCE [dbo].[StageRouteSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[StageRoute]
ADD CONSTRAINT [StageRouteId]
DEFAULT (NEXT VALUE FOR [dbo].[StageRouteSeq]) FOR [Id]

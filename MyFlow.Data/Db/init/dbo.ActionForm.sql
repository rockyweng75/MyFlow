CREATE TABLE [dbo].[ActionForm]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [StageId] INT NULL, 
    [OrderId] INT NULL, 
    [FormId] INT NULL, 
    [ActionType] INT NULL, 
    [ActionName] NVARCHAR(MAX) NULL, 
    [ButtonName] NVARCHAR(MAX) NULL, 
    [ActionClass] NVARCHAR(MAX) NULL, 
)

create SEQUENCE [dbo].[ActionFormSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[ActionForm]
ADD CONSTRAINT [ActionFormId]
DEFAULT (NEXT VALUE FOR [dbo].[ActionFormSeq]) FOR [Id]
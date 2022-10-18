CREATE TABLE [dbo].[StageValidation]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FlowId] INT NULL, 
    [StageId] INT NULL, 
    [OrderId] INT NULL, 
    [ValidateName] NVARCHAR(MAX) NULL, 
    [ValidateClass] NVARCHAR(MAX) NULL, 
)

create SEQUENCE [dbo].[StageValidationSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[StageValidation]
ADD CONSTRAINT [StageValidationId]
DEFAULT (NEXT VALUE FOR [dbo].[StageValidationSeq]) FOR [Id]
CREATE TABLE [dbo].[Form]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FormType] NVARCHAR(MAX) NULL,
    [FormName] INT NULL, 
    [Close] INT NULL
)

create SEQUENCE [dbo].[FormSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[Form]
ADD CONSTRAINT [FormId]
DEFAULT (NEXT VALUE FOR [dbo].[FormSeq]) FOR [Id]

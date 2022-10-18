CREATE TABLE [dbo].[FormItem]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FormId] INT NULL, 
    [OrderId] INT NULL, 
    [ItemType] INT NULL, 
    [ItemTitle] NVARCHAR(MAX) NULL, 
    [ItemValue] NVARCHAR(MAX) NULL, 
    [ItemDesc] NVARCHAR(MAX) NULL, 
    [ItemFormat] NVARCHAR(MAX) NULL, 
    [DataRef] NVARCHAR(MAX) NULL, 
    [Multiple] NVARCHAR(MAX) NULL, 
    [Disabled] NVARCHAR(MAX) NULL, 
    [Required] NVARCHAR(MAX) NULL, 
)

create SEQUENCE [dbo].[FormItemSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[FormItem]
ADD CONSTRAINT [FormItemId]
DEFAULT (NEXT VALUE FOR [dbo].[FormItemSeq]) FOR [Id]
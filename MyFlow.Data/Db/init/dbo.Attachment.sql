CREATE TABLE [dbo].[Attachment]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ApplyId] INT NULL,
    [ApprId] INT NULL,
    [FileName] NVARCHAR(MAX) NULL, 
    [FilePath] NVARCHAR(MAX) NULL, 
    [ContentType] NVARCHAR(MAX) NULL, 
    [FileData] varbinary(max) 
)

create SEQUENCE [dbo].[AttachmentSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[Attachment]
ADD CONSTRAINT [AttachmentId]
DEFAULT (NEXT VALUE FOR [dbo].[AttachmentSeq]) FOR [Id]


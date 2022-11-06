CREATE TABLE [dbo].[Deadline]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [BeginDate] DATETIME NULL, 
    [EndDate] DATETIME NULL, 
    [DeadlineClass] NVARCHAR(MAX) NULL,
    [DeadlineRemark] NVARCHAR(MAX) NULL
)

create SEQUENCE [dbo].[DeadlineSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[Deadline]
ADD CONSTRAINT [DeadlineId]
DEFAULT (NEXT VALUE FOR [dbo].[DeadlineSeq]) FOR [Id]


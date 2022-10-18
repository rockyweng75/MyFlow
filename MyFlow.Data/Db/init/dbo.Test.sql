CREATE TABLE [dbo].[Test]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Column1] NVARCHAR(MAX) NULL,  
    [Column2] INT NULL,
    [Column3] DateTime NULL,
)

create SEQUENCE [dbo].[TestSeq] as int
start with 1
INCREMENT BY 1;

ALTER TABLE [dbo].[Test]
ADD CONSTRAINT [TestId]
DEFAULT (NEXT VALUE FOR [dbo].[TestSeq]) FOR [Id]

CREATE TABLE [dbo].[Calendar] (
	[IndexDay] INT CHECK (IndexDay < 29) PRIMARY KEY NOT NULL,
	CHECK (IndexDay > 0)
);

CREATE TABLE [dbo].[ToDoItem] (
    [ItemId] UNIQUEIDENTIFIER default NEWID() PRIMARY KEY NOT NULL,
    [Title] NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (500) NOT NULL,
    [Responsible] NVARCHAR (500) NOT NULL,
    [IsCompleted] BIT NOT NULL,
    [State] BIT NOT NULL,
    [IndexDay] INT,
    CONSTRAINT [FK_dbo.ToDoItem_dbo.Calendar_IndexDay] FOREIGN KEY ([IndexDay]) REFERENCES [dbo].[Calendar] ([IndexDay]) ON DELETE CASCADE
);
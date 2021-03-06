Create TABLE Authors (
    AuthorId UNIQUEIDENTIFIER NOT NULL,
    UserName VARCHAR(255),
    [Name] VARCHAR(255),
    PRIMARY KEY(AuthorId)
);
GO
Create TABLE News (
    NewsId UNIQUEIDENTIFIER NOT NULL,
    Title VARCHAR(255) NOT NULL,
    ThumbnailURL VARCHAR(100) NULL,
    Content NVARCHAR(MAX) NOT NULL,
    AuthorId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY(NewsId),
    FOREIGN KEY(AuthorId) REFERENCES Authors(AuthorId)
);
GO
Create TABLE Comments (
    CommentId UNIQUEIDENTIFIER NOT NULL,
    [Text] VARCHAR(2000) NOT NULL,
    AuthorId UNIQUEIDENTIFIER NOT NULL,
    NewsId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY(CommentId),
    FOREIGN KEY(AuthorId) REFERENCES Authors(AuthorId),
    FOREIGN KEY(NewsId) REFERENCES News(NewsId)
);
GO

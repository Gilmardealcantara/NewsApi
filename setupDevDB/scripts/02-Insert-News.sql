declare @authorId UNIQUEIDENTIFIER = '87549312-b134-423b-9b18-4c97a06deb3a';
declare @newsId UNIQUEIDENTIFIER = '3b2c1964-cdd4-423e-9919-c22bd8182dd9';

insert into Authors (AuthorId, UserName)
values (@authorId, 'gilmar.alcantara@gmail.com');

insert into News (NewsId, Title, ThumbnailURL, Content, AuthorId)
values (@newsId, 'Integration Test Title', null, 'Integration Test Content', @authorId);
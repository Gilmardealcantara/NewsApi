-- for commands tests
declare @authorId2 UNIQUEIDENTIFIER = '8b3e711e-affa-4dc6-98ac-f22083d7957f';
declare @newsId2 UNIQUEIDENTIFIER = '71c0ac73-de91-484b-8550-b6bfa6f71d34';


insert into Authors (AuthorId, UserName, [Name])
values (@authorId2, 'jose@gmail.com', 'Jose Maria');

insert into News (NewsId, Title, ThumbnailURL, Content, AuthorId)
values (@newsId2, 'Integration Test 2 Title', null, 'Integration Test 2 Content', @authorId2);
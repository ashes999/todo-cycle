create table tasks (
	Id int primary key not null identity,
	UserId nvarchar(128) not null foreign key references AspNetUsers(Id),
	name varchar(255) not null, -- Markdown
	note varchar(max), -- Markdown
	[order] int not null default 0
)
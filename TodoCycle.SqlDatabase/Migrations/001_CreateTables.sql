create table tasks (
	Id uniqueidentifier primary key not null default newid(),
	UserId varchar(128) not null foreign key references AspNetUsers(Id),
	name varchar(255) not null, -- Markdown
	note varchar(max) -- Markdown
)
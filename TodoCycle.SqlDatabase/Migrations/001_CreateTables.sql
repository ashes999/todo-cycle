create table tasks (
	Id int primary key not null identity,
	UserId nvarchar(128) not null foreign key references AspNetUsers(Id),
	Name varchar(255) not null, -- Markdown
	Note varchar(max), -- Markdown
	[Order] int not null default 0,
	CreatedOnUtc datetime not null default getutcdate(),
	DoneOnUtc datetime default getutcdate(),
	StartDateUtc datetime -- null if not set

)

create table schedules (
	TaskId int foreign key references tasks(Id),
	ScheduleJson varchar(max) -- JSON
)
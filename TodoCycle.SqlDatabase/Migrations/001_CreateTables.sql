create table Tasks (
	Id int primary key not null identity,
	UserId nvarchar(128) not null foreign key references AspNetUsers(Id),
	Name varchar(255) not null, -- Markdown
	Note varchar(max), -- Markdown
	[Order] int not null default 0,
	CreatedOnUtc datetime not null default getutcdate(),
	IsDone bit not null default(0),
	DoneOnUtc datetime -- Null if not done
)

create table ScheduledTasks (
	-- Inherited (duplicated) from Tasks
	Id int primary key not null identity,
	UserId nvarchar(128) not null foreign key references AspNetUsers(Id),
	Name varchar(255) not null, -- Markdown
	Note varchar(max), -- Markdown
	[Order] int not null default 0,
	CreatedOnUtc datetime not null default getutcdate(),
	IsDone bit not null default(0),
	DoneOnUtc datetime, -- Null if not done

	-- Unique to scheduled tasks
	StartDateUtc datetime, -- null if not set
	ScheduleJson varchar(max) -- JSON blurb explaining the task schedule
)
# Todo Cycle

Todo Cycle is a hosted web application for self-management which occupies the middle-ground between a simple TODO list and a Scrum or Kanban-based agile system.

Todo Cycle supports the following features:

- A prioritizable list of stuff to do, at a massive scale (think hundreds of items to work on)
- Focus via a segregation of active tasks vs. a backlog of things to do
- Ability to manage smaller sub-projects, each with a backlog and sprints
- Recurring tasks that persist until they're complete
- One click to enter tasks, complete them, and prioritize them
- Data based statistics on how much you expect to be able to do per cycle

# Dev Environment Setup

- Run `npm install` from the `TodoCycle.WebMvc` directory
- Change the connection string in `TodoCycle.WebMvc\web.config` to use a local database
- Run the website. Register as a new user (this will call down to the database).

# Design Notes

Designed as a Web-API based ASP.NET MVC application. Each major area (eg. todo lists, backlog <=> sprint planning) are single-page applications in Angular.
# Task Tracker

## Description
A command-line task tracker built with C# and .NET.<br>
The project URL https://roadmap.sh/projects/task-tracker

## The user should be able to:

- Add, Update, and Delete tasks
- Mark a task as in progress or done
- List all tasks
- List all tasks that are done
- List all tasks that are not done
- List all tasks that are in progress

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Getting Started

### Build and Run the Application

1. Restore the Dependencies:
```bash
dotnet restore
```

2. Build the application:
```bash
dotnet build
```

3. Run the application:
```bash
dotnet run
```

## Commands
### Add a Task
```bash
dotnet run add "Modify the description here"
```
### Update a Task
```bash
dotnet run update <task-id> "Modify the description here"
```
### Delete a task
```bash
dotnet run delete <task-id>
```
### Change Task Status
```bash
dotnet run mark-in-progress <task-id>
```
```bash
dotnet run mark-done <task-id>
```
### List tasks with status
```bash
dotnet run list <status>
```
- (`<status>` is optional and can be *todo*, *in-progress* or *done*)

## Viewing Task Data
Tasks are stored in `tasks.json`.

## Common Issues
If you encounter issues with the `tasks.json` file, delete it, and the application will create a new one.

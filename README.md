This project to demonstrate the issue with Npgsql generating a `RenameColumn` migration, when migrating from xmin to version column (.NET 6 to .NET 8),
please see: https://github.com/npgsql/efcore.pg/issues/3145

Steps to reproduce:
1. Delete the Migrations folder, if exists.
2. Set the TargetFramework to net6.0, then build solution.
3. Add a new migration
4. Change the TargetFramework to net8.0, build solution, and add a new migration.
5. The new migration should contain the `RenameColumn` command.

Note: I set two preprocessor directives: Net6 and Net8, to toggle between the code changes when changing from .NET version to another,
files that contain changes:
- Setting.cs
- ServerDataContext.cs

Note that the project is only functional via running migrations through EF command-line-tools as noted above to reproduce the issue.
This project to demonstrate the issue with Npgsql generating a `RenameColumn` migration, when migrating from xmin to version column (.NET 6 to .NET 8),
please see: https://github.com/npgsql/efcore.pg/issues/3145

Steps to reproduce:
1. Delete the migrations folder in the project: efcore.pg-issue3145.Migrations, if exists.
2. Set the TargetFramework to net6.0 in the Directory.Build.props file, then build solution.
3. Add a new migration (starter project: efcore.pg-issue3145.MigrationsRunner, migration project: efcore.pg-issue3145.Migrations)
4. Change the TargetFramework to net8.0 in the Directory.Build.props file, build solution, and add a new migration.
5. The new migration should contain the `RenameColumn` command.

Note: I set two preprocessor directives: Net6 and Net8, to toggle between the code changes when changing from .NET version to another,
files that contain changes:
- Setting.cs
- ServerDataContext.cs

Note that the project might not run correctly, it's only functional via running migrations as noted above to reproduce the issue.
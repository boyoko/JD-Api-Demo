# 参考文档
https://docs.microsoft.com/en-us/ef/core/get-started/full-dotnet/new-db

## DbSet to database

Install-Package Microsoft.EntityFrameworkCore.SqlServer

Install-Package Microsoft.EntityFrameworkCore.Tools

Add-Migration MyFirstMigration

Update-Database

## Add DbSet 

Add-Migration Add实体名

Update-Database

## add or modify field

Add-Migration updatedb

Update-Database updatedb


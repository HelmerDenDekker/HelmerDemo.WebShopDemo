# Setup project
This is an idea, I want the steup of the databse to be separate from the API and all.

(c) By Patrick Keller and Helmer den Dekker

I took an example from the eShopOnContainers by microsoft, and cut a lot of cases, we might add the later

## Setup

The idea is to run this project and it sets up the database and migrations for you. We did not achieve this yet, but we have made it a long way

## Infrastructure EF migrations

In Visual Studio, go to the Package Manager console and select the WSD.Catalog.Infrastructure project

### Create migration
Add-Migration <TheMigrationName> -StartupProject WSD.Catalog.Infrastructure.Setup

## Update database without specific migration
Update-Database -StartupProject WSD.Catalog.Infrastructure.Setup

## Update database with specific migration
Update-Database <TheMigrationName> -StartupProject WSD.Catalog.Infrastructure.Setup

## Create SQL scripts from migration a to migration b
Script-Migration -From <Migration a> -To <Migration b> -Context CatalogDbContext -StartupProject WSD.Catalog.Infrastructure.Setup -Output "WSD.Catalog.Infrastructure\Scripts\<ScriptName>.sql"



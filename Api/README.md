# Database sutup

To setup the project database you need to follow these steps:
  1. Instal MS SQL Server and SSMS. [Download MS SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
  2. You can find the database connection string at appsettings.Development.json file.
  3. Connection string:
     
   >server=.;database=TravelSpot;Trusted_Connection=true;TrustServerCertificate=True
   
  4. After database is configured you should add migrations to the project with this commands:

    dotnet ef migrations add Initial

After this steps database would be created and you can use it.

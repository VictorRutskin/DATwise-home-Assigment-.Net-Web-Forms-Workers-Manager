# DATwise home Assigment - .Net Web Forms Workers Manager
A .NET Web Forms application designed for managing workers. This project serves as a comprehensive platform for performing CRUD operations on employee records, with features including advanced search, data filtering, and accessability support.

## Configuration

### Web Config - Edit the needed connection string 
```
<connectionStrings>
  <add name="DATwiseDbConnection" 
       connectionString="server=your_server;
                         database=DATwise;
                         User ID=your_username;
                         Password=your_password;
                         TrustServerCertificate=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### MSSQL - Execute the next query scripts 

#### Database Creation
```
CREATE DATABASE DATwise;
GO
```
#### Add Employees Table
```
USE DATwise;
GO

CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY, 
    FirstName NVARCHAR(50) NOT NULL, 
    LastName NVARCHAR(50) NOT NULL, 
    Email NVARCHAR(100) NOT NULL, 
    Phone NVARCHAR(15), 
    HireDate DATETIME NOT NULL 
);
GO
```

#### Add Logs Table
```
USE DATwise;
GO

CREATE TABLE Logs
(
    Id INT PRIMARY KEY IDENTITY(1,1),  
    Time DATETIME NOT NULL,            
    ExceptionType NVARCHAR(255) NOT NULL, 
    Message NVARCHAR(MAX) NOT NULL,    
    StackTrace NVARCHAR(MAX) NULL,     
    InnerExceptionMessage NVARCHAR(MAX) NULL 
);
GO
```

## Running The Project
- Make sure you've configured everything.

## Technologies I've Used for the Project
- .Net Web Forms
- Microsoft SQL Server.


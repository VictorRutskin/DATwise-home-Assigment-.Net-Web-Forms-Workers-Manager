# DATwise home Assigment - .Net Web Forms Workers Manager
 
## Configuration

### MSSQL
- Execute the next query scripts 

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

## Running The Project
- Make sure you've configured everything.

## Technologies I've Used for the Project
- .Net Web Forms
- Microsoft SQL Server.


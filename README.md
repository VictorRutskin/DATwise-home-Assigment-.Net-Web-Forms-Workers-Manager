# DATwise home Assigment - .Net Web Forms Workers Manager
A .NET Web Forms application designed for managing workers. This project serves as a comprehensive platform for performing CRUD operations on employee records, with features including advanced search, data filtering, and accessability support.


## Requirements:

### 1. Database Design:
- Create a SQL Server database with a table named `Employees` containing the following columns:
  - `EmployeeID` (int) – Primary Key, Auto-increment
  - `FirstName` (nvarchar(50))
  - `LastName` (nvarchar(50))
  - `Email` (nvarchar(100))
  - `Phone` (nvarchar(15))
  - `HireDate` (datetime)

### 2. Web Forms:
- **Employee List:** Create a page (`EmployeeList.aspx`) displaying a list of employees in a `GridView`. The `GridView` should include options to edit and delete records.
- **Add/Edit Employee:** Create a page (`EmployeeForm.aspx`) with a form to add a new employee or edit an existing one. The form should include validation for required fields and correct data formats (e.g., email).

### 3. Functionality:
- Implement data access using `SqlDataSource`.
- The application should include basic input validation (e.g., required fields, valid email format).
- Use appropriate ASP.NET controls like `GridView`, `DetailsView`, `TextBox`, `Button`, etc.
- Implement error handling to manage potential exceptions during database operations.

### 4. User Experience:
- The application should have a simple and user-friendly interface.
- Navigation between the employee list and the add/edit form should be intuitive.

### 5. Bonus (Optional):
- Implement a search function to filter the employee list by name or email.
- Add sorting and pagination options to the employee list.

## Evaluation Criteria:

1. **Code Quality:** Clean and well-organized code with appropriate variable names and comments.
2. **Functionality:** All specified features work correctly without issues.
3. **UI/UX:** Intuitive and user-friendly interface.
4. **Database Interaction:** Proper implementation of CRUD operations.
5. **Error Handling:** Appropriate exception management and user notifications in case of errors.

---

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

#### Optional: Add Initial Employees
```
INSERT INTO [DATwise].[dbo].[Employees] ( [FirstName], [LastName], [Email], [Phone], [HireDate])
VALUES
('John', 'Doe', 'johndoe@example.com', '0521234567', '2021-05-15'),
('Jane', 'Smith', 'janesmith@example.com', '0539876543', '2020-08-20'),
('Michael', 'Johnson', 'michaeljohnson@example.com', '0541239876', '2019-11-01'),
('Emily', 'Brown', 'emilybrown@example.com', '0522345678', '2022-01-30'),
('David', 'Williams', 'davidwilliams@example.com', '0523456789', '2018-07-14'),
('Emma', 'Jones', 'emmajones@example.com', '0548765432', '2017-09-18'),
('Daniel', 'Garcia', 'danielgarcia@example.com', '0534567890', '2021-03-22'),
('Sophia', 'Martinez', 'sophiamartinez@example.com', '0545678901', '2020-11-12'),
('Olivia', 'Hernandez', 'oliviahernandez@example.com', '0529876543', '2019-02-25'),
('Noah', 'Lopez', 'noahlopez@example.com', '0538765432', '2022-06-10');

```

## Running The Project
- Make sure you've configured everything.

## Technologies I've Used for the Project
- .Net Web Forms
- Microsoft SQL Server.
- accessibility-widget.pages.dev


CREATE TABLE Employee(
Emp_Id INT PRIMARY KEY IDENTITY(1,1)
,Emp_FirstName VARCHAR(200) NOT NULL
,Emp_MiddleName VARCHAR(200)
,Emp_LastName VARCHAR(200) NOT NULL
,Emp_DeptId INT NOT NULL
,Emp_Salary INT NOT NULL
,Emp_IsActive Varchar(5) NOT NULL
,Emp_IsDeleted BIT NOT NULL
,Emp_CreatedOn DateTime NOt NULL
,Emp_CreatedBy INT NOT NULL
,Emp_ModifiedOn DateTime 
,Emp_ModifiedBy INT,
Constraint Employee_Department Foreign Key (Emp_DeptId)
References Department(Dept_Id)
)


CREATE TABLE Department(
Dept_Id INT PRIMARY KEY IDENTITY(1,1)
,Dept_Name VARCHAR(100) NOT NULL
,Dept_Code Varchar(10) NOt NULL
)

Insert into Department
VALUES
('Sales','AAAA')
,('HR','AAAB')
,('Support','AAAC')
,('Management','AAAD')
,('Marketing','AAAE')
,('IT','AAAF')
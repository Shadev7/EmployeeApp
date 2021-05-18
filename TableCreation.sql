USE PersonalDatabase
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'employees'))
BEGIN
CREATE TABLE employees (
	EmployeeID INT PRIMARY KEY,
	FirstName VARCHAR (20) DEFAULT NULL,
	LastName VARCHAR (25) NOT NULL,
	Email VARCHAR (100) NOT NULL,
	Phone VARCHAR (20) DEFAULT NULL,
	JobId INT NOT NULL,
	ManagerId INT DEFAULT NULL,
	DepartmentId INT DEFAULT NULL,
	FOREIGN KEY (ManagerId) REFERENCES employees (EmployeeID)
);
END
go

USE PersonalDatabase
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Departments'))
BEGIN
CREATE TABLE Departments (
	DepartmentId INT PRIMARY KEY,
	DepartmentName VARCHAR (100) NOT NULL
);
END
go


IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Jobs'))
BEGIN
CREATE TABLE Jobs (
	JobId INT PRIMARY KEY,
	JobTitle VARCHAR (100) NOT NULL
);
END
go

/*DROP TABLE employees*/
USE PersonalDatabase
select * from employees
Insert into employees values (100,'FName1','LName1','FName1.LName1@mymail.com','100-100-1000',5,100,2)
Insert into employees values (101,'FName1.1','LName1.1','FName11.LName11@mymail.com','100-100-1000',5,100,2)

go
update employees set ManagerId = 100 Where EmployeeID = 100

Insert into Departments values (0, '_Admin_')
Insert into Departments values (1, 'HR')
Insert into Departments values (2, 'IT')
Insert into Departments values (3, 'Finance')
Insert into Departments values (4, 'Legal')
Insert into Departments values (5, 'Sales')
Insert into Departments values (6, 'Facilities')

select * from Departments

drop table Department

Update employees set JobId = 2 where EmployeeID =100


Insert into Jobs values (2, 'Manager')
Insert into Jobs values (5, 'Consultant')

ALTER TABLE employees
ADD CONSTRAINT FK_JobID
FOREIGN KEY (JobId) REFERENCES Jobs(JobId); 

ALTER TABLE employees
ADD CONSTRAINT FK_DepartmentId
FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId); 


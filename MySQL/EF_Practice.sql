/*
Create DataBase EF_Practice;

CREATE TABLE Departments
(
     ID INT PRIMARY KEY IDENTITY(1,1),
     Name VARCHAR(50),
     Location VARCHAR(50)
)

CREATE TABLE Employees
(
     ID INT PRIMARY KEY IDENTITY(1,1),
     Name VARCHAR(50),
     Email VARCHAR(50),
     Gender VARCHAR(50),
     Salary INT,
     DepartmentId INT FOREIGN KEY REFERENCES Departments(ID)
)

--Populate the Departments table with some test data
INSERT INTO Departments VALUES ('IT', 'Multan')
INSERT INTO Departments VALUES ('HR', 'lahore')
INSERT INTO Departments VALUES ('Sales', 'Islamabad')

INSERT INTO Employees VALUES ('Mark', 'Mark@g.com', 'Male', 60000, 1)
INSERT INTO Employees VALUES ('Steve', 'Steve@g.com', 'Male', 45000, 3)
INSERT INTO Employees VALUES ('Pam', 'Pam@g.com', 'Female', 60000, 1)
INSERT INTO Employees VALUES ('Sara', 'Sara@g.com', 'Female', 345000, 3)
INSERT INTO Employees VALUES ('Ben', 'Ben@g.com', 'Male', 70000, 1)
INSERT INTO Employees VALUES ('Philip', 'Philip@g.com', 'Male', 45000, 2)
INSERT INTO Employees VALUES ('Mary', 'Mary@g.com', 'Female', 30000, 2)
INSERT INTO Employees VALUES ('Valarie', 'Valarie@g.com', 'Female', 35000, 3)
INSERT INTO Employees VALUES ('John', 'John@g.com', 'Male', 80000, 1)

Select * From Departments;
Select * From Employees;

*/


Drop Database EF_Practice;

Create Database SchoolDb;

Select * Form Students;
Select * From Grades;

/*
CREATE TABLE Persons (
    PersonID int,
    LastName varchar(255),
    FirstName varchar(255),
    Address varchar(255),
    City varchar(255)
);
Select * from Persons;
Drop Table Persons;

SELECT LastName, FirstName 
INTO TestTable 
FROM Persons;

Select * from Persons;
Select * from TestTable;

ALTER TABLE Persons
ADD Email varchar(255);
Select * from Persons;

EXEC sp_rename 'Persons.Email',  'PersonEmail', 'COLUMN';

ALTER TABLE Persons
ALTER COLUMN PersonEmail varchar(200);

ALTER TABLE Persons
ALTER COLUMN PersonID int NOT NULL;

ALTER TABLE Persons
ADD UNIQUE(Address);

Alter Table Persons
Add Constraint d_value
Default 'Some' For LastName;

// Identity being used for auto Icrement
CREATE TABLE Products (
    ProductId int IDENTITY(1,1) PRIMARY KEY,
    ProductName varchar(255) NOT NULL,
    Price int
);
// View Code
Create View PersonView as
Select FirstName,LastName
From Persons
where PersonID > 0;


*/





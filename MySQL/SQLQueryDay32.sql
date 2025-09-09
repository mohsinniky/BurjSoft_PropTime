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






*/







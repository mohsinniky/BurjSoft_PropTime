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

-- Insert sample data into Persons table
INSERT INTO Persons (PersonID, LastName, FirstName, Address, City, PersonEmail)
VALUES
(1, 'Smith', 'John', '123 Main St', 'New York', 'john.smith@email.com'),
(2, 'Johnson', 'Emily', '456 Oak Ave', 'Los Angeles', 'emily.j@email.com'),
(3, 'Williams', 'Michael', '789 Pine Rd', 'Chicago', 'm.williams@email.com'),
(4, 'Brown', 'Sarah', '321 Elm Ln', 'Houston', 'sarah.brown@email.com'),
(5, 'Davis', 'David', '654 Maple Dr', 'Phoenix', 'david.davis@email.com'),
(6, 'Miller', 'Jennifer', '987 Cedar Ct', 'Philadelphia', 'jennifer.m@email.com');

-- Verify the inserted data
SELECT * FROM Persons;






*/
-- Select Distinct
Select Distinct FirstName,LastName From Persons;

-- Count Usage
Select Count(Distinct FirstName) From Persons;

--  Where
Select * from Persons where PersonID != 2;
Select * from Persons where PersonID = 2;
Select * from Persons where PersonID Between 2 AND 5;
Select * from Persons where FirstName like 'Emily';
Select * from Persons where LastName in ('Smith','Williams');

-- Order By
Select PersonID,FirstName,LastName From Persons
ORDER BY FirstName DESC;

Select PersonID,FirstName,LastName From Persons
ORDER BY FirstName DESC, LastName DESC;

-- Not
Select * from Persons where NOT PersonID != 2;
Select * from Persons where NOT FirstName like 'E%';

-- NULL
Select * from Persons where FirstName IS NOT NULL;
Select * from Persons where FirstName IS NULL;

-- Update, In update where is a must else all rows will be affected
UPDATE Persons
Set FirstName='Mohsin', LastName= 'Raza'
where PersonID=1;


-- Delete 
Delete from Persons where PersonEmail= 'emily.j@email.com';


-- Select Top
Select top 3 * from Persons where FirstName IS NOT NULL;
Select * from Persons LIMIT 3 where FirstName IS NOT NULL ;




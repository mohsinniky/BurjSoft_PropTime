Select p.Name, Max(mr.RecordDate)
from Patient p
Join MedicalRecord mr On 
mr.PatientId = p.Id
Group by p.Name;


Create Procedure DisplayPatient
As
Begin
	Select * From Patient
	Select * From Doctor
End;

Exec DisplayPatient;


-- With Params

Create Procedure IdProcedure @PatientId int
As
Begin
	Select * From Patient 
	Where Id = @PatientId
End;

Exec IdProcedure @PatientId = 9;
Drop Procedure DisplayPatient;


Declare @DateVariable bit 
Set @DateVariable = 3
Select @DateVariable as DateVar

Create Table #Test (
Name varchar(100),
Age int ,
DOB Date,
ExpireDate DateTime
)

Select * From #Test


Insert Into #Test Values (
'Mohsin',
20 ,
'2000-02-02',
'2000-02-02'
)
Insert Into #Test Values (
'Mohsin',
20 ,
'02_22_2002',
'2000_02_02'
)

SELECT CAST('02.02.2000 ' AS DATE) ;



Alter Procedure TableCreation
As
Begin
	Declare @WhereDate Date = '1950-02-03'
	Select * 
	Into #NewTable
	From Patient p
	Where p.DateOfBirth > @WhereDate;


	Select * From #NewTable;

End;

Drop Table #NewTable;

Exec TableCreation


Create Type StudentTableType as Table(
Id int,
Name Varchar(50)
)

CREATE TABLE StudentsTable(
Id int,
Name Varchar(50)
);

Alter Procedure InsertStudent
@Students StudentTableType ReadOnly 	-- Parameter
As

Begin
Insert into StudentsTable
Select Id,Name from @Students;

Select * From StudentsTable
End;

Declare @StudentData StudentTableType
Insert into @StudentData Values (1,'Mohsin'), (2,'Raza');
Exec InsertStudent @StudentData;


Create Index DoctorIdIndex on Doctor(Id);

Create Procedure TestProcedure
-- Parameter 
-- Create database HospitalManagement;
-- Drop Database HospitalManagement

/* Create Table Patient(
Id int Identity(1,1) Primary Key,
Name Varchar(100) Not Null,
DateOfBirth Date,
Gender Varchar(100) Not Null,
IsDeleted Bit Default(0)
); */

Create Table MedicalRecord(
Id int Identity(1,1) Primary Key,
MedicalRecordDate Date Not Null,
DetailedInfo text, 
PatientID int Foreign Key references Patient(Id) 
);

-- Remove Column
Alter Table MedicalRecord
Drop Column PatientId;

-- Rename Column
exec sp_rename "MedicalRecord.MedicalRecordDate", "RecordDate", "COLUMN";

-- Changetype
Alter Table MedicalRecord
Alter Column DetailedInfo Varchar(MAX);

-- Remove Constraint
Alter Table MedicalRecord
Drop Constraint FK__MedicalRe__Patie__3B75D760;


-- Remove Column
Alter Table MedicalRecord
Drop Column PatientId;


-- Adding Column
Alter Table MedicalRecord
Add PatientId int ;

-- Changing the Key To Foreign Key
Alter Table MedicalRecord
Add Foreign Key(PatientId) REFERENCES Patient(Id);

-- Adding the key Constraint
Alter Table MedicalRecord
Add Constraint FK_MedicalRecord_Patients
Foreign Key(PatientId) REFERENCES Patient(Id);


-- Creating the other Two tables
Create Table Doctor(
Id int Primary Key,
FirstName Varchar(100) Not Null,
LastName Varchar(100),
Specialty Varchar(100) Not Null
);

Create Table MedicalPractice(
Id int Primary key,
Description Varchar(100), 
DatePerformed Date Not Null,
Diagnosis text Not Null,
PatientId int Not Null,
DoctorId int Not Null,
Outcome Varchar(200) Not Null
);

-- Adding the key Constraint 
Alter Table MedicalPractice
Add Constraint FK_MedicalPractice_Patients
Foreign Key(PatientId) REFERENCES Patient(Id);

Alter Table MedicalPractice
Add Constraint FK_MedicalPractice_Doctors
Foreign Key(DoctorId) REFERENCES Doctor(Id);








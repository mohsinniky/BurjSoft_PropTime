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


-- Adding Column in a single Query
Alter table MedicalRecord
Drop Constraint FK_MedicalRecord_Patients;

Alter table MedicalRecord
Drop Column PatientId;

Alter table MedicalRecord
Add PatientId int, Constraint FK_MedicalRecord_Patient
Foreign Key(PatientId) references Patient(id);

Alter table MedicalRecord
Drop Constraint FK_MedicalRecord_Patient, Column PatientId;

-- Adding Data
Alter table MedicalRecord
Add PatientId int;
-- Single
Insert Into MedicalRecord(RecordDate, DetailedInfo, PatientID) values('2002-08-25','Cancer',1);
-- Multiple
Insert Into MedicalRecord(RecordDate, DetailedInfo, PatientID) 
values('2002-08-25','Cancer2',2),
('2002-08-01','Cancer3',3),
('2002-08-05','Cancer4',4);

Select * from MedicalRecord;

Alter Table MedicalRecord
Add constraint FK_MedicalRecord_Patient
Foreign Key(PatientId) References Patient(Id);


Truncate Table MedicalRecord;

-- Insert 10 Patients
INSERT INTO Patient (Name, DateOfBirth, Gender)
VALUES
('Ali Raza', '1985-01-15', 'Male'),
('Sara Khan', '1990-03-22', 'Female'),
('Ahmed Butt', '1978-07-09', 'Male'),
('Fatima Tariq', '1988-11-30', 'Female'),
('Bilal Qureshi', '1992-05-17', 'Male'),
('Ayesha Malik', '1995-09-25', 'Female'),
('Usman Javed', '1982-12-05', 'Male'),
('Hina Sheikh', '1993-04-18', 'Female'),
('Zain Abbas', '1987-08-13', 'Male'),
('Mehwish Ali', '1991-02-27', 'Female');

-- Insert 10 Doctors
INSERT INTO Doctor (Id, FirstName, LastName, Specialty)
VALUES
(1, 'Asad', 'Iqbal', 'Cardiology'),
(2, 'Sadia', 'Naseer', 'Dermatology'),
(3, 'Hamza', 'Rashid', 'Orthopedics'),
(4, 'Nimra', 'Shah', 'Pediatrics'),
(5, 'Rizwan', 'Ali', 'Neurology'),
(6, 'Farah', 'Khan', 'Gynecology'),
(7, 'Salman', 'Qadir', 'ENT'),
(8, 'Hassan', 'Jamil', 'Oncology'),
(9, 'Saba', 'Rauf', 'Psychiatry'),
(10, 'Adnan', 'Siddiqui', 'Urology');

-- Insert 20 MedicalRecords (referencing the 10 patients)
INSERT INTO MedicalRecord (RecordDate, DetailedInfo, PatientId)
VALUES
('2023-01-10', 'Blood Pressure Check', 1),
('2023-02-15', 'Diabetes Screening', 2),
('2023-03-20', 'Fracture X-Ray', 3),
('2023-04-25', 'Pregnancy Test', 4),
('2023-05-30', 'Migraine Consultation', 5),
('2023-06-05', 'Skin Allergy', 6),
('2023-07-12', 'Ear Infection', 7),
('2023-08-18', 'Cancer Screening', 8),
('2023-09-22', 'Depression Assessment', 9),
('2023-10-29', 'Kidney Function Test', 10),
('2023-11-03', 'Stomach Pain Follow-up', 1),
('2023-12-14', 'Thyroid Check', 2),
('2024-01-19', 'Asthma Follow-up', 3),
('2024-02-23', 'Eye Vision Test', 4),
('2024-03-28', 'Arthritis Evaluation', 5),
('2024-04-30', 'Child Vaccination', 6),
('2024-05-15', 'Liver Function Test', 7),
('2024-06-20', 'Chest Pain Consultation', 8),
('2024-07-25', 'Back Pain Follow-up', 9),
('2024-08-30', 'Blood Test Results', 10);

-- Insert 20 MedicalPractice records (referencing 10 patients and 10 doctors)
INSERT INTO MedicalPractice (Id, Description, DatePerformed, Diagnosis, PatientId, DoctorId, Outcome)
VALUES
(1, 'Angioplasty', '2023-01-15', 'Blocked Artery', 1, 1, 'Successful'),
(2, 'Skin Biopsy', '2023-02-20', 'Skin Rash', 2, 2, 'Normal'),
(3, 'Bone Surgery', '2023-03-25', 'Fracture', 3, 3, 'Recovered'),
(4, 'Vaccination', '2023-04-30', 'Routine Immunization', 4, 4, 'Completed'),
(5, 'EEG', '2023-05-05', 'Seizure', 5, 5, 'Stable'),
(6, 'Ultrasound', '2023-06-10', 'Pregnancy', 6, 6, 'Healthy'),
(7, 'Tonsillectomy', '2023-07-15', 'Tonsillitis', 7, 7, 'Successful'),
(8, 'Chemotherapy', '2023-08-20', 'Cancer', 8, 8, 'Ongoing'),
(9, 'Therapy Session', '2023-09-25', 'Depression', 9, 9, 'Improved'),
(10, 'Cystoscopy', '2023-10-30', 'Bladder Issue', 10, 10, 'Resolved'),
(11, 'Follow-up Consultation', '2023-11-05', 'Hypertension', 1, 1, 'Stable'),
(12, 'Dermatology Follow-up', '2023-12-10', 'Skin Condition', 2, 2, 'Improved'),
(13, 'Physical Therapy', '2024-01-15', 'Joint Pain', 3, 3, 'Progressing'),
(14, 'Pediatric Checkup', '2024-02-20', 'Child Development', 4, 4, 'Normal'),
(15, 'Neurology Follow-up', '2024-03-25', 'Migraine', 5, 5, 'Better'),
(16, 'Gynecology Visit', '2024-04-30', 'Routine Check', 6, 6, 'Healthy'),
(17, 'ENT Consultation', '2024-05-05', 'Hearing Test', 7, 7, 'Normal'),
(18, 'Oncology Follow-up', '2024-06-10', 'Cancer Treatment', 8, 8, 'Stable'),
(19, 'Psychiatry Session', '2024-07-15', 'Anxiety', 9, 9, 'Improving'),
(20, 'Urology Follow-up', '2024-08-20', 'Kidney Stones', 10, 10, 'Resolved');


Select * From Patient;
Select * From Doctor;
Select * From MedicalRecord;
Select * From MedicalPractice;
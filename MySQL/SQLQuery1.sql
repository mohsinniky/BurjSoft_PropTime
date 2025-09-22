Select * From Patient;
Select * From Doctor;
Select * From MedicalRecord;
Select * From MedicalPractice;


Select Name from Patient where Gender = 'Male' AND IsDeleted = 0 AND Name like '%la%';
Select * from Patient where Name like '%la%';

-- Drop From 

-- Select Name,Gender from Patient where DateOfBirth > 1992-00-00 AND DateOfBirth < 1996-00-00 ;
-- only yearly based, 

-- Update DateOfBirth = '2000-05-19'

Select p.Name m.DetailedInfo
from  Patient p
INNER JOIN MedicalRecord m ON
where p.ID = m.PatientId AND p.Gender = Female;

--Delete Query 
Delete From MedicalRecord where PatientId = 5;
Delete From MedicalPractice where PatientId = 5;
Delete From Patient Where Id = 5;

Update Patient 
Set DateOfBirth = '2000-01-01'
where Id = 4;

Select Name,Gender from Patient where DateOfBirth > '1992-01-01' AND DateOfBirth < '1996-01-01' ;

Select p.Name, m.DetailedInfo
from  Patient p
INNER JOIN MedicalRecord m ON
p.ID = m.PatientId AND p.Gender = 'Female';


Update Patient
Set IsDeleted = 1
where Id = 4 or Id= 6;


Select Name,Gender from Patient where YEAR(DateOfBirth) > 1992 AND DateOfBirth < '1996-01-01' ;


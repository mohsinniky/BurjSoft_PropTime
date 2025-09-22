
--    **Beginner Level (1-15):**
-- 1. Retrieve all columns from the Patient table.
Select * From Patient;
-- 2. Show only the names and genders of all patients.
Select Name,Gender From Patient;
-- 3. List all doctors with their first name, last name, and specialty.
Select FirstName, LastName, Specialty from Doctor;
-- 4. Find all medical records from the year 2023.
Select * From MedicalRecord Where YEAR(RecordDate) = 2023;
-- 5. Show patients born before 1990.
Select * From Patient Where YEAR(DateOfBirth) < 1990;
-- 6. List female patients only.
Select * From Patient Where Gender = 'Female';
-- 7. Find all medical practices performed in March of any year.
Select * From MedicalPractice Where Month(DatePerformed)= 3;
-- 8. Show doctors whose specialty contains 'ology'.
Select * From Doctor where Specialty Like '%ology';
-- 9. List patients whose names start with 'A'.
Select * From Patient Where Name like 'A%';
-- 10. Find medical records for patient with ID 5.
Select * From MedicalRecord where PatientId = 6;
-- 11. Show all medical practices with 'Successful' outcome.
Select * from MedicalPractice where Outcome = 'Successful';
-- 12. List doctors in alphabetical order by last name.
Select * from Doctor Order By LastName;
-- 13. Find patients born between 1980 and 1990.
Select * From  Patient Where YEAR(DateOfBirth) > 1980 AND YEAR(DateOfBirth) < 1990;
-- 14. Show medical records with DetailedInfo containing 'Test'.
Select * From MedicalRecord where DetailedInfo like '%Test%';
-- 15. List all unique specialties from the Doctor table.
Select Distinct Specialty From Doctor;

--     **Basic Intermediate Level (16-30):**
-- 16. Count how many patients are male and how many are female.
Select Gender,Count(Id) As 'Total' From Patient Group BY Gender;
-- 17. Find the oldest patient's name and date of birth.
Select Name, DateOfBirth From Patient Where DateOfBirth = 
( Select Min(DateOfBirth) From Patient);
-- 18. List doctors along with how many medical practices they've performed.
Select d.Id,d.FirstName,d.Specialty , Count(mp.Id) as 'Patient Count' 
From Doctor d
Inner Join MedicalPractice mp ON
d.Id = mp.DoctorId Group By d.Id,d.FirstName,d.Specialty;
-- 19. Show patients who have more than 1 medical record.
Select * From Patient where Id in 
(Select PatientId from MedicalRecord Group By PatientId Having Count(PatientId) > 1 
); -- Self Completed, NO AI
-- 20. Find the most recent medical record for each patient.
Select * From MedicalRecord where RecordDate in (
Select Max(RecordDate) as Id From MedicalRecord group by PatientId );




-- 21. List medical practices with their corresponding patient names and doctor names.
Select mp.*, p.Name AS 'Patient Name', d.FirstName + d.LastName As 'Doctor Name'
From MedicalPractice mp
Full Join Patient p On mp.PatientId= p.Id 
Full Join Doctor d On mp.DoctorId = d.Id;
-- 22. Show doctors who haven't performed any medical practices.
Delete From MedicalPractice where DoctorId In (1,2);
Select d.FirstName/*, mp.Description, mp.DoctorId*/
From Doctor d
Left Join MedicalPractice mp on
d.Id = mp.DoctorId where mp.Description Is Null ;

Select p1.*,p2.Name As 'Second Patient Table Namee'
from Patient p1, Patient p2
where p1.Id = p2.Id;

-- 23. Find patients who have both medical records and medical practices.
Select p.Name 
From Patient p
 Join MedicalPractice mp On
p.Id = mp.PatientId 
Left Join MedicalRecord mr On 
p.Id = mr.PatientId 
where mp.Description is not null And mr.Id is not NUll
group by p.Name;
-- 24. List medical records with their corresponding patient information.
Select mr.* , p.Name, p.Gender, p.DateOfBirth
From MedicalRecord mr
Inner Join Patient p On
p.Id = mr.PatientId;
-- 25. Show the total number of medical practices per specialty.
Select Count(mp.Id) As TotalOperations, d.Specialty
From MedicalPractice mp
Left Join Doctor d on
mp.DoctorId = d.Id 
Group By d.Specialty;
-- 26. Find patients with no medical records.
Select p.Name
From Patient p
Left Join MedicalRecord mr on
p.Id = mr.PatientId where mr.DetailedInfo Is Null ;
-- 27. List medical practices performed by cardiologists.
Select mp.*
From MedicalPractice mp
Inner Join Doctor d ON
mp.DoctorId = d.Id where d.Specialty like 'Ortho%';
Select * From Doctor;
-- 28. Show the average number of medical records per patient.
Select AVG(CountRecord) As AveragePerPatient from
(
Select Count(mr.Id) as CountRecord, p.Name
From MedicalRecord mr
Inner Join Patient p ON
mr.PatientId = p.Id 
Group BY p.Name
)As Result;

-- 29. Find doctors who have treated more than 2 patients.
Select d.FirstName + d.LastName As DoctorName
From Doctor d
Join MedicalPractice mp On 
mp.DoctorId = d.Id 
group by mp.DoctorId, d.FirstName, d.LastName
Having Count(mp.DoctorId) > 1;



30. List all medical practices with patient age at the time of procedure.

**Intermediate Level (31-45):**
31. Show patients who have medical records but no medical practices.
32. Find the busiest doctor (most medical practices performed).
33. List patients along with their first and last medical record dates.
34. Show medical practices where the doctor's specialty matches the procedure type.
35. Find patients who have been treated by multiple specialists.


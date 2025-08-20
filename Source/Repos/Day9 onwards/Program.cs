

using Oops;

namespace Day9_onwards
{
    public static class Program
    {
        public static void addToStudentList(Students studentListObject, int s_Id, string s_Name, int s_Age, bool s_IsActive)
        {
            studentListObject.studentId.Add(s_Id);
            studentListObject.studentName.Add(s_Name);
            studentListObject.studentAge.Add(s_Age);
            studentListObject.studentIsActive.Add(s_IsActive);
        }

        public static void displayStudentList(Students studentListObject)
        {

            Console.Write("ID\t");
            Console.Write("Name\t");
            Console.Write("Age\t");
            Console.Write("Is Active\t");
            Console.WriteLine();

            for (int i = 0; i < studentListObject.studentId.Count; i++)
            {
                Console.Write(studentListObject.studentId[i] + "\t");
                Console.Write(studentListObject.studentName[i] + "\t");
                Console.Write(studentListObject.studentAge[i] + "\t");
                Console.Write(studentListObject.studentIsActive[i] + "\t");
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Students studentList = new Students();

            addToStudentList(studentList, 1, "Mohsin", 19, true);
            addToStudentList(studentList, 2, "Raza", 19, true);
            addToStudentList(studentList, 3, "Hammad", 19, true);
            addToStudentList(studentList, 4, "Ayesha", 20, true);
            addToStudentList(studentList, 5, "Bilal", 22, false);
            addToStudentList(studentList, 6, "Fatima", 18, true);
            addToStudentList(studentList, 7, "Usman", 21, true);
            addToStudentList(studentList, 8, "Sana", 19, false);
            addToStudentList(studentList, 9, "Ali", 23, true);
            addToStudentList(studentList, 10, "Zainab", 20, true);
            addToStudentList(studentList, 11, "Ahmed", 18, false);
            addToStudentList(studentList, 12, "Hina", 21, true);
            addToStudentList(studentList, 13, "Kamran", 22, true);
            addToStudentList(studentList, 14, "Sadia", 19, false);
            addToStudentList(studentList, 15, "Omar", 20, true);
            addToStudentList(studentList, 16, "Amina", 18, true);
            addToStudentList(studentList, 17, "Faisal", 24, false);
            addToStudentList(studentList, 18, "Rabia", 19, true);
            addToStudentList(studentList, 19, "Tariq", 21, true);
            addToStudentList(studentList, 20, "Nadia", 22, false);

            displayStudentList(studentList);

            //Queries/Questions


        }
    }
}
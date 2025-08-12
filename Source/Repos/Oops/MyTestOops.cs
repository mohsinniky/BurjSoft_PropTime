namespace Oops
{
    public class MyTestOops
    {
        public int Id { get; set; }

        protected string name = "This Is A Protected String";

        internal string internalValue = "This is a Internal String";

    }

    public class MyPrivateOop
    {
        private int RollNumber { get; set; }

        public void PRivateShow()
        {
            RollNumber = 12;
            Console.WriteLine(RollNumber);
        }

    }

    public class MyProtectedOop : MyTestOops

    {
        public void showPOrotectedSample()
        {
            Console.WriteLine(name);
        }
    }

    public class NotMyProtectedOop

    {
        public void showPOrotectedSample2()
        {
            MyTestOops myTestOops = new MyTestOops();
            Console.WriteLine(myTestOops.Id);
            Console.WriteLine(myTestOops.internalValue);

        }
    }





}

namespace GenericsBasics
{
    class Program //Part 13 - Generics - Introduction
    {
        static void Main(string[] args)
        {
            Salaries salaries = new Salaries();
            //ArrayList employeeSalaries = salaries.GetSalaries();
            List<float> employeeSalaries = salaries.GetSalaries();


            //float salary = (float)employeeSalaries[1]; //<---unbox needed for ArrayList items
            float salary = employeeSalaries[1]; //<---unbox not needed
            salary += salary * 0.02f;

            Console.WriteLine(salary);
            Console.ReadKey();
        }
    }

    public class Salaries
    {
        //ArrayList _salaryList = new();
        List<float> _salaryList = new();
        public Salaries()
        {
            /*note:
                when using: ArrayList _salaryList = new();
                _salaryList.Add(60000.32); < ---stored as object on the managed heap(boxing)
                causes float salary = (float)employeeSalaries[1]; to produce:
                System.InvalidCastException
                HResult = 0x80004002
                Message = Unable to cast object of type 'System.Double' to type 'System.Single'.
                Source =< Cannot evaluate the exception source>
                StackTrace:
                < Cannot evaluate the exception stack trace >

                when using: List<float> _salaryList = new();
                _salaryList.Add(60000.32); <---Argument 1: cannot convert from 'double' to 'float'
            */
            _salaryList.Add(60000.32f);
            _salaryList.Add(40000.16f);
            _salaryList.Add(20000.78f);
        }

        //public ArrayList GetSalaries()
        public List<float> GetSalaries()
        {
            return _salaryList;
        }
    }
}

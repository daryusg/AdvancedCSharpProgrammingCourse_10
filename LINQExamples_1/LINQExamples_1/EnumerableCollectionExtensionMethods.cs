using static LINQExamples_1.Program;

namespace LINQExamples_1
{
    //note: i had to move the following to its own class as got error: Extension methods must be defined in a top level static class; EnumerableCollectionExtensionMethods is a nested class
    internal static class EnumerableCollectionExtensionMethods
    {
        public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees)
        {
            foreach (Employee emp in employees)
            {
                Console.WriteLine($"Accessing employee: {emp.FirstName} {emp.LastName}");
                if (emp.AnnualSalary > 50000)
                    yield return emp;
            }

        }
    }
}

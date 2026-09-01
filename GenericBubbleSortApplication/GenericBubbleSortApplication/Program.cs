namespace GenericBubbleSortApplication
{
    class Program //Part 14 - Generics - Understanding Constraints
    {
        static void Main(string[] args)
        {
            //int[] arr = new int[] { 6, 2, 5, 8, 1 };
            //int[] arr = [6, 2, 5, 8, 1]; //CS1503 Argument 1: cannot convert from 'int[]' to 'object[]' but 
            //object[] arr = [6, 2, 5, 8, 1];
            //string[] arr = ["Camel", "Elephant", "Fox", "Alligator", "Bison", "Deer"];

            Employee[] arr = new Employee[]
            {
                new Employee { Id = 5, Name = "Eve" },
                new Employee { Id = 2, Name = "Alice" },
                new Employee { Id = 4, Name = "Charlie" },
                new Employee { Id = 1, Name = "John" },
                new Employee { Id = 3, Name = "Bob" }
            };

            //SortArray sortArray = new();
            SortArray<Employee> sortArray = new();
            //-------------------------------------------------
            //SortArray<int> sortArray = new();
            //note: public readonly struct Int32 : IComparable, IComparable<Int32>, IConvertible, IEquatable<Int32>, IFormattable, IParsable<Int32>, ISpanFormattable, ISpanParsable<Int32>, IUtf8SpanFormattable, IUtf8SpanParsable<Int32>, IAdditionOperators<Int32, Int32, Int32>, IAdditiveIdentity<Int32, Int32>, IBinaryInteger<Int32>, IBinaryNumber<Int32>, IBitwiseOperators<Int32, Int32, Int32>, IComparisonOperators<Int32, Int32, bool>, IEqualityOperators<Int32, Int32, bool>, IDecrementOperators<Int32>, IDivisionOperators<Int32, Int32, Int32>, IIncrementOperators<Int32>, IModulusOperators<Int32, Int32, Int32>, IMultiplicativeIdentity<Int32, Int32>, IMultiplyOperators<Int32, Int32, Int32>, INumber<Int32>, INumberBase<Int32>, ISubtractionOperators<Int32, Int32, Int32>, IUnaryNegationOperators<Int32, Int32>, IUnaryPlusOperators<Int32, Int32>, IShiftOperators<Int32, Int32, Int32>, IMinMaxValue<Int32>, ISignedNumber<Int32>
            //  implements both the non-generic (IComparable) and generic (IComparable<Int32>) versions of the IComparable interface
            //-------------------------------------------------
            //SortArray<string> sortArray = new();
            sortArray.BubbleSort(arr);

            foreach (var item in arr)
                Console.WriteLine(item);

            Console.ReadKey();
        }
    }

    //public class Employee : IComparable
    public class Employee : IComparable<Employee>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CompareTo(Employee? other)
        {
            //return this.Id.CompareTo(other.Id); //sort by Id
            return this.Name.CompareTo(other.Name); //sort by Name
        }

        //public int CompareTo(object? obj)
        //{
        //    return this.Id.CompareTo(((Employee)obj).Id);
        //}

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }

    //public class SortArray
    //public class SortArray<T> where T : IComparable
    public class SortArray<T> where T : IComparable<T> //T must implement the generic version of the IComparable interface
    {
        //public void BubbleSort(object[] arr)
        public void BubbleSort(T[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    //if (((IComparable)arr[j]).CompareTo(arr[j + 1]) > 0)
                    if (arr[j].CompareTo(arr[j + 1]) > 0)
                        Swap(arr, j);
                }
            }
        }

        //private void Swap(object[] arr, int j)
        private void Swap(T[] arr, int j)
        {
            // Swap arr[j] and arr[j + 1]
            //object temp = arr[j];
            T temp = arr[j];
            arr[j] = arr[j + 1];
            arr[j + 1] = temp;

        }
    }
}

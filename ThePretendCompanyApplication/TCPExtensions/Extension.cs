namespace TCPExtensions
{
    public static class Extension
    {
        public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
        {
            List<T> filteredList = new();
            foreach (T record in records)
            {
                if (func(record))
                {
                    filteredList.Add(record);
                }
            }
            return filteredList;
        }
    }
}

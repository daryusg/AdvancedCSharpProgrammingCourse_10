using System;
using System.Collections.Generic;
using System.Text;

namespace HRAdministrationAPI
{
    public static class FactoryPattern<K,T> where T:class, K, new() //20260619 Part 2 - Overview of the Advanced C# Course
    {
        public static K GetInstance()
        {
            K objK;
            objK = new T();
            return objK;
        }
    }
}

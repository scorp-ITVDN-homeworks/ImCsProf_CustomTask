using System;

namespace CustomArray.MyPoorManTests
{
    class Program
    {
        // не умею в юнит-тестирование
        // и загрузку nuget-пакетов
        static void Main(string[] args)
        {
            var testArray = new CustomArray<int>(-8, 2, 3, 6, 33, 17, 27, 11);

            foreach(int item in testArray)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(new string('-', 50));

            Console.WriteLine(testArray.First);
            Console.WriteLine(testArray.Last);
            int lastIndex = 0;
            if (lastIndex <= testArray.Last)
                Console.WriteLine(testArray[lastIndex]);

            try
            {
                Console.WriteLine(testArray[-2]);
            }
            catch { }

            Console.WriteLine(new string('-', 50));

            foreach (int item in testArray)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(new string('-', 50));

            foreach(int item in testArray.Array)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();

            Console.WriteLine(new string('-', 50));

            int[] lst = null;
            CustomArray<int> errorArray = new CustomArray<int>(-5, lst);

            Console.ReadKey();
        }
    }
}

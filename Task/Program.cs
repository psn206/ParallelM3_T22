using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размерность массива: ");
            int sizeArr = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> funk1 = new Func<object, int[]>(GetArr);
            Task<int[]> task1 = new Task<int[]>(funk1, sizeArr);

            Func<Task<int[]>, int[]> funk2 = new Func<Task<int[]>, int[]>(GetSum);
            Task<int[]> task2 = task1.ContinueWith<int[]>(funk2);

            Action<Task<int[]>> action = new Action<Task<int[]>>(GetMax);
            Task task3 = task2.ContinueWith(action);

            task1.Start();
            Console.ReadKey();

        }

        static int[] GetArr(object sizeArr)
        {
            int n = (int)sizeArr;
            int[] arr = new int[n];
            Random rnd = new Random();
            Console.WriteLine($"Массив случайных чисел размерностью {n}:");
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next(0, 100);
                Console.Write($"{arr[i]} ");
            }
            return arr;
        }
        static int[] GetSum(Task<int[]> task)
        {
            int[] arr = task.Result;
            int sum = arr.Sum();
            Console.WriteLine($"\nСумма массива: {sum}");
            return arr;
        }
        static void GetMax(Task<int[]> task)
        {
            int[] arr = task.Result;
            int max = arr.Max();
            Console.WriteLine($"Максимаьное число в массиве: {max}");

        }
    }
}

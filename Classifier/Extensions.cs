using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public static class Extensions
    {
        public static T ToEnum<T>(this string enumString) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }

        public static void PrintAll<T>(this IEnumerable<T> list, string title)
        {
            Console.WriteLine(title.ToUpper() + ":");
            foreach (T item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }

        public static T Enumerize<T>(this string enumString)
        {
            enumString = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(enumString.ToLower()).Replace(' ', '_');
            return (T)Enum.Parse(typeof(T), enumString);
        }

        public static void Print(this double[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.Write(array[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

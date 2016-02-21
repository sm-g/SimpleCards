using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCards.Engine
{
    static class Extensions
    {
        static Random random = new Random();
        public static void Shuffle<T>(this List<T> array)
        {
            for (int i = array.Count; i > 1; i--)
            {
                int j = random.Next(i); // 0 <= j <= i-1
                T tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
        }
    }
}

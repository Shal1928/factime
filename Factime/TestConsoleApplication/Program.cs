using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UseAbilities.Extensions.EnumerableExt;

namespace TestConsoleApplication
{
    public class IntWrap
    {
        public IntWrap(int i)
        {
            Value = i;
        }

        public int Value
        {
            get; 
            set;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var nums = new[] { 1, 1, 1, 2, 3, 2, 2, 3, 3, 4, 4, 5, 5, 6, 3 ,6, 7 };

            var nums2 = new[]
                            {
                                new IntWrap(8),
                                new IntWrap(4),
                                new IntWrap(2),
                                new IntWrap(2),
                                new IntWrap(4),
                                new IntWrap(7),
                                new IntWrap(4),
                            };

            IEnumerable<int> top5 = nums
                       .GroupBy(i => i)
                       .OrderByDescending(g => g.Count())
                       .Take(5)
                       .Select(g => g.Key);

            Console.WriteLine(nums.GetFrequentlyValues(1).FirstOrDefault());
            Console.WriteLine(DateTime.Now.Month);

            Console.ReadLine();
        }
    }
}

using System;
using System.Linq;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var list = cookies.OrderBy(x => x).ToList();
            int operations = 0;
            while (list.Count > 1 && list[0] <= k)
            {
                operations++;
                var newSweatness = list[0] + 2 * list[1];
                list.RemoveAt(0);
                list.RemoveAt(0);
                list.Add(newSweatness);
                list = list.OrderBy(x => x).ToList();
            }

            if (list[0] > k)
            {
                return operations;
            }
            return -1;
        }
    }
}

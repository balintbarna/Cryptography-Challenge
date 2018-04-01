using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoChallenge
{
    class U
    {
        public static IEnumerable<int> range(int start, int count)
        {
            return Enumerable.Range(start, count);
        }

        public static IEnumerable<int> range(int start, int count, int step)
        {
            return Enumerable.Range(start, count).Select((x, i) => (step - 1) * i + x);
        }

        public static IEnumerable<int> through(int from, int to)
        {
            return range(from, to - from + 1);
        }

        public static IEnumerable<int> through(int from, int to, int step)
        {
            return range(from, (to - from) / 2 + 1, step);
        }
    }
}

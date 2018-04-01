using System;
using System.Linq;

namespace CryptoChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Challenges();

            Console.ReadKey();
        }

        static void Challenges()
        {
            Set1();
        }

        static void Set1()
        {

            string separator = "\n-------------------------------------------\n";
            Console.WriteLine(separator);
            Console.WriteLine("HexAndBase64Test " + (TestCases.HexAndBase64Test() ? "successful" : "failed"));
            Console.WriteLine(separator);
            Console.WriteLine("HexXorTest " + (TestCases.HexXorTest() ? "successful" : "failed"));
            Console.WriteLine(separator);
            Console.WriteLine("SingleCharacterEncryption " + (TestCases.SingleCharacterEncryption() ? "successful" : "failed"));
            Console.WriteLine(separator);
            Console.WriteLine("SingleCharacterEncryptionForManyEntries " + (TestCases.SingleCharacterEncryptionForManyEntries() ? "successful" : "failed"));
            Console.WriteLine(separator);
            Console.WriteLine("RepeatingXorTest " + (TestCases.RepeatingXorTest() ? "successful" : "failed"));
            Console.WriteLine(separator);
            Console.WriteLine("HemmingTest " + (TestCases.HemmingTest() ? "successful" : "failed"));
            Console.WriteLine(separator);
            Console.WriteLine("BreakingRepeatingKeyXor " + (TestCases.BreakingRepeatingKeyXor() ? "successful" : "failed"));
            Console.WriteLine(separator);
        }
    }
}

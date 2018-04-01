using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CryptoChallenge
{
    class TestCases
    {
        public static bool HexAndBase64Test()
        {
            string start = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            string output = Helpers.HexToBase64(start);
            string expectedOutput = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";
            //Console.WriteLine(output);
            return output == expectedOutput;
        }

        public static bool HexXorTest()
        {
            string input1 = "1c0111001f010100061a024b53535009181c";
            string input2 = "686974207468652062756c6c277320657965";
            string expectedOutput = "746865206b696420646f6e277420706c6179";
            string output = Helpers.BytesToHex(Helpers.ByteXor(Helpers.HexToBytes(input1), Helpers.HexToBytes(input2)));
            //Console.WriteLine(output);
            return output == expectedOutput;
        }

        public static bool SingleCharacterEncryption()
        {
            string input = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            var bytes = Helpers.HexToBytes(input);
            foreach (char c in Helpers.legitChars)
            {
                var resultText = new String(Helpers.ByteXor(bytes, c).Select(b => (char)b).ToArray());
                if (!Helpers.IsRealText(resultText)) continue;
                Console.WriteLine(c + ":\t" + resultText);
                return true;
            }
            return false;
        }

        public static bool SingleCharacterEncryptionForManyEntries()
        {
            var entries = File.ReadAllLines("4.txt");
            bool had = false;
            for(int i = 0; i < entries.Length; i++)
            {
                var bytes = Helpers.HexToBytes(entries[i]);
                //foreach (char c in Helpers.smallLetters + Helpers.capitalLetters)
                foreach(char c in Helpers.legitChars)
                {
                    var resultText = new String(Helpers.ByteXor(bytes, c).Select(b => (char)b).ToArray());

                    if (!Helpers.IsRealText(resultText)) continue;
                    StringBuilder output = new StringBuilder("line: " + i).Append("\t")
                        .Append("hex: " + entries[i]).Append("\t")
                        //.Append("original: " + new String(bytes.Select(b => (char)b).Where(x => !"\r\n".Contains(x)).ToArray())).Append("\t")
                        .Append("char: " + c).Append("\t")
                        .Append("XORd: " + resultText);
                    Console.WriteLine(output);
                    had = true;
                }
            }
            return had;
        }

        public static bool RepeatingXorTest()
        {
            var expectedOutput = "0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f";
            var input = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";
            var key = "ICE";
            var resultString = Helpers.StringXor(input, key);
            //convert to hex
            var result = Helpers.BytesToHex(resultString.ToCharArray().Select(c => (byte)c).ToArray());
            return result == expectedOutput;
        }

        public static bool HemmingTest()
        {
            string one = "this is a test";
            string other = "wokka wokka!!!";
            int dist = Helpers.BitWiseHemmingDistance(one, other);
            return dist == 37;
        }

        public static bool BreakingRepeatingKeyXor()
        {
            // set valid keysizes
            int minkeysize = 1;
            int maxkeysize = 100;
            // read input data into byte[]
            var input = Convert.FromBase64String(File.ReadAllLines("6.txt").Aggregate((s1, s2) => s1 + s2));
            var mindist = Double.MaxValue;
            var keysize = 0;
            foreach(int i in U.through(minkeysize, maxkeysize))
            {
                var first = input.Take(i).ToArray();
                var second = input.Skip(i).Take(i).ToArray();
                var third = input.Skip(2 * i).Take(i).ToArray();
                var fourth = input.Skip(3 * i).Take(i).ToArray();
                var dist1 = Helpers.BitWiseHemmingDistance(first, second);
                var dist2 = Helpers.BitWiseHemmingDistance(third, fourth);
                var dist = (dist1 + dist2) / 2.0 / i;
                if(dist < mindist)
                {
                    mindist = dist;
                    keysize = i;
                }
            }
            var blockCount = input.Length / keysize;
            // we have the keysize
            var keyArr = new byte[keysize];
            var scores = new double[keysize];

            // for each position in block/key
            foreach(int i in U.range(0, keysize))
            {
                // init score
                scores[i] = double.MaxValue;
                // create byte array with first byte of each block
                var bytes = input.Where((b, index) => (index % keysize) == i).ToArray();
                // for each possible character
                foreach (byte b in U.through(0, 127))
                {
                    var xored = Encoding.ASCII.GetString(Helpers.ByteXor(bytes, b));
                    // if seems legint let's score
                    if (Helpers.IsRealText(xored))
                    {
                        var score = StatisticalAnalysis.RateText(xored);
                        // if score is better than previous best, make this the winner
                        if (score < scores[i])
                        {
                            scores[i] = score;
                            keyArr[i] = b;
                        }
                    }
                }
            }
            if (scores.Any(d => d == double.MaxValue)) return false;
            var key = Encoding.ASCII.GetString(keyArr);
            Console.WriteLine("Key: " + key);
            // we have the key, lets translate the whole thing
            var output = Encoding.ASCII.GetString(Helpers.ByteXor(input, Encoding.ASCII.GetBytes(key)));
            Console.WriteLine(output);
            return true;
        }

        
    }
}

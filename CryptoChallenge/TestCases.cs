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
            Console.WriteLine(output);
            return output == expectedOutput;
        }

        public static bool HexXorTest()
        {
            string input1 = "1c0111001f010100061a024b53535009181c";
            string input2 = "686974207468652062756c6c277320657965";
            string expectedOutput = "746865206b696420646f6e277420706c6179";
            string output = Helpers.BytesToHex(Helpers.ByteXor(Helpers.HexToBytes(input1), Helpers.HexToBytes(input2)));
            Console.WriteLine(output);
            return output == expectedOutput;
        }

        public static bool SingleCharacterEncryption()
        {
            string input = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            var bytes = Helpers.HexToBytes(input);
            foreach (char c in Helpers.allChars)
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
                foreach(char c in Helpers.allChars)
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

        public static bool BreakingRepeatingKeyXor()
        {
            //test hemming distance
            string one = "this is a test";
            string other = "wokka wokka!!!";
            return false;
        }
    }
}

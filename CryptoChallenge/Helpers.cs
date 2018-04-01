using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoChallenge
{
    class Helpers
    {
        public static readonly string capitalLetters = GetCharacterRange('A','Z');
        public static readonly string smallLetters = GetCharacterRange('a', 'z');
        public static readonly string numbers = GetCharacterRange('0', '9');
        public static readonly string punctuation = GetCharacterRange('0', '9');
        public static readonly string basicCharacters = " '.?!:,;-\n\r\t";
        public static readonly string legitChars = capitalLetters + smallLetters + numbers + basicCharacters;
        public static readonly string allChars = GetCharacterRange(' ', '~') + "\n\r\t";
        public static string HexToBase64(string hex)
        {
            return Convert.ToBase64String(HexToBytes(hex));
        }

        public static byte[] HexToBytes(string hex)
        {
            return Enumerable.Range(0, hex.Length / 2).Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16)).ToArray();
        }

        public static string BytesToHex(byte[] bytes)
        {
            return bytes.Select(b => b.ToString("x2")).Aggregate((s1, s2) => s1 + s2);
        }

        public static string Base64ToHex(string base64)
        {
            return BytesToHex(Convert.FromBase64String(base64));
        }

        public static byte[] ByteXor(byte[] one, byte[] other)
        {
            int len1 = one.Length;
            int len2 = other.Length;
            int maxLen = len1 > len2 ? len1 : len2;
            var result = new byte[maxLen];
            for (int i = 0; i < maxLen; i++)
                result[i] = (byte)(one[i % len1] ^ other[i % len2]);

            return result;
        }

        public static byte[] ByteXor(byte[] one, byte other)
        {
            int len = one.Length;
            var result = new byte[len];
            for (int i = 0; i < len; i++)
                result[i] = (byte)(one[i] ^ other);

            return result;
        }

        public static byte[] ByteXor(byte[] one, char other)
        {
            return ByteXor(one, (byte)other);
        }

        public static string StringXor(string one, string other)
        {
            var bytesone = Encoding.ASCII.GetBytes(one);
            var bytesother = Encoding.ASCII.GetBytes(other);
            var bytesresult = ByteXor(bytesone, bytesother);
            return new String(bytesresult.Select(b => (char)b).ToArray());
        }

        public static bool IsRealText(string text)
        {
            return text.All(c => legitChars.Contains(c));
        }

        public static string GetCharacterRange(char from, char to)
        {
            return new String(U.through(from, to).Select(i => (Char)i).ToArray());
        }

        public static int HighBitCount(byte n)
        {
            int count = 0;
            while (n != 0)
            {
                count++;
                n &= (byte)(n - 1);
            }
            return count;
        }

        public static int HighBitCount(byte[] bytes)
        {
            int sum = 0;
            foreach(var b in bytes)
            {
                sum += HighBitCount(b);
            }
            return sum;
        }

        public static int BitWiseHemmingDistance(byte[] one, byte[] other)
        {
            byte[] xor = ByteXor(one, other);
            return HighBitCount(xor);
        }

        public static int BitWiseHemmingDistance(string one, string other)
        {
            byte[] xor = ByteXor(Encoding.ASCII.GetBytes(one), Encoding.ASCII.GetBytes(other));
            return HighBitCount(xor);
        }
    }
}

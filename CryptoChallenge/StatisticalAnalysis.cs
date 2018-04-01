using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CryptoChallenge
{
    class StatisticalAnalysis
    {
        static Dictionary<char, double> ExpectedCharacterPercentages = new Dictionary<char, double>()
        {
            {'a', 8.167},
            {'b', 1.492},
            {'c', 2.782},
            {'d', 4.253},
            {'e', 12.702},
            {'f', 2.228},
            {'g', 2.015},
            {'h', 6.094},
            {'i', 6.966},
            {'j', 0.153},
            {'k', 0.772},
            {'l', 4.025},
            {'m', 2.406},
            {'n', 6.749},
            {'o', 7.507},
            {'p', 1.929},
            {'q', 0.095},
            {'r', 5.987},
            {'s', 6.327},
            {'t', 9.056},
            {'u', 2.758},
            {'v', 0.978},
            {'w', 2.360},
            {'x', 0.150},
            {'y', 1.974},
            {'z', 0.074}
        };

        /// <summary>
        /// Rates text on probability of being legit english text. Lower is better.
        /// </summary>
        /// <param name="text">Text to be rated</param>
        /// <returns>Rating</returns>
        public static double RateText(string text)
        {
            if (String.IsNullOrWhiteSpace(text)) return double.MaxValue;
            double textScore = 0;
            var chars = text.ToLower();
            //var chars = new String(text.ToLower().Where(c => Helpers.smallLetters.Contains(c)).ToArray());
            foreach (char charToBeChecked in ExpectedCharacterPercentages.Keys)
            {
                double expectedPc = ExpectedCharacterPercentages[charToBeChecked];
                double realPc = chars.Count(c => c == charToBeChecked) * 100.0 / chars.Length;
                double charScore = Math.Pow((realPc - expectedPc), 2) / expectedPc;
                textScore += charScore;
            }
            return textScore;
        }
    }
}

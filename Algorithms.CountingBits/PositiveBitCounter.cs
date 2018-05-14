// -----------------------------------------------------------------------
// <copyright file="BitCounter.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace Payvision.CodeChallenge.Algorithms.CountingBits
{
    using System;
    using System.Collections.Generic;

    public class PositiveBitCounter
    {
        private char[] ToBinary(int number) => Convert.ToString(number, 2).ToCharArray();
        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
                throw new ArgumentException(nameof(input));

            var binaryInput = ToBinary(input);
            int binaryInputLength = binaryInput.Length;
            var response = new List<int> { 0 };
            for (int i = binaryInputLength - 1; i >= 0; i--)
            {
                if (binaryInput[i] == '0')
                    continue;

                response[0]++;
                response.Add(binaryInputLength - i - 1);
            }

            return response;
        }
    }
}
// -----------------------------------------------------------------------
// <copyright file="FraudRadar.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection
{
    using System.Collections.Generic;
    using System.IO;

    public class OrderCsvLoader
    {
        public IEnumerable<Order> Load(string filePath)
        {
            var foo = File.ReadAllLines(filePath)
                .Select(OrderFactory.FromCsv);

            return File.ReadAllLines(filePath)
                    .Select(OrderFactory.FromCsv)
                    .Where(l => l.Successful).Select(x => x.Order);

        }
    }
}
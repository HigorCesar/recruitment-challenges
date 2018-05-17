// -----------------------------------------------------------------------
// <copyright file="FraudRadar.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection
{
    using System.Collections.Generic;
    public class FraudRadar
    {
        public IEnumerable<FraudResult> Check(List<Order> orders)
        {
            var fraudResults = new List<FraudResult>();
            foreach (var order in orders)
            {
                if (fraudResults.Exists(f => f.OrderId == order.OrderId))
                    continue;

                var fraudulentOrder = orders.FirstOrDefault(order.IsOtherFraudulent);
                if (fraudulentOrder != null)
                    fraudResults.Add(new FraudResult { IsFraudulent = true, OrderId = fraudulentOrder.OrderId });
            }
            return fraudResults;
        }

        public class FraudResult
        {
            public int OrderId { get; set; }

            public bool IsFraudulent { get; set; }
        }
    }
}
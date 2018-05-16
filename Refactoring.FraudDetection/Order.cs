
using System;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection
{
    public class Email
    {
        private readonly string innerValue;

        public Email(string email)
        {
            var aux = email.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            innerValue = string.Join("@", new[] { aux[0], aux[1] }); ;
        }

        public override string ToString() => innerValue;
    }

    public class Street
    {
        private readonly string innerValue;
        public Street(string value) => innerValue = value.Replace("st.", "street").Replace("rd.", "road");
        public override string ToString() => innerValue;
    }

    public class State
    {
        private readonly string innerValue;
        public State(string value) => innerValue = value.Replace("il", "illinois").Replace("ca", "california").Replace("ny", "new york");
        public override string ToString() => innerValue;
    }
    public class Order
    {
        public int OrderId { get; set; }

        public int DealId { get; set; }

        public Email Email { get; set; }

        public Street Street { get; set; }

        public string City { get; set; }

        public State State { get; set; }

        public string ZipCode { get; set; }

        public string CreditCard { get; set; }
    }

    public class BuildOrderResult
    {
        public bool Successful { get; private set; }
        public Order Order { get; private set; }

        private BuildOrderResult()
        {

        }
        public static BuildOrderResult Success(Order order) => new BuildOrderResult { Order = order, Successful = true };
        public static BuildOrderResult Fail() => new BuildOrderResult { Successful = false };


    }

    public class OrderCsvItem
    {
        public enum Columns
        {
            OrderId = 0,
            DealId = 1,
            Email = 2,
            Street = 3,
            City = 4,
            State = 5,
            ZipCode = 6,
            CreditCard = 7,
        }

        private const int OrderNumberOfFields = 8;
        private readonly string[] items;

        public OrderCsvItem(string line)
        {
            items = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public bool IsValid()
        {
            if (items.Length < OrderNumberOfFields)
                return false;

            if (!int.TryParse(GetValue(Columns.OrderId), out _))
                return false;

            if (!int.TryParse(GetValue(Columns.DealId), out _))
                return false;

            return true;
        }


        public string GetValue(Columns key) => items[(int)key];
    }
    public class OrderFactory
    {


        public static BuildOrderResult FromCsv(string line)
        {
            var orderCsvItem = new OrderCsvItem(line);
            if (orderCsvItem.IsValid())
                return BuildOrderResult.Fail();

            return BuildOrderResult.Success(new Order
            {
                OrderId = Convert.ToInt32(orderCsvItem.GetValue(OrderCsvItem.Columns.OrderId)),
                DealId = Convert.ToInt32(orderCsvItem.GetValue(OrderCsvItem.Columns.DealId)),
                Email = new Email(orderCsvItem.GetValue(OrderCsvItem.Columns.Email)),
                Street = new Street(orderCsvItem.GetValue(OrderCsvItem.Columns.Street)),
                City = orderCsvItem.GetValue(OrderCsvItem.Columns.City),
                State = new State(orderCsvItem.GetValue(OrderCsvItem.Columns.State)),
                ZipCode = orderCsvItem.GetValue(OrderCsvItem.Columns.ZipCode),
                CreditCard = orderCsvItem.GetValue(OrderCsvItem.Columns.CreditCard),
            });

        }
    }
}

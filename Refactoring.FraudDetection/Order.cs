
using System;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection
{
    public class Email : IEquatable<Email>
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
        public override int GetHashCode() => innerValue.GetHashCode();

        public bool Equals(Email other)
        {
            if (other == null) return false;
            return innerValue == other.innerValue;
        }
    }

    public class Street : IEquatable<Street>
    {
        private readonly string innerValue;
        public Street(string value) => innerValue = value.Replace("st.", "street").Replace("rd.", "road");
        public override string ToString() => innerValue;
        public override int GetHashCode() => innerValue.GetHashCode();

        public bool Equals(Street other)
        {
            if (other == null) return false;
            return this.innerValue == other.innerValue;

        }
    }

    public class State : IEquatable<State>
    {
        private readonly string innerValue;
        public State(string value) => innerValue = value.Replace("il", "illinois").Replace("ca", "california").Replace("ny", "new york");
        public override string ToString() => innerValue;

        public override int GetHashCode() => innerValue.GetHashCode();

        public bool Equals(State other)
        {
            if (other == null) return false;
            return innerValue == other.innerValue;
        }
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

        private bool HasSameAddress(Order other)
        {
            //it lacks a better domain definition.eg: these fields are probably required
            // so they should be in ctor or factory
            if (other == null || State == null || City == null || ZipCode == null || Street == null)
                return false;

            return State.Equals(other.State) && City.Equals(other.City) &&
                ZipCode.Equals(other.ZipCode) && Street.Equals(other.Street);
        }


        public bool IsOtherFraudulent(Order other)
        {
            bool SameDealAndEmailButDifferentCreditCard() => DealId == other.DealId && Email.Equals(other.Email) && CreditCard != other.CreditCard;
            bool SameAddressButDifferentCreditCard() => HasSameAddress(other) && CreditCard != other.CreditCard;

            return SameDealAndEmailButDifferentCreditCard() || SameAddressButDifferentCreditCard();
        }
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
            if (!orderCsvItem.IsValid())
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

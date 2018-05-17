
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

        //Could be internal only visible to test assembly
        public bool HasSameAddress(Order other)
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

   

   
}

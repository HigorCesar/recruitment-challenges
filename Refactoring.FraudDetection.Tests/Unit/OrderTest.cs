using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests.Unit
{
    [TestClass]
    public class OrderTest
    {
        private Order orderUnderTest;
        [TestInitialize]
        public void TestSetup()
        {
            orderUnderTest = new Order
            {
                Street = new Street("123 Sesame St."),
                City = "Amsterdam",
                CreditCard = "12345689010",
                DealId = 1,
                Email = new Email("foo@gmail.com"),
                OrderId = 1,
                State = new State("North Holland"),
                ZipCode = "0011"
            };
        }
        [TestMethod]
        public void HasSameAddress_True()
        {
            var anotherOrder = new Order
            {
                Street = new Street("123 Sesame St."),
                City = "Amsterdam",
                CreditCard = "12345689010",
                DealId = 2,
                Email = new Email("bar@gmail.com"),
                OrderId = 2,
                State = new State("North Holland"),
                ZipCode = "0011"
            };
            orderUnderTest.HasSameAddress(anotherOrder).Should().BeTrue();
        }
        [TestMethod]
        public void HasSameAddress_False()
        {
            var anotherOrder = new Order
            {
                Street = new Street("1234 Sesame St."),
                City = "Amsterdam",
                CreditCard = "12345689010",
                DealId = 2,
                Email = new Email("bar@gmail.com"),
                OrderId = 2,
                State = new State("North Holland"),
                ZipCode = "0011"
            };
            orderUnderTest.HasSameAddress(anotherOrder).Should().BeFalse();
        }

        [TestMethod]
        public void IsOtherFraudulent_When_Email_And_Deal_Are_Equals_but_CreditCard_Is_Different_then_True()
        {
            var anotherOrder = new Order
            {
                Street = new Street("1234 Sesame St."),
                City = "Amsterdam",
                CreditCard = "0000",
                DealId = 1,
                Email = new Email("foo@gmail.com"),
                OrderId = 2,
                State = new State("North Holland"),
                ZipCode = "0011"
            };
            orderUnderTest.IsOtherFraudulent(anotherOrder).Should().BeTrue();
        }
        [TestMethod]
        public void IsOtherFraudulent_When_DealId_And_Address_Is_Equals_but_CreditCard_Is_Different_then_True()
        {
            var anotherOrder = new Order
            {
                Street = new Street("123 Sesame St."),
                City = "Amsterdam",
                CreditCard = "0000",
                DealId = 1,
                Email = new Email("foo@gmail.com"),
                OrderId = 2,
                State = new State("North Holland"),
                ZipCode = "0011"
            };
            orderUnderTest.IsOtherFraudulent(anotherOrder).Should().BeTrue();
        }
        [TestClass]
        public class EmailTest
        {
            [TestMethod]
            public void EmailConstructor_RemoveDot()
            {
                var sut = new Email("higor.crr@gmail.com");
                sut.ToString().Should().Be("higorcrr@gmail.com");
            }
            [TestMethod]
            public void EmailConstructor_RemovePlus()
            {
                var sut = new Email("higor+crr@gmail.com");
                sut.ToString().Should().Be("higor@gmail.com");
            }
            [TestMethod]
            public void Email_Equality()
            {
                var sut = new Email("higor.crr@gmail.com");
                var sutCopy = new Email("higor.crr@gmail.com");

                sut.Should().Equals(sutCopy);
            }
        }
    }
}

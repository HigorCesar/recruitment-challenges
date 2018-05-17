using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests.Unit
{
    [TestClass]
    public class OrderTest
    {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests.Unit
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void EmailNormalization()
        {
            //var target = new Order();
            //target.Email = "higor.crr@gmail.com";

            //var aux = target.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            //var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            //aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            //target.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}

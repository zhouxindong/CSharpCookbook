using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpCookbook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpCookbook.Tests
{
    [TestClass()]
    public class NumbericTests
    {
        [TestMethod()]
        public void IsApproximatelyEqualToTest()
        {
            bool approximate = Numberic.IsApproximatelyEqualTo(1, 7, 0.142857, 0.0000001);
            Assert.IsFalse(approximate);
            approximate = Numberic.IsApproximatelyEqualTo(1, 7, 0.1428571, 0.0000001);
            Assert.IsTrue(approximate);
        }
    }
}
using System;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Tests.Helpers;
using NUnit.Framework;

namespace CQRS.Tests
{
    [TestFixture]
    public class MoneyTests
    {
        [Test]
        public void AddTest()
        {
            Money result = new Money(1.11) + new Money(2.22);
            result.ShouldEqual(new Money(3.33));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage="Currency mismatch")]
        public void CannotAddDifferentCurrenciesTest()
        {
            var money = new Money(1.11m, "PLN") + new Money(2.22m, "EUR");
        }

        [Test]
        public void MultiplyTest()
        {
            Money result = new Money(1.11).MultiplyBy(5.0);
            result.ShouldEqual(new Money(5.55));
        }

        [Test, Ignore]
        public void ApiTest()
        {
            Money result = 1.PLN() + 2.PLN();
            result.ShouldEqual(3.PLN());


            Money result2 = 1.3m.PLN() + 2.PLN();
            result2.ShouldEqual(3.3.PLN());
        }
    }
}

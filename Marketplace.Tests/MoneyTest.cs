using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Marketplace.Tests
{
    public class MoneyTest
    {
        [Fact]
        public void Money_object_with_the_same_amount_should_be_equeal()
        {
            var firstAmount = new Money(5);
            var secondAmount = new Money(5);

            Assert.Equal(firstAmount, secondAmount);
        }
    }
}

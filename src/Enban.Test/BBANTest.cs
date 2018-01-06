using System;
using Xunit;

namespace Enban.Test
{
    public class BBANTest
    {
        [Fact]
        public void ToIBAN_GivenNoExplicitCheckDigit_ComputesCorrectCheckDigit()
        {
            var iban = new BBAN("DE", "210501700012345678").ToIBAN();
            Assert.Equal(68, iban.CheckDigit);
        }

        [Fact]
        public void Construct_GivenCorrectFormat_ThenInstanceConstructed()
        {
            var bban = new BBAN("DE", "370400440532013000");
        }

        [Fact]
        public void Construct_GivenWrongFormat_ThenException()
        {
            Assert.Throws<ArgumentException>(() => new BBAN("DE", "X70400440532013000"));
        }
    }
}

using System;
using System.Linq;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Finance;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class FinanceAutoDataTests
    {
        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues(
            AccountName accountName,
            AccountNumber accountNumber,
            Amount amount,
            Bic bic,
            Currency currency,
            Iban iban,
            RoutingNumber routingNumber,
            TransactionType transactionType)
        {
            var values = new object[]
            {
                accountName,
                accountNumber,
                amount,
                bic,
                currency,
                iban,
                routingNumber,
                transactionType,
            };

            var nonBogusValues = values
                .Where(x => x.ToString()!.Length >= 36 && Guid.TryParse(x.ToString()![^36..], out _));

            Assert.IsEmpty(nonBogusValues);
        }
    }
}
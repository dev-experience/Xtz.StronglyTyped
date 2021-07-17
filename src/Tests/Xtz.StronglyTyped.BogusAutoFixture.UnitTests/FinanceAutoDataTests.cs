using NUnit.Framework;
using Xtz.StronglyTyped.BogusAutoFixture.UnitTests.Extensions;
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

            Assert.That(values, Is.All.Matches<object>(x => !x.ToString()!.IsBogusGeneratedValue()));
        }
    }
}
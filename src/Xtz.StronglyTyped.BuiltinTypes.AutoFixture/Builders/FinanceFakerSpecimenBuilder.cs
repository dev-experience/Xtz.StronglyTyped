using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Finance;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    public class FinanceFakerSpecimenBuilder : BaseFakerSpecimenBuilder
    {
        private readonly FinanceFakerBuilder _builder = new();

        protected override Dictionary<Type, Func<IFakerTInternal>> FakerFactories => new()
        {
            { typeof(AccountName), () => _builder.BuildAccountNameFaker() },
            { typeof(AccountNumber), () => _builder.BuildAccountNumberFaker() },
            { typeof(Amount), () => _builder.BuildAmountFaker() },
            { typeof(Bic), () => _builder.BuildBicFaker() },
            { typeof(Currency), () => _builder.BuildCurrencyFaker() },
            { typeof(Iban), () => _builder.BuildIbanFaker() },
            { typeof(RoutingNumber), () => _builder.BuildRoutingNumberFaker() },
            { typeof(TransactionType), () => _builder.BuildTransactionTypeFaker() },
        };
    }
}
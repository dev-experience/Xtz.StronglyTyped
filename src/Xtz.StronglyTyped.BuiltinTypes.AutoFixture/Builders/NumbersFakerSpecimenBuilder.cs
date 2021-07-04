using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Numbers;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    public class NumbersFakerSpecimenBuilder : BaseFakerSpecimenBuilder
    {
        private readonly NumbersFakerBuilder _builder = new();

        protected override Dictionary<Type, Func<IFakerTInternal>> FakerFactories => new()
        {
            { typeof(EvenInt32), () => _builder.BuildEvenInt32Faker() },
            { typeof(EvenInt64), () => _builder.BuildEvenInt64Faker() },
            { typeof(NegativeInt32), () => _builder.BuildNegativeInt32Faker() },
            { typeof(NegativeInt64), () => _builder.BuildNegativeInt64Faker() },
            { typeof(NonNegativeInt32), () => _builder.BuildNonNegativeInt32Faker() },
            { typeof(NonNegativeInt64), () => _builder.BuildNonNegativeInt64Faker() },
            { typeof(NonPositiveInt32), () => _builder.BuildNonPositiveInt32Faker() },
            { typeof(NonPositiveInt64), () => _builder.BuildNonPositiveInt64Faker() },
            { typeof(OddInt32), () => _builder.BuildOddInt32Faker() },
            { typeof(OddInt64), () => _builder.BuildOddInt64Faker() },
            { typeof(PositiveInt32), () => _builder.BuildPositiveInt32Faker() },
            { typeof(PositiveInt64), () => _builder.BuildPositiveInt64Faker() },
        };
    }
}
using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Name;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    public class NameFakerSpecimenBuilder : BaseFakerSpecimenBuilder
    {
        private readonly NameFakerBuilder _builder = new(true);

        protected override Dictionary<Type, Func<IFakerTInternal>> FakerFactories => new()
        {
            { typeof(FirstName), () => _builder.BuildFirstNameFaker() },
            { typeof(FullName), () => _builder.BuildFullNameFaker() },
            { typeof(LastName), () => _builder.BuildLastNameFaker() },
            { typeof(NamePrefix), () => _builder.BuildNamePrefixFaker() },
            { typeof(NameSuffix), () => _builder.BuildNameSuffixFaker() },
        };
    }
}
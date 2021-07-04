using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Phone;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    public class PhoneFakerSpecimenBuilder : BaseFakerSpecimenBuilder
    {
        private readonly PhoneFakerBuilder _builder = new(true);

        protected override Dictionary<Type, Func<IFakerTInternal>> FakerFactories => new()
        {
            { typeof(PhoneNumber), () => _builder.BuildPhoneNumberFaker() },
        };
    }
}
using System;
using AutoFixture;
using AutoFixture.NUnit3;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StrongAutoDataAttribute : AutoDataAttribute
    {
        public StrongAutoDataAttribute()
            : base(CreateFixture)
        {
        }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new AddressFakerSpecimenBuilder());
            fixture.Customizations.Add(new CommerceFakerSpecimenBuilder());
            fixture.Customizations.Add(new CompanyFakerSpecimenBuilder());
            fixture.Customizations.Add(new FinanceFakerSpecimenBuilder());
            fixture.Customizations.Add(new IdFakerSpecimenBuilder());
            fixture.Customizations.Add(new InternetFakerSpecimenBuilder());
            fixture.Customizations.Add(new NameFakerSpecimenBuilder());
            fixture.Customizations.Add(new NumbersFakerSpecimenBuilder());
            fixture.Customizations.Add(new PhoneFakerSpecimenBuilder());
            fixture.Customizations.Add(new VehicleFakerSpecimenBuilder());
            return fixture;
        }
    }
}

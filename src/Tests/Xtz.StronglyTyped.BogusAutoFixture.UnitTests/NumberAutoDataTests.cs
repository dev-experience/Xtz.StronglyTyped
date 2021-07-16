using System;
using System.Linq;
using NUnit.Framework;
using Xtz.StronglyTyped.BogusAutoFixture.UnitTests.Extensions;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Numbers;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class NumberAutoDataTests
    {
        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues(
            EvenInt32 evenInt32,
            EvenInt64 evenInt64,
            NegativeInt32 negativeInt32,
            NegativeInt64 negativeInt64,
            NonNegativeInt32 nonNegativeInt32,
            NonNegativeInt64 nonNegativeInt64,
            NonPositiveInt32 nonPositiveInt32,
            NonPositiveInt64 nonPositiveInt64,
            OddInt32 oddInt32,
            OddInt64 oddInt64,
            PositiveInt32 positiveInt32,
            PositiveInt64 positiveInt64)
        {
            var values = new object[]
            {
                evenInt32,
                evenInt64,
                negativeInt32,
                negativeInt64,
                nonNegativeInt32,
                nonNegativeInt64,
                nonPositiveInt32,
                nonPositiveInt64,
                oddInt32,
                oddInt64,
                positiveInt32,
                positiveInt64,
            };

            Assert.That(values, Is.All.Matches<object>(x => !x.ToString().IsBogusGeneratedValue()));
        }
    }
}
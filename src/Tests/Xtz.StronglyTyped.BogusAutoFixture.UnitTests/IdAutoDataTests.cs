using System;
using AutoFixture.NUnit3;
using NUnit.Framework;
using UnitTests;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class IdAutoDataTests
    {
        [Test]
        [AutoData]
        public void ShouldGenerateStronglyTypedValues(
            EmployeeGuidId employeeGuidId,
            EmployeeIntId employeeIntId)
        {
            Assert.That(employeeGuidId.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(employeeIntId.Value, Is.Not.EqualTo(default(int)));
        }

        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues_WithCustomSpecimen(
            EmployeeGuidId employeeGuidId,
            EmployeeIntId employeeIntId)
        {
            Assert.That(employeeGuidId.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(employeeIntId.Value, Is.Not.EqualTo(default(int)));
        }
    }
}
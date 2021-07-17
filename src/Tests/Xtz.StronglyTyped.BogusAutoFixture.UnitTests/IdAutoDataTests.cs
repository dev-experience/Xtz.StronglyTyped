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
            Assert.AreNotEqual(default(Guid), employeeGuidId.Value);
            Assert.AreNotEqual(default(int), employeeIntId.Value);
        }

        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues_WithCustomSpecimen(
            EmployeeGuidId employeeGuidId,
            EmployeeIntId employeeIntId)
        {
            Assert.AreNotEqual(default(Guid), employeeGuidId.Value);
            Assert.AreNotEqual(default(int), employeeIntId.Value);
        }
    }
}
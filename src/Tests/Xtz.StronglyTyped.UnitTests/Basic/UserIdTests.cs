using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.UnitTests.Basic
{
    public class UserIdTests
    {
        [Test]
        public void UserId_ShouldInstantiate_GivenGuid()
        {
            //// Arrange

            var value = Guid.NewGuid();

            //// Act

            var result = new UserId(value);

            //// Assert

            Assert.AreNotEqual(Guid.Empty, result);
        }

        [Test]
        public void UserId_ShouldThrow_GivenInvalidValue()
        {
            //// Arrange

            var value = Guid.Empty;

            //// Act

            [ExcludeFromCodeCoverage]
            // ReSharper disable once ObjectCreationAsStatement
            void Action() => new UserId(value);

            //// Assert

            Assert.Throws<InvalidValueException>(Action);
        }
    }
}
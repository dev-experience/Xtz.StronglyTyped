using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AutoFixture.Kernel;
using Bogus;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture
{
    public abstract class BaseFakerSpecimenBuilder : ISpecimenBuilder
    {
        private NoSpecimen NoSpecimen { get; } = new();

        protected abstract Dictionary<Type, Func<IFakerTInternal>> FakerFactories { get; }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is not ParameterInfo parameterInfo)
            {
                return NoSpecimen;
            }

            var parameterType = parameterInfo.ParameterType;

            var fakerObject = GetFaker(parameterType);
            if (fakerObject == null) return NoSpecimen;

            dynamic faker = fakerObject;

            try
            {
                var result = faker.Generate();
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception '{e.GetType()}': {e.Message}");
                return NoSpecimen;
            }
        }

        private IFakerTInternal? GetFaker(Type resultType)
        {
            if (FakerFactories.TryGetValue(resultType, out var fakerFunc))
            {
                return fakerFunc();
            }

            return null;
        }
    }
}
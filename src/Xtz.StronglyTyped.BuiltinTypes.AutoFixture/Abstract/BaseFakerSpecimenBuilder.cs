using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using Bogus;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture
{
    public abstract class BaseFakerSpecimenBuilder : ISpecimenBuilder
    {
        private static readonly int DEFAULT_REPEAT_COUNT = 3;
        private NoSpecimen NoSpecimen { get; } = new();

        protected abstract Dictionary<Type, Func<IFakerTInternal>> FakerFactories { get; }

        // KLUDGE: Don't be surprised if you run single test without AutoData but it creates test values for other tests (which you are not trying to run at the moment)
        public object Create(object request, ISpecimenContext context)
        {
            if (request is not ParameterInfo parameterInfo)
            {
                return request switch
                {
                    MultipleRequest {Request: SeededRequest {Request: Type multipleType}} =>
                        CreateArray(multipleType, context),
                    SeededRequest {Request: Type type}
                        when type.TryGetSingleGenericTypeArgument(typeof(IEnumerable<>), out var enumerableType) =>
                            CreateEnumerable(enumerableType, context),
                    _ => NoSpecimen
                };
            }

            var parameterType = parameterInfo.ParameterType;

            return Create(parameterType);
        }

        private object CreateEnumerable(Type? enumerableType, ISpecimenContext context)
        {
            if (enumerableType is null) return NoSpecimen;

            var specimen = context.Resolve(new MultipleRequest(new SeededRequest(enumerableType, null)));
            if (specimen is OmitSpecimen) return specimen;

            var typedAdapterType = typeof(ConvertedEnumerable<>).MakeGenericType(enumerableType);
            return Activator.CreateInstance(typedAdapterType, specimen);
        }

        private object CreateArray(Type multipleType, ISpecimenContext context)
        {
            var firstElement = Create(multipleType);
            if (firstElement is NoSpecimen) return firstElement;

            var count = DEFAULT_REPEAT_COUNT;
            if (context is SpecimenContext {Builder: Fixture fixture})
            {
                count = fixture.RepeatCount;
                if (count < 1)
                {
                    count = DEFAULT_REPEAT_COUNT;
                }
            }

            var result = new object[count];
            result[0] = firstElement;
            for (var i = 1; i < count; i++)
            {
                result[i] = Create(multipleType);
            }

            return result;
        }

        private object Create(Type? parameterType)
        {
            if (parameterType is null) return NoSpecimen;

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
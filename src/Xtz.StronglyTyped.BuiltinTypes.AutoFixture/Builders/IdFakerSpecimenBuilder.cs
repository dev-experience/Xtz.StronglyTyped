using System;
using System.Diagnostics;
using System.Reflection;
using AutoFixture.Kernel;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Ids;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    public class IdFakerSpecimenBuilder : ISpecimenBuilder
    {
        private NoSpecimen NoSpecimen { get; } = new();

        private readonly IdFakerBuilder _builder = new();

        private readonly MethodInfo _buildGuidIdFakerMethod;

        private readonly MethodInfo _buildIntIdFakerMethod;

        public IdFakerSpecimenBuilder()
        {
            _buildGuidIdFakerMethod = GetType().GetMethod(nameof(GetGuidIdFaker), BindingFlags.Instance | BindingFlags.NonPublic)!;
            _buildIntIdFakerMethod = GetType().GetMethod(nameof(GetIntIdFaker), BindingFlags.Instance | BindingFlags.NonPublic)!;
        }

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

        private IFakerTInternal? GetFaker(Type parameterType)
        {
            if (typeof(GuidId).IsAssignableFrom(parameterType))
            {
                var method = _buildGuidIdFakerMethod.MakeGenericMethod(parameterType);
                var result = method.Invoke(this, null) as IFakerTInternal;
                return result;
            }

            if (typeof(IntId).IsAssignableFrom(parameterType))
            {
                var method = _buildIntIdFakerMethod.MakeGenericMethod(parameterType);
                var result = method.Invoke(this, null) as IFakerTInternal;
                return result;
            }

            return null;
        }

        private Faker<TGuidId> GetGuidIdFaker<TGuidId>()
            where TGuidId : GuidId
        {
            var faker = _builder.BuildGuidIdFaker<TGuidId>();
            return faker;
        }

        private Faker<TIntId> GetIntIdFaker<TIntId>()
            where TIntId : IntId
        {
            var faker = _builder.BuildIntIdFaker<TIntId>();
            return faker;
        }
    }
}
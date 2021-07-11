using System;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Ids;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class IdFakerBuilder : BaseFakerBuilder
    {
        public IdFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
        }

        /// <summary>
        /// A random GUID-based ID faker.
        /// </summary>
        public Faker<TGuidId> BuildGuidIdFaker<TGuidId>()
            where TGuidId : GuidId
        {
            var result = GetFaker(() => new Faker<TGuidId>()
                .CustomInstantiator(_ => (TGuidId)Activator.CreateInstance(typeof(TGuidId), Guid.NewGuid())));
            return result;
        }

        /// <summary>
        /// A random integer-based ID faker.
        /// </summary>
        public Faker<TIntId> BuildIntIdFaker<TIntId>()
            where TIntId : IntId
        {
            var result = GetFaker(() => new Faker<TIntId>()
                .CustomInstantiator(f => (TIntId)Activator.CreateInstance(typeof(TIntId), f.Random.Int(1, int.MaxValue))));
            return result;
        }
    }
}
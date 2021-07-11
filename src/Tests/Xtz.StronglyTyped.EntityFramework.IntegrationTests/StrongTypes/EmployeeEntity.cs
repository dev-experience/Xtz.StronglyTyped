using System.ComponentModel.DataAnnotations;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes
{
    public class EmployeeEntity
    {
        [Key]
        public EmployeeStructGuidId StructGuidId { get; set; }

        public Email Email { get; set; }

        public AvatarUri AvatarUri { get; set; }

        public IpV4Address IpV4Address { get; set; }

        public MacAddress MacAddress { get; set; }
    }
}
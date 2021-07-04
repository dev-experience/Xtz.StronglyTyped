using System.ComponentModel.DataAnnotations;

namespace Xtz.StronglyTyped.IntegrationTests.Settings
{
    public class RequiredTestSettings
    {
        [Required]
        public Country Country { get; set; }
    }
}

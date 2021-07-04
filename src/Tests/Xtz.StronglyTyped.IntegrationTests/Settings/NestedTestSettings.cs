namespace Xtz.StronglyTyped.IntegrationTests.Settings
{
    public class NestedTestSettings
    {
        public InnerSettings Inner { get; set; }

        public class InnerSettings
        {
            public Country Country { get; set; }
        }
    }
}

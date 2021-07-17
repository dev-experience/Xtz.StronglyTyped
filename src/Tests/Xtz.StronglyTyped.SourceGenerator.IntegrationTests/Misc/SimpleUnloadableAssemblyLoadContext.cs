using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Xtz.StronglyTyped.SourceGenerator.IntegrationTests
{
    internal sealed class SimpleUnloadableAssemblyLoadContext : AssemblyLoadContext, IDisposable
    {
        public SimpleUnloadableAssemblyLoadContext()
            : base(true)
        {
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }

        public void Dispose()
        {
            Unload();
        }
    }
}
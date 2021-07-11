using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Xtz.StronglyTyped.Swashbuckle
{
    public class RemoveRedundantTypesFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            context.SchemaRepository.Schemas.Remove(nameof(MailAddress));
            context.SchemaRepository.Schemas.Remove(nameof(IPAddress));
            context.SchemaRepository.Schemas.Remove(nameof(PhysicalAddress));
            context.SchemaRepository.Schemas.Remove(nameof(AddressFamily));
        }
    }
}
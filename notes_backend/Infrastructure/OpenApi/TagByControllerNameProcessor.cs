using System;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace NotesBackend.Infrastructure.OpenApi
{
    /// <summary>
    /// Operation processor that sets the OpenAPI operation tag based on the controller name.
    /// Compatible with NSwag 14.x APIs.
    /// </summary>
    public sealed class TagByControllerNameProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            var declaringType = context.MethodInfo?.DeclaringType;
            if (declaringType != null)
            {
                var name = declaringType.Name;
                if (name.EndsWith("Controller", StringComparison.Ordinal))
                {
                    name = name[..^"Controller".Length];
                }
                context.OperationDescription.Operation.Tags.Clear();
                context.OperationDescription.Operation.Tags.Add(name);
            }
            return true;
        }
    }
}

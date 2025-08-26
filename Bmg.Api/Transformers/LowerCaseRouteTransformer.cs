namespace Bmg.Api.Transformers;

public class LowerCaseRouteTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value) => value?.ToString()?.ToLowerInvariant();
}

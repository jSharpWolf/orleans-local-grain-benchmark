using Orleans;

namespace OrleansMultiSilo.GrainInterfaces;

[GenerateSerializer]
[Alias("_call")]
public record Call
{
    [Id(0)]
    public required int Depth { get; init; }
}
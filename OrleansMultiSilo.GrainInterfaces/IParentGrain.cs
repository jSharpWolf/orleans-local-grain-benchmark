using Orleans;

namespace OrleansMultiSilo.GrainInterfaces;

public interface ICallPerformer
{
    Task PerformCall(Call call);
}

public interface IParentGrain : IGrainWithGuidKey, ICallPerformer;
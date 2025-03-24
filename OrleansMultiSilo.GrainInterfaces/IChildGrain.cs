using Orleans;

namespace OrleansMultiSilo.GrainInterfaces;

public interface IChildGrain : IGrainWithGuidCompoundKey, ICallPerformer;
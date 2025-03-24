using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using OrleansMultiSilo.GrainInterfaces.ImplTypes;
using OrleansMultiSilo.Silo.Grains.BaseImplementation;

namespace OrleansMultiSilo.Silo.Grains.Implementations;

public class RandomParent(ILogger<RandomParent> logger) : ParentGrain<IStatelessChild>(logger), IRandomParent;

[StatelessWorker(maxLocalWorkers: 1)]
public class StatelessChild : ChildGrain<IStatelessChild>, IStatelessChild;
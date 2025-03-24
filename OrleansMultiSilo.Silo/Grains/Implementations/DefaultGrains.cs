using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using OrleansMultiSilo.GrainInterfaces.ImplTypes;
using OrleansMultiSilo.Silo.Grains.BaseImplementation;

namespace OrleansMultiSilo.Silo.Grains.Implementations;

// Default implementation: Uses random placement
public class DefaultParent(ILogger<DefaultParent> logger) : ParentGrain<IDefaultChild>(logger), IDefaultParent;
public class DefaultChild : ChildGrain<IDefaultChild>, IDefaultChild;
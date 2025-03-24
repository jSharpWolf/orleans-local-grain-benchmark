using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Placement;
using OrleansMultiSilo.GrainInterfaces.ImplTypes;
using OrleansMultiSilo.Silo.Grains.BaseImplementation;

namespace OrleansMultiSilo.Silo.Grains.Implementations;

// Use random placement for parent, and "PreferLocalPlacement" for child
public class PreferLocalParent(ILogger<PreferLocalParent> logger) : ParentGrain<IPreferLocalChild>(logger), IPreferLocalParent;

[PreferLocalPlacement]
public class PreferLocalChild : ChildGrain<IPreferLocalChild>, IPreferLocalChild;
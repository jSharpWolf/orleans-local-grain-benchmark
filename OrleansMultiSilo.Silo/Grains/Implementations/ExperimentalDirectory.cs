using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.GrainDirectory;
using Orleans.Placement;
using OrleansMultiSilo.GrainInterfaces.ImplTypes;
using OrleansMultiSilo.Silo.Grains.BaseImplementation;

namespace OrleansMultiSilo.Silo.Grains.Implementations;

// Use experimental directory for parent and child grains
[GrainDirectory("experimental")]
public class ExperimentalDirectoryParent(ILogger<ExperimentalDirectoryParent> logger) : ParentGrain<IExperimentalDirectoryChild>(logger), IExperimentalDirectoryParent;

[GrainDirectory("experimental")]
public class ExperimentalDirectoryChild : ChildGrain<IExperimentalDirectoryChild>, IExperimentalDirectoryChild;
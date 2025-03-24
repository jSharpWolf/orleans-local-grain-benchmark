using Microsoft.Extensions.Logging;
using Orleans.GrainDirectory;
using OrleansMultiSilo.GrainInterfaces.ImplTypes;
using OrleansMultiSilo.Silo.Grains.BaseImplementation;

namespace OrleansMultiSilo.Silo.Grains.Implementations;

public class RandomParentForNaiveChild(ILogger<IRandomParentForNaiveChild> logger) : ParentGrain<INaiveChild>(logger), IRandomParentForNaiveChild;

[GrainDirectory(GrainDirectoryName = nameof(NaiveGrainDirectory))]
public class NaiveChild : ChildGrain<INaiveChild>, INaiveChild;
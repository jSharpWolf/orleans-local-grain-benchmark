using OrleansMultiSilo.GrainInterfaces;

namespace OrleansMultiSilo.Silo.Grains.BaseImplementation;

/// <summary>
/// Base implementation of child, will redirect call to other children until depth = 1
/// </summary>
public abstract class ChildGrain<T> : Grain, IChildGrain where T : IChildGrain
{
    public async Task PerformCall(Call call)
    {
        if (call.Depth > 1)
        {
            var newCall = new Call { Depth = call.Depth - 1 };
            var childGrain = GrainFactory.GetGrain<T>(this.GetPrimaryKey(), newCall.Depth.ToString());
            await childGrain.PerformCall(new Call { Depth = call.Depth - 1 });
        }
    }
}
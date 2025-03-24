using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrleansMultiSilo.GrainInterfaces;
using OrleansMultiSilo.GrainInterfaces.ImplTypes;

namespace OrleansMultiSilo.Api;

public class RestApi(WebApplication host, IClusterClient client)
{
    private readonly ILogger _logger = host.Services.GetRequiredService<ILogger<RestApi>>();

    public void Map()
    {
        MapTestEndpoint();
        MapBenchmarkEndpoint();
    }

    private async Task<object> Benchmark(int depth, int? count, int? duration, ModeSwitch mode)
    {
        var sw = Stopwatch.StartNew();
        if (duration != null)
        {
            count = int.MaxValue;
        }
        else if (count != null)
        {
            duration = int.MaxValue;
        }

        var num = 0;
        for (num = 0; num < count && sw.Elapsed.TotalSeconds < duration; ++num)
        {
            var call = new Call { Depth = depth };
            _logger.LogTrace("Begin request with {depth}", call.Depth);
            var id = Guid.NewGuid();
            IParentGrain parentGrain;
            switch (mode)
            {
                case ModeSwitch.Default:
                    parentGrain = client.GetGrain<IDefaultParent>(id);
                    break;
                case ModeSwitch.HashBasedParentStatelessChild:
                    parentGrain = client.GetGrain<IRandomParent>(id);
                    break;
                case ModeSwitch.HashBasedParentNaiveChild:
                    parentGrain = client.GetGrain<IRandomParentForNaiveChild>(id);
                    break;
                case ModeSwitch.Experimental:
                    parentGrain = client.GetGrain<IExperimentalDirectoryParent>(id);
                    break;
                case ModeSwitch.PreferLocal:
                    parentGrain = client.GetGrain<IPreferLocalParent>(id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
            await parentGrain.PerformCall(call);
        }
        sw.Stop();
        var perSec = num / sw.Elapsed.TotalSeconds;
        var avgDuration = sw.Elapsed.TotalMilliseconds / num;
        _logger.LogInformation("Executed test request with {depth} inner calls for {count} times in {time}ms", depth, count, sw.ElapsedMilliseconds);
        return new
        {
            TotalCount = num,
            TotalDuration = sw.ElapsedMilliseconds,
            InstancesPerSecond = perSec,
            AverageDuration = avgDuration,
            Mode = mode.ToString()
        };
    }

    private void MapBenchmarkEndpoint()
    {
        host.MapGet("/bench", async ([FromQuery] int depth, ILogger<Program> logger, [FromQuery]int? count = 1, [FromQuery]int? duration = null, [FromQuery]string? mode = null) =>
        {
            var modeSwitch = ModeSwitch.Default;
            if (mode != null)
            {
                if (int.TryParse(mode, out var intMode))
                {
                    modeSwitch = (ModeSwitch)intMode;
                }

                if (Enum.TryParse(typeof(ModeSwitch), mode, true, out var res) && res is ModeSwitch enumMode)
                {
                    modeSwitch = enumMode;
                }
            }

            return await Benchmark(depth, count, duration, modeSwitch);
        });
    }

    private void MapTestEndpoint()
    {
        host.MapGet("/test", async (ILogger<Program> logger) =>
        {
            var sw = Stopwatch.StartNew();
            var call = new Call { Depth = Random.Shared.Next(0, 3) };
            logger.LogTrace("Begin request with {depth}", call.Depth);
            var grain = client.GetGrain<IParentGrain>(Guid.NewGuid());
            await grain.PerformCall(call);
            sw.Stop();
            logger.LogDebug("Executed test request with {depth} in {time}ms", call.Depth, sw.ElapsedMilliseconds);
        });
    }
}
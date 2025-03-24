using System.Runtime.InteropServices;
using Orleans.GrainDirectory;

namespace OrleansMultiSilo.Silo;

public class NaiveGrainDirectory : IGrainDirectory
{
    private Dictionary<GrainId, GrainAddress> _dictionary = new();
    private Lock _lock = new();
    public Task<GrainAddress?> Register(GrainAddress address)
    {
        lock (_lock)
        {
            _dictionary[address.GrainId] = address;
        }

        return Task.FromResult(address)!;
    }

    public Task Unregister(GrainAddress address)
    {
        lock (_lock)
        {
            _dictionary.Remove(address.GrainId);
        }

        return Task.CompletedTask;
    }

    public Task<GrainAddress?> Lookup(GrainId grainId)
    {
        lock (_lock)
        {
            if (_dictionary.TryGetValue(grainId, out var addr))
            {
                return Task.FromResult(addr)!;
            }
        }

        return Task.FromResult<GrainAddress?>(null);
    }

    public Task UnregisterSilos(List<SiloAddress> siloAddresses)
    {
        lock (_lock)
        {
            var saHash = siloAddresses.ToHashSet();
            var todelete = new List<GrainId>();
            foreach (var g in _dictionary.Values)
            {
                if (g.SiloAddress != null && saHash.Contains(g.SiloAddress))
                {
                    todelete.Add(g.GrainId);
                }
            }

            foreach (var td in todelete)
            {
                _dictionary.Remove(td);
            }
        }

        return Task.CompletedTask;
    }
}
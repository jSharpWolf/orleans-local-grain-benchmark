using System;
using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime.Hosting;
using OrleansMultiSilo.Silo;

var primarySiloPort = 35700;
var builder = Host.CreateDefaultBuilder(args)
    .UseOrleans(silo =>
    {
        var port = silo.Configuration.GetValue<int?>("Silo:Port") ?? primarySiloPort;
        var primaryEndpoint = port != primarySiloPort ? new IPEndPoint(IPAddress.Loopback, primarySiloPort) : null;
        var pid = Process.GetCurrentProcess().Id;
        Console.WriteLine($"Process id {pid} - Starting Silo with {port}. Primary silo port: {primarySiloPort}. Is primary: {primaryEndpoint == null}");

        silo.AddGrainDirectory(nameof(NaiveGrainDirectory), (sp, s) => new NaiveGrainDirectory());
#pragma warning disable ORLEANSEXP003
        silo.AddDistributedGrainDirectory("experimental");
#pragma warning restore ORLEANSEXP003
        silo.Configure<GrainCollectionOptions>(opt =>
        {
            opt.CollectionAge = TimeSpan.FromSeconds(30);
            opt.CollectionQuantum = TimeSpan.FromSeconds(15);
        });
        silo.UseLocalhostClustering(siloPort: port, primarySiloEndpoint: primaryEndpoint)
            .ConfigureLogging(logging => logging
                .AddSimpleConsole(conf =>
                {
                    conf.TimestampFormat = "HH:mm:ss.fff ";
                    conf.SingleLine = true;
                }));
    })
    .UseConsoleLifetime();

using IHost host = builder.Build();

await host.RunAsync();
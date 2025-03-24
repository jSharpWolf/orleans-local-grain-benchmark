using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrleansMultiSilo.Api;
using OrleansMultiSilo.GrainInterfaces;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(35550);
});

var t = builder.Host.UseOrleansClient(client =>
    {
        client.UseLocalhostClustering();
    })
    .ConfigureLogging(logging => logging
        .AddSimpleConsole(conf =>
        {
            conf.TimestampFormat = "HH:mm:ss.fff ";
            conf.SingleLine = true;
        }))
    .UseConsoleLifetime();

var host = builder.Build();
var client = host.Services.GetRequiredService<IClusterClient>();
var api = new RestApi(host, client);
api.Map();
await host.RunAsync();
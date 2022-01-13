using Sentry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Google.Cloud.Functions.Hosting;

[assembly: FunctionsStartup(typeof(SentryStartup))] // Configured via env var on GCP

public class Function : IHttpFunction
{
    private readonly HttpClient _client = new(new SentryHttpMessageHandler());

    public async Task HandleAsync(HttpContext context)
    {
        var stream = await _client.GetStreamAsync("https://live.sentry.io/stream");
        using var reader = new StreamReader(stream);
        var aggregates = new HashSet<object>();
        for (var i = 0; i < 1000; i++)
        {
            var line = await reader.ReadLineAsync();
            if (!line!.StartsWith("data: ")) continue;
            var o = JsonSerializer.Deserialize<object[]>(line[6..]);
            var bug = (lat: double.Parse(o[0].ToString()), lon: double.Parse(o[1].ToString()), platform: o[3].ToString());
            var length = Math.Sqrt(bug.lat * bug.lat + bug.lon * bug.lon);
            bug.lat /= length; bug.lon /= length;
            aggregates.Add(new { lat=Math.Round(bug.lat, 2), lon=Math.Round(bug.lon, 2), bug.platform});
        }

        await context.Response.WriteAsync(JsonSerializer.Serialize(new { bugs = aggregates }));
    }
}

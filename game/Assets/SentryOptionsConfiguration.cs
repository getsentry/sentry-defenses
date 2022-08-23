using System.Collections.Concurrent;
using System.Timers;
using Sentry;
using Sentry.Extensibility;
using Sentry.Unity;
using UnityEngine;

[CreateAssetMenu(fileName = "Assets/Resources/Sentry/SentryOptionsConfiguration.cs", menuName = "Sentry/SentryOptionsConfiguration", order = 999)]
public class SentryOptionsConfiguration : ScriptableOptionsConfiguration
{
    private readonly ConcurrentQueue<Breadcrumb> _breadcrumbs = new ConcurrentQueue<Breadcrumb>();
    private Timer _timer;
    
    public override void Configure(SentryUnityOptions options)
    {
        _timer = new Timer(5000);
        _timer.Elapsed += (sender, args) =>
        {
            options.DiagnosticLogger?.LogDebug("Timer for crumb collection triggered");
            if (!_breadcrumbs.IsEmpty)
            {
                Collect(options.DiagnosticLogger);
            }
        };
        _timer.Start();

        options.BeforeBreadcrumb = breadcrumb =>
        {
            if (_breadcrumbs.Count > 1000)
            {
                options.DiagnosticLogger?.LogDebug("1k crumbs, collecting");
                Collect(options.DiagnosticLogger);
            }
            if (_breadcrumbs.Count > 1000)
            {
                options.DiagnosticLogger?.LogDebug("Still has over 1k crumbs, dropping");
                // Backpressure mechanism.
                // The itself SDK (handling this return) has a ring buffer so lets give him the crumb
                return breadcrumb;
            }
            _breadcrumbs.Enqueue(breadcrumb);
            return breadcrumb;
        };
    }

    private void Collect(IDiagnosticLogger logger)
    {
        var session = SentrySdk.CurrentSession;
        if (session is null)
        {
            logger?.LogInfo("Not capturing crumbs because there's no session");
            return;
        }
        var evt = new SentryEvent();

        while ((!_breadcrumbs.IsEmpty || evt.Breadcrumbs.Count >= 1000) 
               && _breadcrumbs.TryDequeue(out var crumb))
        {
            evt.AddBreadcrumb(crumb);
        }

        evt.SetTag("did", session.DistinctId);
        evt.SetTag("sid", session.Id.ToString());
        evt.Message = "Breadcrumb Event";

        logger?.LogInfo("Capturing breadcrumb event");
        SentrySdk.CaptureEvent(evt);
    }
}
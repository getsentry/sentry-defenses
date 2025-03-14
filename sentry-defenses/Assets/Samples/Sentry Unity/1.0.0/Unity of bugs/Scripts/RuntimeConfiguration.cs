using UnityEngine;
using Sentry.Unity;

[CreateAssetMenu(fileName = "Assets/Resources/Sentry/RuntimeConfiguration.asset", menuName = "Sentry/RuntimeConfiguration", order = 999)]
public class RuntimeConfiguration : Sentry.Unity.SentryRuntimeOptionsConfiguration
{
    /// Called at the player startup by SentryInitialization.
    /// You can alter configuration for the C# error handling and also
    /// native error handling in platforms **other** than iOS, macOS and Android.
    /// Learn more at https://docs.sentry.io/platforms/unity/configuration/options/#programmatic-configuration
    public override void Configure(SentryUnityOptions options)
    {
        // Note that changes to the options here will **not** affect iOS, macOS and Android events. (i.e. environment and release)
        // Take a look at `SentryBuildTimeOptionsConfiguration` instead.

        Debug.Log(nameof(RuntimeConfiguration) + "::Configure() called");

        // BeforeSend is only relevant at runtime. It wouldn't hurt to be set at build time, just wouldn't do anything.
        options.SetBeforeSend((sentryEvent, hint) =>
        {
            if (sentryEvent.Tags.ContainsKey("SomeTag"))
            {
                // Don't send events with a tag SomeTag to Sentry
                return null;
            }

            return sentryEvent;
        });

        Debug.Log(nameof(RuntimeConfiguration) + "::Configure() finished");
    }
}

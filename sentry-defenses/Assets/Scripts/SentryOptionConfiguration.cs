using Sentry.Unity;

public class SentryOptionConfiguration : SentryOptionsConfiguration
{
    public override void Configure(SentryUnityOptions options)
    {
         options.AddInAppIncludeRegex(".*SentryTower.*"); // Sentry marks things started with 'Sentry' as InApp=false
        options.SetBeforeBreadcrumb((breadcrumb, hint) =>
        {
            if (breadcrumb.Category == "http")
            {
                return null;
            }

            return breadcrumb;
        });
    }
}
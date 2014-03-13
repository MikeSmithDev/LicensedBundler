LicensedBundler
===============

A custom implementation of bundling and minification in ASP.NET that preserves important comments, as in `*/! Comments /*`, for licensing (or any other) purposes).

**Implementation**


[Get the package from NuGet](https://www.nuget.org/packages/LicensedBundler/)

Instead of using `ScriptBundle` or `StyleBundle` in your `BundleConfig.cs`, use `LicensedScriptBundle` or `LicensedStyleBundle`.

<pre>
public static void RegisterBundles(BundleCollection bundles)
{
    bundles.Add(new LicensedScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.unobtrusive*",
                  "~/Scripts/jquery.validate*"));
                  
    bundles.Add(new LicensedStyleBundle("~/Content/css").Include(
                  "~/Content/site.css",
                  "~/Content/site2.css",
                  "~/Content/site3.css"));
}
</pre>

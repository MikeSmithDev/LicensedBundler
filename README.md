LicensedBundler
===============

A custom implementation of bundling and minification in ASP.NET that preserves important comments, as in `*/! Comments /*`, for licensing (or any other) purposes).

This also protects against changes in user-agent which can cause your production bundles to be returned with *all* comments and unminified, as covered in [Changing User-Agent to Eureka/1 Changes Output in ASP.NET Bundling and Minification](http://mikesmithdev.com/blog/aspnet-bundling-changes-output-with-user-agent-eureka-1/).

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

So your css:
<pre>
/*! 
    This will be visible in bundled and minified version.
    It's important!
*/

/* other comments you don't want people to see. */

html {
    background-color: #000;
    margin: 0;
    padding: 0;
}

body {
    background-color: #fff;
    border-top: solid 10px #000;
    color: #333;
    font-size: .85em;
    font-family: "Segoe UI", Verdana, Helvetica, Sans-Serif;
    margin: 0;
    padding: 0;
}
</pre>

becomes this:

<pre>
/*! 
    This will be visible in bundled and minified version.
    It's important!
*/
html{background-color:#000;margin:0;padding:0}body{background-color:#fff;border-top:solid 10px #000;color:#333;font-size:.85em;font-family:"Segoe UI",Verdana,Helvetica,Sans-Serif;margin:0;padding:0}
</pre>

using Microsoft.Extensions.Primitives;

namespace TLD14.Composition;

public sealed class SecurityHeadersMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context)
    {
        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
        context.Response.Headers.Append(
            "referrer-policy",
            new StringValues("strict-origin-when-cross-origin"));

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
        context.Response.Headers.Append(
            "x-content-type-options",
            new StringValues("nosniff"));

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
        context.Response.Headers.Append(
            "x-frame-options",
            new StringValues("DENY"));

        // https://security.stackexchange.com/questions/166024/does-the-x-permitted-cross-domain-policies-header-have-any-benefit-for-my-websit
        context.Response.Headers.Append(
            "X-Permitted-Cross-Domain-Policies",
            new StringValues("none"));

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
        context.Response.Headers.Append(
            "x-xss-protection",
            new StringValues("0; mode=block"));

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Expect-CT
        // You can use https://report-uri.com/ to get notified when a misissued certificate is detected
        context.Response.Headers.Append(
            "Expect-CT",
            new StringValues("max-age=0, enforce, report-uri=\"https://example.report-uri.com/r/d/ct/enforce\""));

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Feature-Policy
        // https://github.com/w3c/webappsec-feature-policy/blob/master/features.md
        // https://developers.google.com/web/updates/2018/06/feature-policy
        context.Response.Headers.Append("Feature-Policy", new StringValues(
            "accelerometer 'none';" +
            "ambient-light-sensor 'none';" +
            "autoplay 'none';" +
            "battery 'none';" +
            "camera 'none';" +
            "display-capture 'none';" +
            "document-domain 'none';" +
            "encrypted-media 'none';" +
            "execution-while-not-rendered 'none';" +
            "execution-while-out-of-viewport 'none';" +
            "gyroscope 'none';" +
            "magnetometer 'none';" +
            "microphone 'none';" +
            "midi 'none';" +
            "navigation-override 'none';" +
            "payment 'none';" +
            "picture-in-picture 'none';" +
            "publickey-credentials-get 'none';" +
            "sync-xhr 'none';" +
            "usb 'none';" +
            "wake-lock 'none';" +
            "xr-spatial-tracking 'none';"
            ));

        // https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP
        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
        context.Response.Headers.Append("Content-Security-Policy", new StringValues(
            "child-src 'none';" +
            "fenced-frame-src 'none';" +
            "frame-src 'none';" +
            "object-src 'none';" +
            "worker-src 'none';"
            ));

        return next(context);
    }
}
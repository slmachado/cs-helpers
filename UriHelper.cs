namespace Helpers;

/// <summary>
/// UriHelper class implementation.
/// </summary>
public static class UriHelper
{
    public static Uri GetUri( Uri uri, string ip, string port)
    {
        return new Uri(string.Concat(uri.Scheme, "://", ip, ":", port, uri.LocalPath));
    }
}
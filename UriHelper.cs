namespace Helpers
{
    /// <summary>
    /// UriHelper class implementation.
    /// </summary>
    public static class UriHelper
    {
        /// <summary>
        /// Constructs a new URI using the specified IP and port, retaining the scheme and local path of the original URI.
        /// </summary>
        /// <param name="uri">The original URI.</param>
        /// <param name="ip">The IP address to use in the new URI.</param>
        /// <param name="port">The port to use in the new URI.</param>
        /// <returns>A new URI constructed with the specified IP and port.</returns>
        public static Uri GetUri(Uri uri, string ip, string port)
        {
            return new Uri(string.Concat(uri.Scheme, "://", ip, ":", port, uri.LocalPath));
        }

        /// <summary>
        /// Adds or updates a query parameter in the URI.
        /// </summary>
        /// <param name="uri">The original URI.</param>
        /// <param name="paramName">The name of the query parameter to add or update.</param>
        /// <param name="paramValue">The value of the query parameter.</param>
        /// <returns>A new URI with the added or updated query parameter.</returns>
        public static Uri AddOrUpdateQueryParam(Uri uri, string paramName, string paramValue)
        {
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            query[paramName] = paramValue;
            var uriBuilder = new UriBuilder(uri)
            {
                Query = query.ToString()
            };
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Removes a query parameter from the URI.
        /// </summary>
        /// <param name="uri">The original URI.</param>
        /// <param name="paramName">The name of the query parameter to remove.</param>
        /// <returns>A new URI with the specified query parameter removed.</returns>
        public static Uri RemoveQueryParam(Uri uri, string paramName)
        {
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            query.Remove(paramName);
            var uriBuilder = new UriBuilder(uri)
            {
                Query = query.ToString()
            };
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Checks if a URI is valid.
        /// </summary>
        /// <param name="uri">The URI string to validate.</param>
        /// <returns>True if the URI is valid; otherwise, false.</returns>
        public static bool IsValidUri(string uri)
        {
            return Uri.TryCreate(uri, UriKind.Absolute, out _);
        }

        /// <summary>
        /// Gets the host and port from the URI.
        /// </summary>
        /// <param name="uri">The URI to extract the host and port from.</param>
        /// <returns>A tuple containing the host and port.</returns>
        public static (string Host, int Port) GetHostAndPort(Uri uri)
        {
            return (uri.Host, uri.Port);
        }

        /// <summary>
        /// Builds a URI from separate parts.
        /// </summary>
        /// <param name="scheme">The scheme (e.g., http, https) of the URI.</param>
        /// <param name="host">The host (e.g., example.com) of the URI.</param>
        /// <param name="port">The port of the URI.</param>
        /// <param name="path">The path (e.g., /index.html) of the URI.</param>
        /// <param name="queryParams">A dictionary of query parameters to include in the URI.</param>
        /// <returns>A new URI constructed from the specified parts.</returns>
        public static Uri BuildUri(string scheme, string host, int port, string path, IDictionary<string, string> queryParams)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = scheme,
                Host = host,
                Port = port,
                Path = path,
                Query = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={System.Web.HttpUtility.UrlEncode(kvp.Value)}"))
            };
            return uriBuilder.Uri;
        }
    }
}

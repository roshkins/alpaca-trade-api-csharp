﻿using System;

namespace Alpaca.Markets.Extensions
{
    /// <summary>
    /// Set of extensions methods for replacing target URL for Alpaca and Polygon data streaming
    /// clients (<see cref="AlpacaDataStreamingClient"/> and <see cref="PolygonStreamingClient"/>)
    /// with custom values or with local proxy WebSocket URL obtained from environment variables.
    /// </summary>
    public static class EnvironmentExtensions
    {
        private const String EnvironmentVariableName = "DATA_PROXY_WS";

        private const String DefaultAlpacaProxyAgentUri = "ws://127.0.0.1:8765";

        private sealed class ProxyEnvironment : IEnvironment
        {
            private readonly IEnvironment _environment;

            public ProxyEnvironment(
                IEnvironment environment)
            {
                _environment = environment;
#pragma warning disable 618
                PolygonStreamingApi = _environment.PolygonStreamingApi;
#pragma warning restore 618
                AlpacaDataStreamingApi = _environment.AlpacaDataStreamingApi;
            }

            public Uri PolygonStreamingApi { get; set; }

            public Uri AlpacaDataStreamingApi { get; set; }

            public Uri AlpacaTradingApi => _environment.AlpacaTradingApi;

            public Uri AlpacaDataApi => _environment.AlpacaDataApi;

#pragma warning disable 618
            public Uri PolygonDataApi => _environment.PolygonDataApi;
#pragma warning restore 618

            public Uri AlpacaStreamingApi => _environment.AlpacaStreamingApi;
        }

        /// <summary>
        /// Replaces <see cref="IEnvironment.AlpacaDataStreamingApi"/> value with the
        /// <paramref name="alpacaProxyAgentUrl"/> value or from environment variable
        /// named <c>DATA_PROXY_WS</c> with default fallback value equal to default
        /// Alpaca proxy agent local URL (<c>ws://127.0.0.1:8765</c>).
        /// </summary>
        /// <param name="environment">Original environment URLs for modification.</param>
        /// <param name="alpacaProxyAgentUrl">
        /// New value for the <see cref="IEnvironment.AlpacaDataStreamingApi"/> property
        /// in the modified <paramref name="environment"/> object.
        /// </param>
        /// <returns>New environment URLs object.</returns>
        public static IEnvironment WithProxyForAlpacaDataStreamingClient(
            this IEnvironment environment,
            Uri? alpacaProxyAgentUrl = null) =>
            new ProxyEnvironment(environment)
            {
                AlpacaDataStreamingApi = alpacaProxyAgentUrl ?? getFromEnvironmentOrDefault()
            };

        /// Replaces <see cref="IEnvironment.PolygonStreamingApi"/> value with the
        /// <paramref name="alpacaProxyAgentUrl"/> value or from environment variable
        /// named <c>DATA_PROXY_WS</c> with default fallback value equal to default
        /// Alpaca proxy agent local URL (<c>ws://127.0.0.1:8765</c>).
        /// <param name="environment">Original environment URLs for modification.</param>
        /// <param name="alpacaProxyAgentUrl">
        /// New value for the <see cref="IEnvironment.PolygonStreamingApi"/> property
        /// in the modified <paramref name="environment"/> object.
        /// </param>
        /// <returns>New environment URLs object.</returns>
        [Obsolete("This method will be removed in the next major SDK release", true)]
        public static IEnvironment WithProxyForPolygonStreamingClient(
            this IEnvironment environment,
            Uri? alpacaProxyAgentUrl = null) =>
            new ProxyEnvironment(environment)
            {
                PolygonStreamingApi = alpacaProxyAgentUrl ?? getFromEnvironmentOrDefault()
            };

        private static Uri getFromEnvironmentOrDefault() => 
            new Uri(Environment.GetEnvironmentVariable(EnvironmentVariableName)
                    ?? DefaultAlpacaProxyAgentUri);
    }
}
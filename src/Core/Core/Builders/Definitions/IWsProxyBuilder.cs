using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.Proxy.Core.Models;
using AspNetCore.Proxy.Helpers;
using AspNetCore.Proxy.Options;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Proxy.Builders;

/// <summary>
/// Interface for a WS proxy builder.
/// </summary>
public interface IWsProxyBuilder : IBuilder<IWsProxyBuilder, WsProxy>
{
    /// <summary>
    /// Sets the endpoint on `this` instance that the proxy-to-build should use.
    /// </summary>
    /// <param name="endpoint">The endpoint to set.</param>
    /// <returns>The current instance with the specified endpoint set.</returns>
    IWsProxyBuilder WithEndpoint(string endpoint);

    /// <summary>
    /// Sets the endpoint on `this` instance that the proxy-to-build should use.
    /// </summary>
    /// <param name="endpoint">The endpoint to set.  This takes the form `(<see cref="HttpContext"/>, <see cref="IDictionary{TKey,TValue}"/>) => <see cref="String"/>`.</param>
    /// <returns>The current instance with the specified endpoint set.</returns>
    IWsProxyBuilder WithEndpoint(EndpointComputerToString endpoint);

    /// <summary>
    /// Sets the endpoint on `this` instance that the proxy-to-build should use.
    /// </summary>
    /// <param name="endpoint">The endpoint to set.  This takes the form `(<see cref="HttpContext"/>, <see cref="IDictionary{String, Object}"/>) => <see cref="ValueTask{TResult}"/>`</param>
    /// <returns>The current instance with the specified endpoint set.</returns>
    IWsProxyBuilder WithEndpoint(EndpointComputerToValueTask endpoint);

    /// <summary>
    /// Sets the options builder on `this` instance that the proxy-to-build should use.
    /// </summary>
    /// <param name="options">The options builder to set.</param>
    /// <returns>The current instance with the specified options set.</returns>
    IWsProxyBuilder WithOptions(IWsProxyOptionsBuilder options);

    /// <summary>
    /// Sets the options builder on `this` instance that the proxy-to-build should use.
    /// </summary>
    /// <param name="builderAction">The options builder action to set.  This takes the form `(<see cref="IHttpProxyOptionsBuilder"/>) => void`.</param>
    /// <returns>The current instance with the specified options set.</returns>
    IWsProxyBuilder WithOptions(Action<IWsProxyOptionsBuilder> builderAction);
}
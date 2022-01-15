using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.Proxy.Helpers;
using AspNetCore.Proxy.Options;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Proxy.Builders;

/// <summary>
/// Interface for a proxy builder.
/// </summary>
public interface IProxyBuilder : IBuilder<IProxyBuilder, Core.Models.Proxy>
{
    /// <summary>
    /// Sets the route on `this` instance that the proxy-to-build should use.
    /// </summary>
    /// <param name="route">The route to set.</param>
    /// <returns>The current instance with the specified route set.</returns>
    IProxyBuilder WithRoute(string route);

    /// <summary>
    /// Sets the HTTP proxy route on `this` instance that the proxy-to-build should use.
    /// An HTTP proxy route may only be called once.
    /// </summary>
    /// <param name="endpoint">The endpoint to set.</param>
    /// <param name="builderAction">The options builder action to set.  This takes the form `(<see cref="IHttpProxyOptionsBuilder"/>) => void`.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseHttp(string endpoint, Action<IHttpProxyOptionsBuilder> builderAction = null);

    /// <summary>
    /// Sets the HTTP proxy route on `this` instance that the proxy-to-build should use.
    /// An HTTP proxy route may only be called once.
    /// </summary>
    /// <param name="endpointComputer">The endpoint to set.  This takes the form `(<see cref="HttpContext"/>, <see cref="IDictionary{TKey,TValue}"/>) => <see cref="String"/>`.</param>
    /// <param name="builderAction">The options builder action to set.  This takes the form `(<see cref="IHttpProxyOptionsBuilder"/>) => void`.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseHttp(EndpointComputerToString endpointComputer, Action<IHttpProxyOptionsBuilder> builderAction = null);

    /// <summary>
    /// Sets the HTTP proxy route on `this` instance that the proxy-to-build should use.
    /// An HTTP proxy route may only be called once.
    /// </summary>
    /// <param name="endpointComputer">The endpoint to set.  This takes the form `(<see cref="HttpContext"/>, <see cref="IDictionary{String, Object}"/>) => <see cref="ValueTask{TResult}"/>`.</param>
    /// <param name="builderAction">The options builder action to set.  This takes the form `(<see cref="IHttpProxyOptionsBuilder"/>) => void`.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseHttp(EndpointComputerToValueTask endpointComputer, Action<IHttpProxyOptionsBuilder> builderAction = null);

    /// <summary>
    /// Sets the HTTP proxy route on `this` instance that the proxy-to-build should use.
    /// An HTTP proxy route may only be called once.
    /// </summary>
    /// <param name="builderAction">The options builder action to set.  This takes the form `(<see cref="IHttpProxyOptionsBuilder"/>) => void`.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseHttp(Action<IHttpProxyBuilder> builderAction);

    /// <summary>
    /// Sets the HTTP proxy route on `this` instance that the proxy-to-build should use.
    /// An HTTP proxy route may only be called once.
    /// </summary>
    /// <param name="builder">The options builder to set.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseHttp(IHttpProxyBuilder builder);

    /// <summary>
    /// Sets the WS proxy route on `this` instance that the proxy-to-build should use.
    /// An WS proxy route may only be called once.
    /// </summary>
    /// <param name="endpoint">The endpoint to set.</param>
    /// <param name="builderAction">The options builder action to set.  This takes the form `(<see cref="IWsProxyOptionsBuilder"/>) => void`.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseWs(string endpoint, Action<IWsProxyOptionsBuilder> builderAction = null);

    /// <summary>
    /// Sets the WS proxy route on `this` instance that the proxy-to-build should use.
    /// An WS proxy route may only be called once.
    /// </summary>
    /// <param name="endpointComputer">The endpoint to set.  This takes the form `(<see cref="HttpContext"/>, <see cref="IDictionary{String, Object}"/>) => <see cref="String"/>`.</param>
    /// <param name="builderAction">The options builder action to set.  This takes the form `(<see cref="IWsProxyOptionsBuilder"/>) => void`.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseWs(EndpointComputerToString endpointComputer, Action<IWsProxyOptionsBuilder> builderAction = null);

    /// <summary>
    /// Sets the WS proxy route on `this` instance that the proxy-to-build should use.
    /// An WS proxy route may only be called once.
    /// </summary>
    /// <param name="endpointComputer">The endpoint to set.  This takes the form `(<see cref="HttpContext"/>, <see cref="IDictionary{String, Object}"/>) => <see cref="ValueTask{String}"/>`.</param>
    /// <param name="builderAction">The options builder action to set.  This takes the form `(<see cref="IWsProxyOptionsBuilder"/>) => void`.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseWs(EndpointComputerToValueTask endpointComputer, Action<IWsProxyOptionsBuilder> builderAction = null);

    /// <summary>
    /// Sets the WS proxy route on `this` instance that the proxy-to-build should use.
    /// An WS proxy route may only be called once.
    /// </summary>
    /// <param name="builderAction">The options builder action to set.  This takes the form `(<see cref="IWsProxyOptionsBuilder"/>) => void`.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseWs(Action<IWsProxyBuilder> builderAction);

    /// <summary>
    /// Sets the WS proxy route on `this` instance that the proxy-to-build should use.
    /// An WS proxy route may only be called once.
    /// </summary>
    /// <param name="builder">The options builder to set.</param>
    /// <returns>The current instance with the specified proxy route set.</returns>
    IProxyBuilder UseWs(IWsProxyBuilder builder);
}
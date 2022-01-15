using System;
using AspNetCore.Proxy.Helpers;

namespace AspNetCore.Proxy.Builders;

/// <summary>
/// Interface for a proxies builder.
/// </summary>
public interface IProxiesBuilder : IBuilder<IProxiesBuilder, Proxies>
{
    /// <summary>
    /// Adds a proxy route to the set of routes `this` builder is tracking.
    /// </summary>
    /// <param name="route">The route.</param>
    /// <param name="proxyAction">An <see cref="Action"/> that mutates a proxy builder.</param>
    /// <returns>The current instance with the specified route mapped.</returns>
    IProxiesBuilder Map(string route, Action<IProxyBuilder> proxyAction);

    /// <summary>
    /// Adds a proxy route to the set of routes `this` builder is tracking.
    /// </summary>
    /// <param name="proxyAction">An <see cref="Action"/> that mutates a proxy builder.</param>
    /// <returns>The current instance with the specified route mapped.</returns>
    IProxiesBuilder Map(Action<IProxyBuilder> proxyAction);

    /// <summary>
    /// Adds a proxy route to the set of routes `this` builder is tracking.
    /// </summary>
    /// <param name="builder">A proxy builder to build for this route.</param>
    /// <returns>The current instance with the specified route mapped.</returns>
    IProxiesBuilder Map(IProxyBuilder builder);
}
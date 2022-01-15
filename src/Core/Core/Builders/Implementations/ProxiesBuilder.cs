using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.Proxy.Builders;

/// <summary>
/// Concrete type for a proxies builder.
/// </summary>
public sealed class ProxiesBuilder : IProxiesBuilder
{
    private readonly IList<IProxyBuilder> _proxyBuilders;

    private ProxiesBuilder()
    {
        _proxyBuilders = new List<IProxyBuilder>();
    }

    /// <summary>
    /// Gets a `new`, empty instance of this type.
    /// </summary>
    /// <returns>A `new` instance of <see cref="ProxiesBuilder"/>.</returns>
    public static ProxiesBuilder Instance => new ProxiesBuilder();

    /// <inheritdoc/>
    public IProxiesBuilder New()
    {
        var instance = Instance;

        foreach(var proxyBuilder in _proxyBuilders)
            instance.Map(proxyBuilder.New());

        return instance;
    }

    /// <inheritdoc/>
    public Proxies Build()
    {
        return new Proxies(_proxyBuilders.Select(b => b.Build()));
    }

    /// <inheritdoc/>
    public IProxiesBuilder Map(string route, Action<IProxyBuilder> builderAction) => this.Map(proxy => builderAction(proxy.WithRoute(route)));

    /// <inheritdoc/>
    public IProxiesBuilder Map(Action<IProxyBuilder> builderAction)
    {
        if(builderAction == null)
            throw new ArgumentException($"{nameof(builderAction)} must not be `null`.");

        var builder = ProxyBuilder.Instance;
        builderAction(builder);

        return this.Map(builder);
    }

    /// <inheritdoc/>
    public IProxiesBuilder Map(IProxyBuilder builder)
    {
        if(builder == null)
            throw new ArgumentException($"{nameof(builder)} must not be `null`.");

        _proxyBuilders.Add(builder);
        return this;
    }
}
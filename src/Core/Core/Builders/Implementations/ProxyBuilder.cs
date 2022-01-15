using System;
using AspNetCore.Proxy.Helpers;
using AspNetCore.Proxy.Options;

namespace AspNetCore.Proxy.Builders;

/// <summary>
/// Concrete type for a proxy builder.
/// </summary>
public sealed class ProxyBuilder : IProxyBuilder
{
    private bool _isRouteless;
    private string _route;
    private IHttpProxyBuilder _httpProxyBuilder;
    private IWsProxyBuilder _wsProxyBuilder;

    private ProxyBuilder()
    {
    }

    /// <summary>
    /// Gets a `new`, empty instance of this type.
    /// </summary>
    /// <returns>A `new` instance of <see cref="ProxyBuilder"/>.</returns>
    public static ProxyBuilder Instance => new ProxyBuilder();

    internal IProxyBuilder WithIsRouteless(bool isRouteless)
    {
        _isRouteless = isRouteless;
        return this;
    }

    /// <inheritdoc/>
    public IProxyBuilder New()
    {
        return Instance
            .WithIsRouteless(_isRouteless)
            .WithRoute(_route)
            .UseHttp(_httpProxyBuilder?.New())
            .UseWs(_wsProxyBuilder?.New());
    }

    /// <inheritdoc/>
    public Core.Models.Proxy Build()
    {
        if(_httpProxyBuilder == null && _wsProxyBuilder == null)
            throw new Exception($"At least one endpoint must be defined with `{nameof(UseHttp)}` or `{nameof(UseWs)}`.");

        return new Core.Models.Proxy(
            _route,
            _httpProxyBuilder?.Build(),
            _wsProxyBuilder?.Build());
    }

    /// <inheritdoc/>
    public IProxyBuilder WithRoute(string route)
    {
        if(_isRouteless)
            throw new Exception("This is a `routeless` Proxy builder (i.e., likely used with `RunProxy`): adding a route in this context is a no-op that should be removed.");

        _route = route;

        return this;
    }

    /// <inheritdoc/>
    public IProxyBuilder UseHttp(string endpoint, Action<IHttpProxyOptionsBuilder> builderAction = null) => this.UseHttp(httpProxy => httpProxy.WithEndpoint(endpoint).WithOptions(builderAction));

    /// <inheritdoc/>
    public IProxyBuilder UseHttp(EndpointComputerToString endpointComputer, Action<IHttpProxyOptionsBuilder> builderAction = null) => this.UseHttp(httpProxy => httpProxy.WithEndpoint(endpointComputer).WithOptions(builderAction));

    /// <inheritdoc/>
    public IProxyBuilder UseHttp(EndpointComputerToValueTask endpointComputer, Action<IHttpProxyOptionsBuilder> builderAction = null) => this.UseHttp(httpProxy => httpProxy.WithEndpoint(endpointComputer).WithOptions(builderAction));

    /// <inheritdoc/>
    public IProxyBuilder UseHttp(Action<IHttpProxyBuilder> builderAction)
    {
        var builder = HttpProxyBuilder.Instance;
        builderAction?.Invoke(builder);

        return this.UseHttp(builder);
    }

    /// <inheritdoc/>
    public IProxyBuilder UseHttp(IHttpProxyBuilder builder)
    {
        if(_httpProxyBuilder != null)
            throw new InvalidOperationException("Cannot set more than one HTTP proxy endpoint.");

        _httpProxyBuilder = builder;

        return this;
    }

    /// <inheritdoc/>
    public IProxyBuilder UseWs(string endpoint, Action<IWsProxyOptionsBuilder> builderAction = null) => this.UseWs(wsProxy => wsProxy.WithEndpoint(endpoint).WithOptions(builderAction));

    /// <inheritdoc/>
    public IProxyBuilder UseWs(EndpointComputerToString endpointComputer, Action<IWsProxyOptionsBuilder> builderAction = null) => this.UseWs(wsProxy => wsProxy.WithEndpoint(endpointComputer).WithOptions(builderAction));

    /// <inheritdoc/>
    public IProxyBuilder UseWs(EndpointComputerToValueTask endpointComputer, Action<IWsProxyOptionsBuilder> builderAction = null) => this.UseWs(wsProxy => wsProxy.WithEndpoint(endpointComputer).WithOptions(builderAction));

    /// <inheritdoc/>
    public IProxyBuilder UseWs(Action<IWsProxyBuilder> builderAction)
    {
        var builder = WsProxyBuilder.Instance;
        builderAction?.Invoke(builder);

        return this.UseWs(builder);
    }

    /// <inheritdoc/>
    public IProxyBuilder UseWs(IWsProxyBuilder builder)
    {
        if(_wsProxyBuilder != null)
            throw new InvalidOperationException("Cannot set more than one WebSocket proxy endpoint.");

        _wsProxyBuilder = builder;

        return this;
    }
}
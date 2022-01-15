using System;
using System.Threading.Tasks;
using AspNetCore.Proxy.Core.Models;
using AspNetCore.Proxy.Helpers;
using AspNetCore.Proxy.Options;

namespace AspNetCore.Proxy.Builders;

/// <summary>
/// Concrete type for a WS proxy builder.
/// </summary>
public sealed class WsProxyBuilder : IWsProxyBuilder
{
    private EndpointComputerToValueTask _endpointComputer;

    private IWsProxyOptionsBuilder _optionsBuilder;

    private WsProxyBuilder()
    {
    }

    /// <summary>
    /// Gets a `new`, empty instance of this type.
    /// </summary>
    /// <returns>A `new` instance of <see cref="WsProxyBuilder"/>.</returns>
    public static WsProxyBuilder Instance => new WsProxyBuilder();

    /// <inheritdoc/>
    public IWsProxyBuilder New()
    {
        return Instance
            .WithEndpoint(_endpointComputer)
            .WithOptions(_optionsBuilder?.New());
    }

    /// <inheritdoc/>
    public WsProxy Build()
    {
        if(_endpointComputer == null)
            throw new Exception("The endpoint must be specified on this WebSocket proxy builder.");

        return new WsProxy(
            _endpointComputer,
            _optionsBuilder?.Build());
    }

    /// <inheritdoc/>
    public IWsProxyBuilder WithEndpoint(string endpoint) => this.WithEndpoint((context, args) => new ValueTask<string>(endpoint));

    /// <inheritdoc/>
    public IWsProxyBuilder WithEndpoint(EndpointComputerToString endpointComputer) => this.WithEndpoint((context, args) => new ValueTask<string>(endpointComputer(context, args)));

    /// <inheritdoc/>
    public IWsProxyBuilder WithEndpoint(EndpointComputerToValueTask endpointComputer)
    {
        _endpointComputer = endpointComputer;
        return this;
    }

    /// <inheritdoc/>
    public IWsProxyBuilder WithOptions(IWsProxyOptionsBuilder optionsBuilder)
    {
        _optionsBuilder = optionsBuilder;
        return this;
    }

    /// <inheritdoc/>
    public IWsProxyBuilder WithOptions(Action<IWsProxyOptionsBuilder> builderAction)
    {
        _optionsBuilder = WsProxyOptionsBuilder.Instance;
        builderAction?.Invoke(_optionsBuilder);

        return this;
    }
}
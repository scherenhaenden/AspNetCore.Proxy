using System;
using System.Threading.Tasks;
using AspNetCore.Proxy.Helpers;
using AspNetCore.Proxy.Options;

namespace AspNetCore.Proxy.Builders;

/// <summary>
/// Concrete type for an HTTP proxy builder.
/// </summary>
public sealed class HttpProxyBuilder : IHttpProxyBuilder
{
    private EndpointComputerToValueTask _endpointComputer;

    private IHttpProxyOptionsBuilder _optionsBuilder;

    private HttpProxyBuilder()
    {
    }

    /// <summary>
    /// Gets a `new`, empty instance of this type.
    /// </summary>
    /// <returns>A `new` instance of <see cref="HttpProxyOptionsBuilder"/>.</returns>
    public static HttpProxyBuilder Instance => new HttpProxyBuilder();

    /// <inheritdoc/>
    public IHttpProxyBuilder New()
    {
        return Instance
            .WithEndpoint(_endpointComputer)
            .WithOptions(_optionsBuilder?.New());
    }

    /// <inheritdoc/>
    public HttpProxy Build()
    {
        if(_endpointComputer == null)
            throw new Exception("The endpoint must be specified on this HTTP proxy builder.");

        return new HttpProxy(
            _endpointComputer,
            _optionsBuilder?.Build());
    }

    /// <inheritdoc/>
    public IHttpProxyBuilder WithEndpoint(string endpoint) => this.WithEndpoint((context, args) => new ValueTask<string>(endpoint));

    /// <inheritdoc/>
    public IHttpProxyBuilder WithEndpoint(EndpointComputerToString endpointComputer) => this.WithEndpoint((context, args) => new ValueTask<string>(endpointComputer(context, args)));

    /// <inheritdoc/>
    public IHttpProxyBuilder WithEndpoint(EndpointComputerToValueTask endpointComputer)
    {
        _endpointComputer = endpointComputer;
        return this;
    }

    /// <inheritdoc/>
    public IHttpProxyBuilder WithOptions(IHttpProxyOptionsBuilder optionsBuilder)
    {
        _optionsBuilder = optionsBuilder;
        return this;
    }

    /// <inheritdoc/>
    public IHttpProxyBuilder WithOptions(Action<IHttpProxyOptionsBuilder> builderAction)
    {
        _optionsBuilder = HttpProxyOptionsBuilder.Instance;
        builderAction?.Invoke(_optionsBuilder);

        return this;
    }
}
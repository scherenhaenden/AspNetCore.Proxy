using AspNetCore.Proxy.Helpers;
using AspNetCore.Proxy.Options;

namespace AspNetCore.Proxy.Builders
{
    /// <summary>
    /// Concrete type for an HTTP proxy definition.
    /// </summary>
    public sealed class HttpProxy
    {
        /// <summary>
        /// EndpointComputer property.
        /// </summary>
        /// <value>The endpoint computer to use when proxying requests.</value>
        public EndpointComputerToValueTask EndpointComputer { get; internal set; }

        /// <summary>
        /// Options property.
        /// </summary>
        /// <value>The options to use when proxying requests.</value>
        public HttpProxyOptions Options { get; internal set; }

        internal HttpProxy(EndpointComputerToValueTask endpointComputer, HttpProxyOptions options)
        {
            EndpointComputer = endpointComputer;
            Options = options;
        }
    }
}
using AspNetCore.Proxy.Helpers;
using AspNetCore.Proxy.Options;

namespace AspNetCore.Proxy.Core.Models
{
    /// <summary>
    /// Concrete type for an WS proxy definition.
    /// </summary>
    public class WsProxy
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
        public WsProxyOptions Options { get; internal set; }

        internal WsProxy(EndpointComputerToValueTask endpointComputer, WsProxyOptions options)
        {
            EndpointComputer = endpointComputer;
            Options = options;
        }
    }
}
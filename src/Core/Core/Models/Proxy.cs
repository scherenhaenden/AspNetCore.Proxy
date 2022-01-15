using AspNetCore.Proxy.Builders;

namespace AspNetCore.Proxy.Core.Models
{
    /// <summary>
    /// Concrete type for a proxy definition.
    /// </summary>
    public class Proxy
    {
        /// <summary>
        /// Route property.
        /// </summary>
        /// <value>The route to proxy.</value>
        public string Route { get; internal set; }

        /// <summary>
        /// HttpProxy property.
        /// </summary>
        /// <value>The route to proxy.</value>
        public HttpProxy HttpProxy { get; internal set; }

        /// <summary>
        /// HttpProxy property.
        /// </summary>
        /// <value>The route to proxy.</value>
        public WsProxy WsProxy { get; internal set; }

        internal Proxy(string route, HttpProxy httpProxy, WsProxy wsProxy)
        {
            Route = route;
            HttpProxy = httpProxy;
            WsProxy = wsProxy;
        }
    }
}
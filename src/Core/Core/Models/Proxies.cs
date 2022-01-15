using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.Proxy.Builders
{
    /// <summary>
    /// Concrete type for a proxies definition.
    /// </summary>
    public class Proxies : IEnumerable<Core.Models.Proxy>
    {
        private readonly IList<Core.Models.Proxy> _proxies;

        /// <summary>
        /// The constructor for <see cref="Proxies"/>.
        /// </summary>
        /// <param name="proxies">The set of proxy routes to handle.</param>
        internal Proxies(IEnumerable<Core.Models.Proxy> proxies)
        {
            _proxies = proxies.ToList();
        }

        /// <inheritdoc/>
        public IEnumerator<Core.Models.Proxy> GetEnumerator() => _proxies.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
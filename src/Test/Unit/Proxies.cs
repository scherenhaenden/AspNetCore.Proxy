using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.Proxy.Builders;
using Xunit;

namespace AspNetCore.Proxy.Tests.Unit
{
    public class Proxies
    {
        [Fact]
        public async Task CanEnumerateProxies()
        {
            const string route = "from";
            const string endpoint = "to";
            var proxies = ProxiesBuilder.Instance.Map(route, b => b.UseHttp(endpoint)).New().Build() as IEnumerable;

            // Some of this is to exercise the enumerator.
            List<Core.Models.Proxy> newProxies = new List<Core.Models.Proxy>();
            foreach(var o in proxies)
                newProxies.Add(o as Core.Models.Proxy);

            Assert.Single(newProxies);

            var newProxy = newProxies[0];
            Assert.Equal(route, newProxies[0].Route);
            Assert.Equal(endpoint, await newProxy.HttpProxy.EndpointComputer(null, null));
        }

        [Fact]
        public void CanProxiesBuilderFailWithNullProxyBuilder()
        {
            Assert.ThrowsAny<Exception>(() => ProxiesBuilder.Instance.Map(null as Action<IProxyBuilder>));

            Assert.ThrowsAny<Exception>(() => ProxiesBuilder.Instance.Map(null as IProxyBuilder));
        }
    }
}
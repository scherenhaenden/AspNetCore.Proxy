using System;
using System.Threading.Tasks;
using AspNetCore.Proxy.Builders;
using Xunit;

namespace AspNetCore.Proxy.Tests.Unit
{
    public class Proxy
    {
        [Fact]
        public async Task CanExerciseProxyBuilder()
        {
            const string httpEndpoint = "http://from";
            const string wsEndpoint = "ws://from";

            var proxyString = ProxyBuilder.Instance.UseHttp(httpEndpoint).UseWs(wsEndpoint).New().Build();
            Assert.Equal(httpEndpoint, await proxyString.HttpProxy.EndpointComputer(null, null));
            Assert.Equal(wsEndpoint, await proxyString.WsProxy.EndpointComputer(null, null));

            var proxyComputerToString = ProxyBuilder.Instance.UseHttp((c, a) => httpEndpoint).UseWs((c, a) => wsEndpoint).New().Build();
            Assert.Equal(httpEndpoint, await proxyComputerToString.HttpProxy.EndpointComputer(null, null));
            Assert.Equal(wsEndpoint, await proxyComputerToString.WsProxy.EndpointComputer(null, null));

            var proxyComputerToValueTask = ProxyBuilder.Instance.UseHttp((c, a) => new ValueTask<string>(httpEndpoint)).UseWs((c, a) => new ValueTask<string>(wsEndpoint)).New().Build();
            Assert.Equal(httpEndpoint, await proxyComputerToValueTask.HttpProxy.EndpointComputer(null, null));
            Assert.Equal(wsEndpoint, await proxyComputerToValueTask.WsProxy.EndpointComputer(null, null));
        }

        [Fact]
        public void CanProxyBuilderFailWithoutHttpOrWsProxy()
        {
            Assert.ThrowsAny<Exception>(() => ProxyBuilder.Instance.New().Build());
        }

        [Fact]
        public void CanProxyBuilderFailWithMultiplProxiesOfSameType()
        {
            Assert.ThrowsAny<Exception>(() => ProxyBuilder.Instance.UseHttp("").UseHttp(""));

            Assert.ThrowsAny<Exception>(() => ProxyBuilder.Instance.UseWs("").UseWs(""));
        }

        [Fact]
        public void CanProxyBuilderFailWhenRoutelessAbused()
        {
            Assert.ThrowsAny<Exception>(() => ProxyBuilder.Instance.WithIsRouteless(true).WithRoute(""));
        }
    }
}
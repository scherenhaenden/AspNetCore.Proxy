using System.Threading.Tasks;
using AspNetCore.Proxy.Extensions;
using Microsoft.AspNetCore.Builder;
using Moq;
using Xunit;

namespace AspNetCore.Proxy.Tests.Unit
{
    public class BasicExtensions
    {
        [Fact]
        public void CanExerciseRunProxy()
        {
            const string endpoint = "garbage";

            var app = Mock.Of<IApplicationBuilder>();

            Basic.RunProxy(app, (c, a) => new ValueTask<string>(endpoint), (c, a) => new ValueTask<string>(endpoint));
            Basic.RunProxy(app, (c, a) => endpoint, (c, a) =>  endpoint);
            Basic.RunProxy(app, endpoint, endpoint);

            Basic.RunHttpProxy(app, b => b.WithEndpoint(endpoint));
            Basic.RunHttpProxy(app, (c, a) => new ValueTask<string>(endpoint));
            Basic.RunHttpProxy(app, (c, a) =>  endpoint);
            Basic.RunHttpProxy(app, endpoint);

            Basic.RunWsProxy(app, b => b.WithEndpoint(endpoint));
            Basic.RunWsProxy(app, (c, a) => new ValueTask<string>(endpoint));
            Basic.RunWsProxy(app, (c, a) =>  endpoint);
            Basic.RunWsProxy(app, endpoint);
        }

        [Fact]
        public void CanRemoveTrailingSlashes()
        {
            const string expected = "http://myaddresswithtoomanyslashes.com";

            var result = Helpers.Helpers.TrimTrailingSlashes($"{expected}////");

            Assert.Equal(expected, result);
        }
    }
}
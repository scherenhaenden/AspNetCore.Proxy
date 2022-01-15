using System;
using Xunit;

namespace AspNetCore.Proxy.Tests.Unit
{
    public class Other
    {
        [Fact]
        public void CanFailOnBadFormContentType()
        {
            var contentType = "text/plain";

            var e = Assert.ThrowsAny<Exception>(() => {
                var dummy = Helpers.Helpers.ToHttpContent(null, contentType);
            });

            Assert.Equal($"Unknown form content type `{contentType}`.", e.Message);
        }
    }
}
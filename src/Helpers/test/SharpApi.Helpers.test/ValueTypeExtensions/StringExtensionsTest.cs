using SharpApi.Helpers.ValueTypeExtensions;
using Xunit;

namespace SharpApi.Helpers.test.ValueTypeExtensions
{
    public class StringExtensionsTest
    {
        [Fact]
        public void IsNullOrEmptyTest()
        {
            Assert.False("test string".IsNullOrEmpty());

            Assert.True(default(string).IsNullOrEmpty());

            Assert.True(string.Empty.IsNullOrEmpty());
        }
    }
}

using System.Dynamic;
using SharpApi.Helpers.ObjectExtensions.Deserialize;
using Xunit;

namespace SharpApi.Helpers.test.ObjectExtensions
{
    public class ObjectDeserializationHelperTest
    {
        [Fact]
        public void Test1()
        {
            var jsonString = "{\"id\":\"1\"}";

            var o = (dynamic)jsonString.FromJson<ExpandoObject>();
        }
    }
}

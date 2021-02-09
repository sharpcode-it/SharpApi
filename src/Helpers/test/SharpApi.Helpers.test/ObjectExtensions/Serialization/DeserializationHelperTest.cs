using System;
using SharpApi.Helpers.ObjectExtensions.Serialization;
using SharpApi.Helpers.test.MockUp;
using Xunit;

namespace SharpApi.Helpers.test.ObjectExtensions.Serialization
{
    public class DeserializationHelperTest
    {
        [Fact]
        public void FromByteArrayTest()
        {
            var fooObject = new Foo{Id=Guid.NewGuid(),CreateDateTime = DateTime.UtcNow,ClassName = this.GetType().FullName};

            var oByte = fooObject.ToByteArray();

            var fooObjectDeserialized = oByte.FromByteArray<Foo>();

            Assert.NotNull(fooObjectDeserialized);

            Assert.True(fooObjectDeserialized.Equals(fooObject));

            var wrongTypeObjectDeserialized = oByte.FromByteArray<DateTime>();

            Assert.True(wrongTypeObjectDeserialized.Equals(default));
        }
    }
}

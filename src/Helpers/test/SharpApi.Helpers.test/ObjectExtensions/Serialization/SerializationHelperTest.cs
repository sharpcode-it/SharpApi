using System;
using SharpApi.Helpers.ObjectExtensions.Serialization;
using SharpApi.Helpers.test.Model;
using Xunit;

namespace SharpApi.Helpers.test.ObjectExtensions.Serialization
{
    public class SerializationHelperTest
    {
        [Fact]
        public void ToByteArrayTest()
        {
            var fooObject = new Foo(){Id=Guid.NewGuid(),CreateDateTime = DateTime.UtcNow,ClassName = this.GetType().FullName};

            var oByte = fooObject.ToByteArray();

            Assert.NotNull(oByte);
            
            Assert.True(oByte.Length>0);
        }

        [Fact]
        public void ToXmlTest()
        {
            var fooObject = new Foo
                {Id = Guid.NewGuid(), CreateDateTime = DateTime.UtcNow, ClassName = this.GetType().FullName};

            Assert.NotNull(fooObject.ToXml());
        }
    }
}

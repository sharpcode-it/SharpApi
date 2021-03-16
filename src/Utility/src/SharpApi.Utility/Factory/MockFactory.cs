using System;
using System.Reflection;

namespace SharpApi.Utility
{
    public static partial class MockFactory
    {
       public static void Initialize(MockConfigOptions mockConfigOptions = null)
        {
            _internalConfigOptions = mockConfigOptions ?? new MockConfigOptions();
        }

        public static T Mock<T>(object configOptions=null)
        {
            if(!IsInitialized()) Initialize();

            var myObject = Create<T>(out var objectType, configOptions);

            switch (objectType)
            {
                case ObjectType.List:
                case ObjectType.ValueType:
                case ObjectType.Dictionary:
                    return myObject;
                case ObjectType.Object:
                    return myObject.Mock();
                default:
                    throw new NotImplementedException();
            }
        }

        #region Private Method
        private static T Mock<T>(this T istance)
        {
            var properties = istance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var p in properties)
            {
                // If not writable then cannot null it; if not readable then cannot check it's value
                if (!p.CanWrite || !p.CanRead) { continue; }

                var mget = p.GetGetMethod(false);
                var mset = p.GetSetMethod(false);

                // Get and set methods have to be public
                if (mget == null) { continue; }
                if (mset == null) { continue; }

                SetProperty(p,istance);

            }

            return istance;
        }
        #endregion
        
    }
}

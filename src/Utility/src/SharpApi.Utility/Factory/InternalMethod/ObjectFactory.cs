using System;
using System.Collections;
using System.Collections.Generic;
using SharpApi.Helpers.ObjectExtensions;

namespace SharpApi.Utility
{
    public static partial class MockFactory
    {
        private static T Create<T>(out ObjectType objectType, object configOptions=null)
        {
            var classType = typeof(T);
            if ((classType.IsValueType) || classType == typeof(string) || classType == typeof(Guid))
            {
                objectType = ObjectType.ValueType;
                return (T) classType.SetPropertyValue(configOptions);
            }

            if (typeof(T).IsList())
            {
                objectType = ObjectType.List;
                return CreateList<T>();
            }

            if (typeof(T).IsDictionary())
            {
                objectType = ObjectType.Dictionary;
                return CreateDictionary<T>();
            }

            objectType = ObjectType.Object;
            return (T) Activator.CreateInstance(typeof(T));
        }

        private static T CreateList<T>()
        {
            var type = typeof(T).GetGenericArguments()[0];
            var listType = typeof(List<>).MakeGenericType(type);
            var myList = (IList)Activator.CreateInstance(listType);

            for (var i = 0; i < RandomGenerator.GetInt(_internalConfigOptions.ListOptions.MinItems,
                _internalConfigOptions.ListOptions.MaxItems+1); i++)
            {
                myList.Add(type.SetPropertyValue());
            }

            return (T)myList;
        }

        private static T CreateDictionary<T>()
        {
            var typeKey = typeof(T).GetGenericArguments()[0];
            var typeValue = typeof(T).GetGenericArguments()[1];
            var listType = typeof(Dictionary<,>).MakeGenericType(typeKey, typeValue);
            var list = (IDictionary)Activator.CreateInstance(listType);

            var loopRangeMax = RandomGenerator.GetInt(_internalConfigOptions.ListOptions.MinItems,
                _internalConfigOptions.ListOptions.MaxItems+1);

            for (var i = 0; i < loopRangeMax; i++)
            {
                list.Add(
                    typeKey.SetKeyValue(i), typeValue.SetPropertyValue());
                //switch (_internalConfigOptions.DictionaryConfigOptions.DictionaryKeyOptions.KeyMode)
                //{
                //    case DictionaryKeyMode.Random:
                //        list.Add(
                //            typeKey.SetKeyValue(i), typeValue.SetPropertyValue());
                //        break;
                //    case DictionaryKeyMode.Static:
                //        list.Add(typeKey.SetKeyValue(i),typeValue.SetPropertyValue());
                //        break;
                //    case DictionaryKeyMode.Mixed:
                //        break;
                //    default:
                //        throw new ArgumentOutOfRangeException();
                //}
            }

            return (T)list;
        }

        private static object CreateDictionary(Type typeKey, Type typeValue)
        {
            var listType = typeof(Dictionary<,>).MakeGenericType(typeKey, typeValue);
            var list = (IDictionary)Activator.CreateInstance(listType);

            for (var i = 0; i < RandomGenerator.GetInt(_internalConfigOptions.ListOptions.MinItems,
                _internalConfigOptions.ListOptions.MaxItems+1); i++)
            {
                list.Add(typeKey.SetPropertyValue(), typeValue.SetPropertyValue());
            }

            return list;
        }

        private static object CreateList(Type type)
        {
            var listType = typeof(List<>).MakeGenericType(type);
            var myList = (IList)Activator.CreateInstance(listType);

            for (var i = 0;
                i < RandomGenerator.GetInt(_internalConfigOptions.ListOptions.MinItems,
                    _internalConfigOptions.ListOptions.MaxItems+1);
                i++)
            {
                myList.Add(type.SetPropertyValue());
            }

            return myList;
        }
    }
}

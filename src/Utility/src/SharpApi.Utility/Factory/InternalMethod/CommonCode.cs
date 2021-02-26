using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SharpApi.Helpers.ObjectExtensions;

namespace SharpApi.Utility
{
    public static partial class MockFactory
    {
        private static MockConfigOptions _internalConfigOptions;

        private static bool IsInitialized()
        {
            return !(_internalConfigOptions is null);
        }

        private static void SetProperty<T>(this PropertyInfo pInfo, T instance)
        {
            var pInfoType = pInfo.PropertyType;
            switch (pInfoType)
            {
                case TypeInfo typeInfo:
                    if (typeInfo.IsGenericType)
                    {
                        SetGenericTypeProperty(pInfo, instance);
                        break;
                    }

                    SetTypeProperty(pInfo, instance);
                    break;
            }
        }

        private static void SetGenericTypeProperty<T>(this PropertyInfo pInfo, T instance)
        {
            if (pInfo.PropertyType.IsDictionary())
            {
                var typeKey = pInfo.PropertyType.GetGenericArguments()[0];
                var typeValue = pInfo.PropertyType.GetGenericArguments()[1];
                var listType = typeof(Dictionary<,>).MakeGenericType(typeKey, typeValue);
                var list = (IDictionary)Activator.CreateInstance(listType);

                for (var i = 0; i < RandomGenerator.GetInt(_internalConfigOptions.ListOptions.MinItems,
                    _internalConfigOptions.ListOptions.MaxItems + 1); i++)
                {
                    list.Add(typeKey.SetKeyValue(i), SetPropertyValue(typeValue));
                }
                pInfo.SetValue(instance, list);
                return;
            }

            if (pInfo.PropertyType.IsList())
            {
                var type = pInfo.PropertyType.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(type);
                var list = (IList)Activator.CreateInstance(listType);

                for (var i = 0;
                    i < RandomGenerator.GetInt(_internalConfigOptions.ListOptions.MinItems,
                        _internalConfigOptions.ListOptions.MaxItems + 1);
                    i++)
                {
                    list.Add(SetPropertyValue(type));
                }

                pInfo.SetValue(instance, list);
            }

            if (pInfo.PropertyType.Implements<IEnumerable>())
            {
                var type = pInfo.PropertyType.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(type);
                var list = (IList)Activator.CreateInstance(listType);

                for (var i = 0; i < RandomGenerator.GetInt(_internalConfigOptions.ListOptions.MinItems,
                    _internalConfigOptions.ListOptions.MaxItems + 1); i++)
                {
                    list.Add(SetPropertyValue(type));
                }

                pInfo.SetValue(instance, list);
            }
        }

        private static object SetKeyValue(this Type type, int keyIndex)
        {
            switch (_internalConfigOptions.DictionaryConfigOptions.DictionaryKeyOptions.KeyMode)
            {
                case DictionaryKeyMode.Static:
                    if (type.IsNumericType()) return (keyIndex + 1);
                    if (type == typeof(string))
                        return string.Concat(
                            _internalConfigOptions.DictionaryConfigOptions.DictionaryKeyOptions.KeyPrefix, keyIndex);
                    break;
                case DictionaryKeyMode.Mixed:
                    if (type == typeof(string))
                        return string.Concat(
                            _internalConfigOptions.DictionaryConfigOptions.DictionaryKeyOptions.KeyPrefix, type.SetPropertyValue());
                    break;
            }

            return type.SetPropertyValue();
        }

        private static object SetPropertyValue(this Type type, object configOptions = null)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Object:
                    {
                        if (type.IsList()) return CreateList(type.GetGenericArguments()[0]);

                        if (type.IsDictionary())
                            return CreateDictionary(type.GetGenericArguments()[0],
                                type.GetGenericArguments()[1]);

                        if (type.Implements<IEnumerable>()) return CreateList(type.GetGenericArguments()[0]);

                        return type.Name.ToLowerInvariant().Equals("guid")
                            ? Guid.NewGuid()
                            : Activator.CreateInstance(type).Mock();

                    }
                case TypeCode.Boolean:
                    return RandomGenerator.GetBoolean();
                case TypeCode.Byte:
                    return RandomGenerator.GetInt(byte.MinValue, byte.MaxValue);
                case TypeCode.Char:
                    return RandomGenerator.GetChar();
                case TypeCode.DateTime:
                    return RandomGenerator.GetDateTime();
                //case TypeCode.DBNull:
                //    break;
                //case TypeCode.Decimal:
                //    break;
                //case TypeCode.Double:
                //    break;
                //case TypeCode.Empty:
                //    break;
                case TypeCode.Int16:
                    return RandomGenerator.GetInt16();
                case TypeCode.Int32:
                    switch (configOptions)
                    {
                        case null:
                            return RandomGenerator.GetInt();
                        default:
                            return configOptions switch
                            {
                                NumberConfigOptions<int> sc => RandomGenerator.GetInt(sc.MinValue, sc.MaxValue + 1),
                                _ => RandomGenerator.GetInt()
                            };
                    }
                case TypeCode.Int64:
                    return RandomGenerator.GetInt64();
                case TypeCode.SByte:
                    return RandomGenerator.GetInt(sbyte.MinValue, sbyte.MaxValue);
                //case TypeCode.Single:
                //    break;
                case TypeCode.String:
                    {
                        switch (configOptions)
                        {
                            case null:
                                return RandomGenerator.GetString(_internalConfigOptions.StringOptions.MinLenght
                                    , _internalConfigOptions.StringOptions.MaxLenght + 1);
                            default:
                                switch (configOptions)
                                {
                                    case StringConfigOptions sc:
                                        return RandomGenerator.GetString(sc.MinLenght
                                            , sc.MaxLenght + 1);
                                    case MockConfigOptions mc:
                                        if (mc.StringOptions != null)
                                            return RandomGenerator.GetString(mc.StringOptions.MinLenght
                                                , mc.StringOptions.MaxLenght + 1);
                                        return RandomGenerator.GetString(_internalConfigOptions.StringOptions.MinLenght
                                            , _internalConfigOptions.StringOptions.MaxLenght + 1);
                                }

                                return RandomGenerator.GetString(_internalConfigOptions.StringOptions.MinLenght
                                    , _internalConfigOptions.StringOptions.MaxLenght + 1);
                        }
                    }
                case TypeCode.UInt16:
                    return RandomGenerator.GetUInt16();
                case TypeCode.UInt32:
                    return RandomGenerator.GetUInt32();
                case TypeCode.UInt64:
                    return RandomGenerator.GetUInt64();
                default:
                    return null;
            }
        }

        private static void SetTypeProperty<T>(this PropertyInfo pInfo, T instance)
        {
            pInfo.SetValue(instance, pInfo.PropertyType.SetPropertyValue());
        }
    }
}

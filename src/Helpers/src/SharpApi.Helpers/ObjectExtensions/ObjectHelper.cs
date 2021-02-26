namespace SharpApi.Helpers.ObjectExtensions
{
    public static class ObjectHelper
    {
        public static bool IsNull(this object? istance)
        {
            return (istance is null);
        }

        public static bool IsType<T>(this object? istance)
        {
            return !istance.IsNull() && istance?.GetType() == typeof(T);
        }
    }
}

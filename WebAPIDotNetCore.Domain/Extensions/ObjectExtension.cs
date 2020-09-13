using System.Linq;

namespace WebAPIDotNetCore.Domain.Extensions
{
    public static class ObjectExtension
    {
        public static void CopyValues<T>(this T oldObject, T newObject)
        {
            if (oldObject == null || newObject == null)
                return;

            var properties = oldObject.GetType().GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(newObject, null);
                if (value != null)
                    prop.SetValue(oldObject, value, null);
            }
        }
    }
}

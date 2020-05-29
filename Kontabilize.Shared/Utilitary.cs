namespace Kontabilize.Shared
{
    public static class Utilitaty
    {
        public static T Equals<T>(T obj, T newObj) where T : class
        {
            if (obj != null && newObj != null && obj.Equals(newObj))
            {
                return obj;
            }

            return newObj;
        }
    }
}
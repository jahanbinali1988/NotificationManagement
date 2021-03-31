using System.Collections.Generic;

namespace Common.Core
{
    public static class Enforce
    {
        public static class That
        {
            public static void CollectionHasBeenInitialized<T>(ref List<T> list)
            {
                list ??= new List<T>();
            }
        }
    }
}

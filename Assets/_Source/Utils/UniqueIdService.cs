using System;
using System.Collections.Generic;

namespace Utils
{
    public static class UniqueIdService
    {
        private static readonly Dictionary<Type, int> UsedId = new();

        public static int CreateId<T>()
        {
            var type = typeof(T);
            
            UsedId.TryAdd(type, 0);

            return ++UsedId[type];
        }
    }
}
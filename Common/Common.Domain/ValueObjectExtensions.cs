using System.Collections.Generic;
using System.Linq;

namespace Common.Domain
{
    public static class ValueObjectExtensions
    {
        public static void Update<T>(this IList<T> sourceList, IList<T> destinationList)
        {
            var added = destinationList.Except(sourceList).ToList();
            var deleted = sourceList.Except(destinationList).ToList();
            added.ForEach(sourceList.Add);
            deleted.ForEach(a => sourceList.Remove(a));
        }
    }
}

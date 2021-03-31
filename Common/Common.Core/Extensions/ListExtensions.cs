using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Core.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<(T, T)> GetAllPairs<T>(this List<T> source)
        {
            return source.SelectMany((element, firstIndex) => source.Where((item, secondIndex) => firstIndex < secondIndex),
                (firstElement, secondElement) => (firstElement, secondElement));
        }
    }
}

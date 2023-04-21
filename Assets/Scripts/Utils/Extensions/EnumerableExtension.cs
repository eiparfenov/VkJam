using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.Extensions
{
    public static class EnumerableExtension
    {
        public static T RandomOrDefault<T>(this IEnumerable<T> source)
        {
            var arr = source.ToArray();
            if (arr.Length == 0) return default;
            return arr[Random.Range(0, arr.Length)];
        }
    }
}
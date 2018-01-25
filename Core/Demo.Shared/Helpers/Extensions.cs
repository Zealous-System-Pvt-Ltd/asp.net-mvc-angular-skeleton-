using System.Collections.Generic;
using System.Linq;

namespace Demo.Shared.Helpers
{
    public static class Extensions
    {
        public static string DictionaryToString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key.ToString() + "=" + kv.Value.ToString()).ToArray()) + "}";
        }
    }
}
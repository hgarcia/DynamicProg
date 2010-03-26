using System.Collections;

namespace LaTrompa.Extensions
{
    public static class CollectionExtensions
    {
        public static IDictionary Combine(this IDictionary result, IDictionary toCombine)
        {
            foreach (var k in toCombine.Keys)
            {
                result.Add(k,toCombine[k]);
            }
            return result;
        }
    }
}
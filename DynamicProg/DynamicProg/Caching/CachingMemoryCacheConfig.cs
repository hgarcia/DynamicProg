using System.Xml;

namespace DynamicProg.Caching
{
    ///<summary>
    /// Configuration options for the <see cref="MemoryCache"/> object
    ///</summary>
    public class CachingMemoryCacheConfig : CachingType
    {
        ///<summary>
        ///Constructor
        ///</summary>
        ///<param name="node">The memoryCache <see cref="XmlNode"/> from the configuration section</param>
        public CachingMemoryCacheConfig(XmlNode node)
            : base(node)
        {
        }
    }
}
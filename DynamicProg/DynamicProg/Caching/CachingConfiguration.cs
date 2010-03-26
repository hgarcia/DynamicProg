using System;
using System.Configuration;
using System.Xml;

namespace DynamicProg.Caching
{
    ///<summary>
    /// Access the configuration for the LaTrompa Caching
    ///</summary>
    public class CachingConfiguration
    {
        ///<summary>
        /// Returns a configuration reader object for the "laTrompaCaching" configuration section
        ///</summary>
        ///<returns>A <c>LaTrompaCachingConfigurationReader</c></returns>
        public static LaTrompaCachingConfigurationReader GetConfig()
        {
            var node = (XmlNode)ConfigurationManager.GetSection("laTrompaCaching");
            return new LaTrompaCachingConfigurationReader(node);
        }
    }

    ///<summary>
    /// Implements <c>IConfigurationSectionHandler</c>
    ///</summary>
    public class LaTrompaCachingConfigurationSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        ///<summary>
        ///Creates a configuration section handler.
        ///</summary>
        ///
        ///<returns>
        ///The created section handler object.
        ///</returns>
        ///
        ///<param name="parent">Parent object.</param>
        ///<param name="configContext">Configuration context object.</param>
        ///<param name="xmlNode">Section XML node.</param><filterpriority>2</filterpriority>
        public object Create(object parent, object configContext, XmlNode xmlNode)
        {
            return xmlNode;
        }

        #endregion
    }

    ///<summary>
    /// Reads the configuration section for the LaTrompa caching
    ///</summary>
    public class LaTrompaCachingConfigurationReader
    {
        private readonly XmlDocument _doc;

        ///<summary>
        /// Constructor
        ///</summary>
        ///<param name="node">The node from the config file from the LaTrompaCaching section</param>
        public LaTrompaCachingConfigurationReader(XmlNode node)
        {
            var reader = new XmlTextReader(node.OuterXml, XmlNodeType.Document, null);
            _doc = new XmlDocument();
            _doc.Load(reader);
        }

        ///<summary>
        /// Gets the configurations for the memory cache object
        ///</summary>
        public CachingMemoryCacheConfig MemoryCache
        {
            get
            {
                return new CachingMemoryCacheConfig(_doc.SelectSingleNode("//memoryCache"));
            }
        }

        ///<summary>
        /// Gets the configuration for the DbCache object
        ///</summary>
        public Caching_db_cache DbCache
        {
            get
            {
                return new Caching_db_cache(_doc.SelectSingleNode("//dbCache"));
            }
        }
    }

    ///<summary>
    ///Configuration options for the <see cref="DbCache"/> object
    ///</summary>
    public class Caching_db_cache : CachingType
    {
        ///<summary>
        /// Constructor
        ///</summary>
        ///<param name="node">The node that holds the configurations</param>
        public Caching_db_cache(XmlNode node)
            : base(node)
        {

        }

        ///<summary>
        /// Returns the connection string for the <see cref="DbCache"/> object
        ///</summary>
        public string ConnectionString
        {
            get
            {
                try
                {
                    return Node.SelectSingleNode("connectionString").InnerText;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        ///<summary>
        ///Returns the table name to use (defaults to SureDbCache)
        ///</summary>
        public string TableName
        {
            get
            {
                try
                {
                    return Node.SelectSingleNode("tableName").InnerText;
                }
                catch (Exception)
                {
                    return "SureDbCache";
                }
            }
        }
    }

    ///<summary>
    /// Base class for the different cache type configurations.
    ///</summary>
    public class CachingType
    {
        ///<summary>
        ///Holds the node
        ///</summary>
        protected XmlNode Node;


        ///<summary>
        ///Constructor
        ///</summary>
        ///<param name="node">The node that holds the configurations</param>
        public CachingType(XmlNode node)
        {
            Node = node;
        }

        ///<summary>
        /// Indicates if this specific cache type is enable
        ///</summary>
        public bool IsEnabled
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(Node.SelectSingleNode("enabled").InnerText);
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
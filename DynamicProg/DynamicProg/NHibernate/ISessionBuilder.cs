using NHibernate;
using NHibernate.Cfg;

namespace DynamicProg.NHibernate
{
    ///<summary>
    /// Defines the contract of a session builder
    ///</summary>
    public interface ISessionBuilder
    {
        ///<summary>
        /// Returns an <c>ISession</c>
        ///</summary>
        ///<returns>The <c>ISession</c> implementation</returns>
        ISession GetSession();


        ///<summary>
        /// Returns the <c>NHibernate.Cfg.Configuration</c> 
        ///</summary>
        ///<returns><c>NHibernate.Cfg.Configuration</c></returns>
        Configuration GetConfiguration();
    }
}
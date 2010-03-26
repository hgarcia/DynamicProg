using System.Web;

namespace DynamicProg.NHibernate
{
    public class NHibernateSessionModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.EndRequest += new System.EventHandler(context_EndRequest);
        }

        void context_EndRequest(object sender, System.EventArgs e)
        {
            var builder = new HybridSessionBuilder();
            var session = builder.GetExistingWebSession();
            if (session != null)
            {
                session.Dispose();
            }
        }

        public void Dispose()
        {

        }
    }
}
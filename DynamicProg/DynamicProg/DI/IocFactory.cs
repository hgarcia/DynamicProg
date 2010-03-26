namespace DynamicProg.DI
{
    public class IocFactory : IIocFactory
    {
        public T GetClassInstance<T>(string key)
        {
            return IocContainer.GetClassInstance<T>(key);
        }
    }
}
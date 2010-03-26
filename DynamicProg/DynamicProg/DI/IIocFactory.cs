namespace DynamicProg.DI
{
    public interface IIocFactory
    {
        T GetClassInstance<T>(string key);
    }
}
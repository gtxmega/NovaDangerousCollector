namespace Services.locator
{
    public interface IServicesLocator
    {
        T GetServices<T>();
    }
}
namespace AnimalStore.Web.Wrappers.Interfaces
{
    public interface ICustomHttpRequestWrapper
    {
        string GetQueryStringValueByKey(string key);
    }
}
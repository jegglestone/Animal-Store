using System.IO;

namespace AnimalStore.Web.Facades
{
    public interface IDataContractJsonSerializerWrapper
    {
        object ReadObject(Stream stream);
    }
}
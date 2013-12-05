using System.IO;
using System.Runtime.Serialization.Json;

namespace AnimalStore.Web.Wrappers.Interfaces
{
    public interface IDataContractJsonSerializerWrapper
    {
        object ReadObject(Stream stream, DataContractJsonSerializer dataContractJsonSerializer);
    }
}
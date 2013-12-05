using System.IO;
using System.Runtime.Serialization.Json;
using AnimalStore.Web.Wrappers.Interfaces;

namespace AnimalStore.Web.Wrappers
{
    public class DataContractJsonSerializerWrapper : IDataContractJsonSerializerWrapper
    {
        public object ReadObject(Stream stream, DataContractJsonSerializer dataContractJsonSerializer)
        {
            return dataContractJsonSerializer.ReadObject(stream);
        }
    }
}
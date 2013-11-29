using System.IO;
using System.Runtime.Serialization.Json;

namespace AnimalStore.Web.Facades
{
    public class DataContractJsonSerializerWrapper : IDataContractJsonSerializerWrapper
    {
        public object ReadObject(Stream stream, DataContractJsonSerializer dataContractJsonSerializer)
        {
            return dataContractJsonSerializer.ReadObject(stream);
        }
    }
}
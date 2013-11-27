using System.IO;
using System.Runtime.Serialization.Json;

namespace AnimalStore.Web.Facades
{
    public class DataContractJsonSerializerWrapper : IDataContractJsonSerializerWrapper
    {
        private readonly DataContractJsonSerializer _dataContractJsonSerializer;

        public DataContractJsonSerializerWrapper(DataContractJsonSerializer dataContractJsonSerializer)
        {
            _dataContractJsonSerializer = dataContractJsonSerializer;
        }

        public object ReadObject(Stream stream)
        {
            return _dataContractJsonSerializer.ReadObject(stream);
        }
    }
}
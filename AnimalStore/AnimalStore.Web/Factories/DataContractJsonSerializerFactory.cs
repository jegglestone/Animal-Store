using System;
using System.Runtime.Serialization.Json;

namespace AnimalStore.Web.Factories
{
    public static class DataContractJsonSerializerFactory
    {
        public static DataContractJsonSerializer GetDataContractJsonSerializer(Type type)
        {
            return new DataContractJsonSerializer(type);
        }
    }
}
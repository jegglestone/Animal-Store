using AnimalStore.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Driver.Builders;

namespace AnimalStore.Data.Repositories.Places
{
    public class PlacesRepository : IPlacesRepository
    {
        readonly MongoServer mongoServer;

        MongoDatabase mongoDatabase
        {
            get { return mongoServer.GetDatabase("places"); }
        }

        public PlacesRepository(MongoClient mongoClient)
        {
            mongoServer = mongoClient.GetServer();
        }

        public IEnumerable<Place> GetAll()
        {
            return mongoDatabase
                .GetCollection<Place>("places")
                .FindAllAs<Place>();
        }

        public Place GetById(int id)
        {
            var query = Query.EQ("PlacesID", id.ToString());
            return mongoDatabase
                .GetCollection<Place>("places")
                .FindOneAs<Place>(query);
        }
    }
}

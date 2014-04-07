using AnimalStore.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Driver.Builders;

namespace AnimalStore.Data.Repositories.Places
{
    public class PlacesRepository : IPlacesRepository
    {
        private readonly MongoServer mongoServer;
        private const string DATABASE_NAME = "places";
        private const string PLACES_COLLECTION = "places";

        MongoDatabase mongoDatabase
        {
            get { return mongoServer.GetDatabase(DATABASE_NAME); }
        }

        public PlacesRepository(MongoClient mongoClient)
        {
            mongoServer = mongoClient.GetServer();
        }

        public IEnumerable<Place> GetAll()
        {
            return mongoDatabase
                .GetCollection<Place>(PLACES_COLLECTION)
                .FindAllAs<Place>();
        }

        public Place GetById(int id)
        {
            var query = Query.EQ("PlacesID", id.ToString());
            return mongoDatabase
                .GetCollection<Place>(PLACES_COLLECTION)
                .FindOneAs<Place>(query);
        }
    }
}

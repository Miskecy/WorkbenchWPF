using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace WorkbenchWPF.Helpers
{
    public class MongoCRUD
    {
        private IMongoDatabase _db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            _db = client.GetDatabase(database);
        }

        public bool CreateOne<T>(string table, T data)
        {
            try
            {
                var collection = _db.GetCollection<T>(table);
                collection.InsertOne(data);
                return true;
            }
            catch
            {
                return false;
            }   
        }

        public void CreateMany<T>(string table, List<T> data)
        {
            var collection = _db.GetCollection<T>(table);
            collection.InsertMany(data);
        }

        public List<T> LoadData<T>(string table)
        {
            var collection = _db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MH.Common.DB
{
    /// <summary>
    /// MongoDB操作对象
    /// </summary>
    public class MongoDo
    {
        private MongoClient _MongoClient;

        private MongoServer _MongoServer;

        private MongoDatabase _MongoDatabase;

        public  MongoCollection<BsonDocument> MongoCollection { get; set; }

        public MongoDo(string connection, string db, string collection)
        {
            this._MongoClient = new MongoClient(connection);
            this._MongoServer = this._MongoClient.GetServer();
            this._MongoDatabase = this._MongoServer.GetDatabase(db);
            this.MongoCollection = this._MongoDatabase.GetCollection(collection);
        }
       
    }
}

using ApiEnvio.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEnvio.Context
{
    public class UserContext
    {
        private readonly IMongoDatabase _database = null;

        public UserContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Users> Usuarios
        {
            get
            {
                return _database.GetCollection<Users>("User");
            }
        }
    }
}

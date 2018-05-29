using ApiEnvio.Context;
using ApiEnvio.Interfaces;
using ApiEnvio.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEnvio.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context = null;

        public UserRepository(IOptions<Settings> settings)
        {
            _context = new UserContext(settings);
        }

        public IEnumerable<Users> GetAllUsers()
        {
            try
            {
                return _context.Usuarios.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public Users GetUser(string id)
        {
            try
            {
                ObjectId internalId = new ObjectId(id);
                return  _context.Usuarios
                                .Find(user => user.Id == id
                                        || user.InternalId == internalId)
                                .FirstOrDefault();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddUser(Users item)
        {
            try
            {
                item.InternalId = ObjectId.GenerateNewId();
                item.Id = item.InternalId.ToString();
                await _context.Usuarios.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveUser(string id)
        {
            try
            {
                var internalId = new ObjectId(id);
                DeleteResult actionResult
                    = await _context.Usuarios.DeleteOneAsync(Builders<Users>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateUser(string id, string nome)
        {
            var filter = Builders<Users>.Filter.Eq(s => s.Id, id);
            var update = Builders<Users>.Update
                            .Set(s => s.Nome, nome);

            try
            {
                UpdateResult actionResult
                    = await _context.Usuarios.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateUserDocument(string id, Users item)
        {
            try
            {
                item.InternalId = new ObjectId(id);

                var usuario= GetUser(id);
                if (usuario != null)
                {
                    ReplaceOneResult actionResult
                    = await _context.Usuarios
                                    .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });

                    return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
                }
                else return false;
                
                
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAllUsers()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Usuarios.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}

using ApiEnvio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEnvio.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAllUsers();
        Users GetUser(string id);

        // add new note document
        Task AddUser(Users item);

        // remove a single document / note
        Task<bool> RemoveUser(string id);

        // update just a single document / note
        Task<bool> UpdateUser(string id, string nome);

        // demo interface - full document update
        Task<bool> UpdateUserDocument(string id, Users item);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllUsers();
    }
}

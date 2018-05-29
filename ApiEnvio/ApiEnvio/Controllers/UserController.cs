using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEnvio.Interfaces;
using ApiEnvio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEnvio.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepository;
        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return _UserRepository.GetAllUsers();
        }

        // GET api/Users/5 - retrieves a specific User using either Id or InternalId (BSonId)
        [HttpGet("{id}")]
        public  Users Get(string id)
        {
            return  _UserRepository.GetUser(id) ?? new Users();
        }

        // POST api/Users - creates a new User
        [HttpPost]
        public void Post([FromBody] Users newUser)
        {
            _UserRepository.AddUser(new Users
            {
                Nome = newUser.Nome,
                Cpf = newUser.Cpf,
                Senha = newUser.Senha
            });
        }

        // PUT api/Users/5 - updates a specific User
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Users value)
        {
            _UserRepository.UpdateUserDocument(id,value);
        }

        // DELETE api/Users/5 - deletes a specific User
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _UserRepository.RemoveUser(id);
        }
    }
}
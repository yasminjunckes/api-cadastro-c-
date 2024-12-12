using Domain.DTOs.User;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Controllers.Users
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public IActionResult Create(UserRequestDTO request)
        {
            var userResponse = _usersService.Create(request);
            return Ok(userResponse);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UserRequestDTO request)
        {
            _usersService.Modify(id, request);
            return Ok("Usuário modificado com sucesso");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _usersService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _usersService.Remove(id);
            return Ok("Usuario removido com sucesso");
        }

        [HttpGet()]
        public IActionResult GetByParameter([FromQuery] Dictionary<string, string> model)
        {
            var users = _usersService.GetAll(x =>
            {
                bool matches = true;
                if (model.TryGetValue("name", out string name))
                {
                    matches = matches && x.Name == name;
                }
                if (model.TryGetValue("personalDocument", out string personalDocument))
                {
                    matches = matches && x.PersonalDocument == personalDocument;
                }
                if (model.TryGetValue("email", out string email))
                {
                    matches = matches && x.Email == email;
                }
                return matches;
            });

            if (users == null)
            {
                return NotFound();
            }

            var userList = new List<User>();
            foreach (var user in users)
            {
                if (user.RemovedAt == null)
                {
                    userList.Add(user);
                }
            }

            double page = Math.Ceiling(userList.Count() / 5.0);
            var result = new
            {
                users = userList.OrderBy(x => x.Name),
                total = userList.Count(),
                pages = page,
                actual = 1
            };

            return Ok(result);
        }
    }
}
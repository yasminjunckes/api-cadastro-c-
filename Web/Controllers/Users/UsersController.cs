using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Primitives;
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
        //IActionResult é mais genérico e conseguimos retornar tanto o Unauthorized, quanto o Ok.
        public IActionResult Create(UsersRequest request)
        {
            var response = _usersService.Create(
                request.Name,
                request.PersonalDocument,
                request.BirthDate,
                request.Email,
                request.Phone
                );

                if (!response.IsValid)
                {
                    return BadRequest(response.Errors);
                }

                return Ok();      
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UsersRequest request)
        {
          var modifiedUser = _usersService.GetById(id);
            if (modifiedUser == null)
            {
                return NotFound();
            }

            modifiedUser.Name = request.Name;
            modifiedUser.PersonalDocument = request.PersonalDocument;
            modifiedUser.Email = request.Email;
            modifiedUser.Phone = request.Phone;
            modifiedUser.BirthDate = request.BirthDate;

            _usersService.Modify(modifiedUser);
            return Ok("Usuario alterado com sucesso");

        }



        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _usersService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }  

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _usersService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            user.RemovedAt = DateTime.Now;
            _usersService.Modify(user);
            return Ok("Usuario " + user.Name + " removido com sucesso");
        }

        [HttpGet()]
        public IActionResult GetByParameter([FromQuery] Dictionary<string, string> model)
        {
            var user = _usersService.GetAll(x =>
            {
                bool matches = true;
                if (model.TryGetValue("name", out string name))
                {
                    matches = matches && x.Name == name;
                }
                return matches;
            });

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.OrderBy(x => x.Name));
        }
    }
}
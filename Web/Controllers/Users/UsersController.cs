using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Primitives;

namespace Web.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        //IActionResult é mais genérico e conseguimos retornar tanto o Unauthorized, quanto o Ok.
        public IActionResult Create(UserRequest request)
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

        // [HttpPut("{id}")]
        // public IActionResult UpdateUser(Guid id, [FromBody] CreateUserRequest request)
        // {
        //     StringValues userId;
        //     if (!Request.Headers.TryGetValue("UserId", out userId))
        //     {
        //         return Unauthorized();
        //     }

        //     var user = _usersService.GetById(Guid.Parse(userId));

        //     if (user == null)
        //     {
        //         return Unauthorized();
        //     }
        //     if (user.CPF != request.CPF)
        //     {
        //         return Unauthorized();
        //     }

        //     var modifiedUser = _usersService.GetById(id);
        //     if (modifiedUser == null)
        //     {
        //         return NotFound();
        //     }

        //     modifiedUser.Name = request.Name;
        //     modifiedUser.CPF = request.CPF;
        //     modifiedUser.Email = request.Email;
        //     modifiedUser.Phone = request.Phone;
        //     modifiedUser.State = request.State;
        //     modifiedUser.City = request.City;
        //     modifiedUser.District = request.District;
        //     modifiedUser.ZipCode = request.ZipCode;
        //     modifiedUser.HouseNumber = request.HouseNumber;
        //     modifiedUser.AddressComplement = request.AddressComplement;
        //     modifiedUser.Profile = request.Profile;
        //     modifiedUser.Password = request.Password;

        //     _usersService.Modify(modifiedUser);
        //     return NoContent();

        // }



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
    }
}
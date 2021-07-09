using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        private readonly IAddressesService _addressesService;
        public UsersController(IUsersService usersService, IAddressesService AddressService)
        {
            _usersService = usersService;
            _addressesService = AddressService;
        }

        [HttpPost]
        //IActionResult é mais genérico e conseguimos retornar tanto o Unauthorized, quanto o Ok.
        public IActionResult Create(UsersRequest request)
        {
            Guid userId = Guid.NewGuid();

            var userResponse = _usersService.Create(
                userId,
                request.Name,
                request.PersonalDocument,
                request.BirthDate,
                request.Email,
                request.Phone
                );

            if (!userResponse.IsValid)
            {
                return BadRequest(userResponse.Errors);
            }

            var viaCep = _addressesService.GetAddress(request.Address.PostalCode);

            var addressResponse = _addressesService.Create(
                viaCep.Line1,
                request.Address.Line2,
                request.Address.Number,
                request.Address.PostalCode,
                viaCep.City,
                viaCep.State,
                viaCep.District,
                request.Address.Principal,
                userId
                );

            IEnumerable<Address> userAddresses = _addressesService.GetAddresses(userId);
            Address firstAddress = userAddresses.First();

            //Forma alternativa de retornar o usuario com endereço

            //User user = _usersService.GetById(userId);
            //var response = new
            //{
            //    id = user.Id,
            //    name = user.Name,
            //    personalDocument = user.PersonalDocument,
            //    birthDate = user.BirthDate,
            //    email = user.Email,
            //    phone = user.Phone,
            //    addresses = userAddresses
            //};

            request.Address.Line1 = viaCep.Line1;
            request.Address.City = viaCep.City;
            request.Address.State = viaCep.State;
            request.Address.District = viaCep.District;
            request.Address.Id = firstAddress.UserId;
            request.Address.UserId = firstAddress.UserId;

            return Ok(request);

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
            return Ok(modifiedUser);

        }



        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _usersService.GetById(id);

            if (user == null || user.RemovedAt != null)
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
            var users = _usersService.GetAll(x =>
            {
                bool matches = true;
                if (model.TryGetValue("name", out string name))
                {
                    matches = matches && x.Name == name;
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
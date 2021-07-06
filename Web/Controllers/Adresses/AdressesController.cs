﻿
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers.Users;

namespace Web.Controllers.Adresses
{
    [ApiController]
    [Route("[controller]")]
    public class AdressesController : ControllerBase
    {
        private readonly IAdressesService _adressesService;
        public AdressesController(IAdressesService AdressService)
        {
            _adressesService = AdressService;
        }

        [HttpPost]
        public IActionResult Create(AdressesRequest request)
        {
            var response = _adressesService.Create(
                request.Line1,
                request.Line2,
                request.Number,
                request.PostalCode,
                request.City,
                request.State,
                request.District,
                request.Principal,
                request.UserId
                );

            return Ok();
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateUser(Guid id, [FromBody] UsersRequest request)
        //{
        //    var modifiedUser = _usersService.GetById(id);
        //    if (modifiedUser == null)
        //    {
        //        return NotFound();
        //    }

        //    modifiedUser.Name = request.Name;
        //    modifiedUser.PersonalDocument = request.PersonalDocument;
        //    modifiedUser.Email = request.Email;
        //    modifiedUser.Phone = request.Phone;
        //    modifiedUser.BirthDate = request.BirthDate;

        //    _usersService.Modify(modifiedUser);
        //    return Ok("Usuario alterado com sucesso");

        //}



        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var adress = _adressesService.GetById(id);

            if (adress == null)
            {
                return NotFound();
            }

            return Ok(adress);
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    var user = _usersService.GetById(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    user.RemovedAt = DateTime.Now;
        //    _usersService.Modify(user);
        //    return Ok("Usuario " + user.Name + " removido com sucesso");
        //}

        //[HttpGet()]
        //public IActionResult GetByParameter([FromQuery] Dictionary<string, string> model)
        //{
        //    var user = _usersService.GetAll(x =>
        //    {
        //        bool matches = true;
        //        if (model.TryGetValue("name", out string name))
        //        {
        //            matches = matches && x.Name == name;
        //        }
        //        return matches;
        //    });

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user.OrderBy(x => x.Name));
        //}
    }
}

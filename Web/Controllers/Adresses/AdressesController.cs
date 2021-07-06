
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
            var viaCep = _adressesService.GetAdress(request.PostalCode);

            var response = _adressesService.Create(
                viaCep.Line1,
                request.Line2,
                request.Number,
                request.PostalCode,
                viaCep.City,
                viaCep.State,
                viaCep.District,
                request.Principal,
                request.UserId
                );

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            var adress = _adressesService.GetAdresses(id);

            if (adress == null)
            {
                return NotFound();
            }

            return Ok(adress);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _adressesService.Delete(id);
            return Ok("Endereço deletado com sucesso.");
        }

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


using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers.Users;

namespace Web.Controllers.Addresses
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressesService _addressesService;
        public AddressesController(IAddressesService AddressService)
        {
            _addressesService = AddressService;
        }

        [HttpPost]
        public IActionResult Create(AddressesRequest request)
        {
            var viaCep = _addressesService.GetAddress(request.PostalCode);

            var response = _addressesService.Create(
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
            var address = _addressesService.GetAddresses(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _addressesService.Delete(id);
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

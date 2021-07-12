using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
            Guid addressId = Guid.NewGuid();
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

            request.Id = addressId;
            request.Line1 = viaCep.Line1;
            request.City = viaCep.City;
            request.State = viaCep.State;
            request.District = viaCep.District;

            return Ok(request);
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
    }
}

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

        [HttpPost("{userId}")]
        public IActionResult Create(Guid userId, [FromBody] AddressesRequest request)
        {
            Guid addressId = Guid.NewGuid();

            if (request.Principal == true)
            {
                var addressCheck = _addressesService.GetAddresses(userId);

                foreach (var item in addressCheck)
                {
                    if (item.Principal == true)
                    {
                        return BadRequest("Já existe um endereço principal.");
                    }
                }
            }

            var viaCep = _addressesService.GetAddress(request.PostalCode);

            if (viaCep.City == null)
            {
                return BadRequest("Cep inválido");
            }

            var response = _addressesService.Create(
                viaCep.Line1,
                request.Line2,
                request.Number,
                request.PostalCode,
                viaCep.City,
                viaCep.State,
                viaCep.District,
                request.Principal,
                userId
                );

            var address = _addressesService.GetById(response.Id);

            return Ok(address);
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

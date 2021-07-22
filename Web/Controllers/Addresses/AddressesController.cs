using Domain.DTOs.Address;
using Domain.Interfaces;
using Domain.Requests;
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
        public IActionResult Create(Guid userId, [FromBody] AddressRequestDTO request)
        {
            var viaCep = _addressesService.GetAddress(request.PostalCode);
            if (viaCep == null)
            {
                return BadRequest("Cep inválido");
            }

            var response = _addressesService.Create(request, userId);

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

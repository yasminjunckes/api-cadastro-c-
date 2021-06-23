using System;
using Domain.Entities;
using Domain.DTO;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Services
{
    public class AdressesService : IAdressesService
    {
        private readonly IAdressesRepository _adressesRepository;

        public AdressesService(IAdressesRepository adressesRepository)
        {
            _adressesRepository = adressesRepository;
        }

        public AdressDTO Create(
            string line1,
            string line2,
            int number,
            string postalCode,
            string city,
            string state,
            string district,
            bool principal,
            Guid adressId
        )
        {
            var adress = new Adress(line1, line2, number, postalCode, city, state, district, principal, adressId);
            // TODO = validação de endereço
            //
            // var adressValidation = adress.Validate();

            // if (adressValidation.isValid)
            // {
            //     _adressesRepository.Add(adress);
            //     return new adressDTO(adress.Id);
            // }
            // return new adressDTO(adressValidation.errors);

             return new AdressDTO(adress.Id);
        }

        public Adress GetById(Guid id)
        {
            return _adressesRepository.Get(id);
        }

        // public void Modify(Adress adress)
        // {
        //     _adressesRepository.Modify(adress);
        // }

        // public IEnumerable<Adress> GetAll(Func<Adress, bool> predicate)
        // {
        //     return _adressesRepository.GetAll(predicate);
        // }
    }
}
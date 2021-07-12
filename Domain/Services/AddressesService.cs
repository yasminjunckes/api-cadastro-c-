using System;
using Domain.Entities;
using Domain.DTO;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Domain.Services
{
    public class AddressesService : IAddressesService
    {
        private readonly IAddressesRepository _addressesRepository;

        public AddressesService(IAddressesRepository addressesRepository)
        {
            _addressesRepository = addressesRepository;
        }

        public AddressDTO Create(
            string line1,
            string line2,
            int number,
            string postalCode,
            string city,
            string state,
            string district,
            bool principal,
            Guid addressId
        )
        {
            var address = new Address(line1, line2, number, postalCode, city, state, district, principal, addressId);
            // TODO = validação de endereço
            //
            // var addressValidation = address.Validate();

            // if (addressValidation.isValid)
            // {
            //     _addressesRepository.Add(address);
            //     return new addressDTO(address.Id);
            // }
            // return new addressDTO(addressValidation.errors);

            _addressesRepository.Add(address);
            return new AddressDTO(address.Id);
        }

        public Address GetAddress(string postaslCode)
        {
           WebRequest request = WebRequest.Create("https://viacep.com.br/ws/" + postaslCode + "/json/");

           request.Credentials = CredentialCache.DefaultCredentials;
           HttpWebResponse response = (HttpWebResponse)request.GetResponse();

           Stream dataStream = response.GetResponseStream();
           StreamReader reader = new StreamReader(dataStream);
           string responseFromServer = reader.ReadToEnd();

           reader.Close();
           dataStream.Close();
           response.Close();

           JObject json = JObject.Parse(responseFromServer);

           string line1 = (string)json.GetValue("logradouro");
           string city = (string)json.GetValue("localidade");
           string state = (string)json.GetValue("uf");
           string district = (string)json.GetValue("bairro");

           Address address = new Address(line1, postaslCode, city, state, district);

           return address;        
        }
            
        public Address GetById(Guid id)
        {
            return _addressesRepository.Get(id);
        }

        public IEnumerable<Address> GetAddresses(Guid userId)
        {
            return _addressesRepository.GetByUserId(userId);
        }

        public void Delete(Guid id)
        {
            var deletedAddress = _addressesRepository.Get(id);
            _addressesRepository.Remove(deletedAddress);
        }

        // public IEnumerable<Address> GetAll(Func<Address, bool> predicate)
        // {
        //     return _addressesRepository.GetAll(predicate);
        // }
    }
}
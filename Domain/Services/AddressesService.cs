using Domain.DTO;
using Domain.DTOs.Address;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Requests;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Domain.Services
{
    public class AddressesService : IAddressesService
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IUsersRepository _usersRepository;

        public AddressesService(IUsersRepository usersRepository, IAddressesRepository addressesRepository)
        {
            _usersRepository = usersRepository;
            _addressesRepository = addressesRepository;
        }

        public AddressDTO Create(AddressRequestDTO request, Guid userId)
        {
            var user = _usersRepository.Get(userId);
            if(user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            bool newPrincipal = request.Principal;
            if (request.Principal == true)
            {
                var addressCheck = GetAddresses(userId);
                foreach (var item in addressCheck)
                {
                    if (item.Principal == true)
                    {
                        newPrincipal = false;
                    }
                }
            }

            var viaCep = GetAddress(request.PostalCode);
            var address = new Address(
                viaCep.Line1, 
                request.Line2, 
                request.Number, 
                request.PostalCode, 
                viaCep.City, 
                viaCep.State, 
                viaCep.District, 
                newPrincipal, 
                userId
                );

            _addressesRepository.Add(address);

            return new AddressDTO(address.Id);
        }

        public bool PostalCodeValidator(string cep)
        {
            cep.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(".", "");
            Regex Rgx = new Regex(@"^\d{8}$");

            if (!Rgx.IsMatch(cep) || cep == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Address GetAddress(string postalCode)
        {
            if (PostalCodeValidator(postalCode) == false)
            {
                throw new Exception("Cep inválido");
            }

            WebRequest request = WebRequest.Create("https://viacep.com.br/ws/" + postalCode + "/json/");

            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            JObject json = JObject.Parse(responseFromServer);

            if (json.ContainsKey("erro")) {
                throw new Exception("Cep inválido");
            }    
            
            string line1 = (string)json.GetValue("logradouro");
            string city = (string)json.GetValue("localidade");
            string state = (string)json.GetValue("uf");
            string district = (string)json.GetValue("bairro");

            Address address = new Address(line1, postalCode, city, state, district);

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
    }
}
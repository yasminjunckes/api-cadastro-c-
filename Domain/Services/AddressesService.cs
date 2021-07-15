using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
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
            Guid userId
        )
        {
            bool newPrincipal = principal;
            if (principal == true)
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
            var address = new Address(line1, line2, number, postalCode, city, state, district, newPrincipal, userId);

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
                Address invalidAddress = null;
                return (invalidAddress);
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
            try
            {
                if((bool)json.GetValue("erro") == true)
                {
                    Address errorAddress = null;
                    return(errorAddress);
                }
            }
            catch(SystemException)
            {}
            
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
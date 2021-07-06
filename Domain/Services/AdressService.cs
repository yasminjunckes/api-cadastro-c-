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

            _adressesRepository.Add(adress);
            return new AdressDTO(adress.Id);
        }

        public Adress GetAdress(string postaslCode)
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

           Adress adress = new Adress(line1, postaslCode, city, state, district);

           return adress;        
        }
            
        public Adress GetById(Guid id)
        {
            return _adressesRepository.Get(id);
        }

        public IEnumerable<Adress> GetAdresses(Guid userId)
        {
            return _adressesRepository.GetByUserId(userId);
        }

        public void Delete(Guid id)
        {
            var deletedAdress = _adressesRepository.Get(id);
            _adressesRepository.Remove(deletedAdress);
        }

        // public IEnumerable<Adress> GetAll(Func<Adress, bool> predicate)
        // {
        //     return _adressesRepository.GetAll(predicate);
        // }
    }
}
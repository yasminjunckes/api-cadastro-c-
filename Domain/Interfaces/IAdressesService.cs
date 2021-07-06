using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAdressesService
    {
        AdressDTO Create(
            string line1,
            string line2,
            int number,
            string postalCode,
            string city,
            string state,
            string district,
            bool principal,
            Guid userId
        );

        Adress GetById(Guid id);

        Adress GetAdress(string postalCode);

        void Delete(Guid id);
    }
}
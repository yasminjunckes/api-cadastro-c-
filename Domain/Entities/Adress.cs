using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Adress : Entity
    {
        public string Line1 { get; protected set; } 
        public string Line2 { get; protected set; } 
        public int Number { get; protected set; } //obrigatorio
        public string PostalCode { get; protected set; } //obrigatorio
        public string City { get; protected set; } 
        public string State { get; protected set; }
        public string District {get; protected set; }
        public bool Principal {get; protected set; } // obrigatorio (validar se apenas um é o principal)
        public Guid UserId { get; protected set; }
        public virtual User User { get; protected set;}

        public Adress(string line1, string line2, int number, string postalCode, string city, string state, string district, bool principal, Guid userId)
        {
            Id = Guid.NewGuid();
            Line1 = line2;
            Line2 = line2;
            Number = number;
            PostalCode = postalCode;
            City = city;
            State = state;
            District = district;
            Principal = principal;
            UserId = userId;
        }
    }
}

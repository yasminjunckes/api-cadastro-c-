using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; } //Validar
        public string PersonalDocument { get; set; } //Validar
        public string BirthDate { get; set; }
        public string Email { get; set; } //Validar
        public string Phone { get; set; } //Validar
        public DateTime? RemovedAt {get; set; } = null;
        
        // public list<Adress> Adresses { get; protected set;}  ---> Incluir no construtor depois

        public User(string name, string personalDocument, string birthDate, string email, string phone)
        {
            Id = Guid.NewGuid();
            Name = name;
            PersonalDocument = personalDocument;
            BirthDate = birthDate;
            Email = email;
            Phone = phone;
        }

    }
}

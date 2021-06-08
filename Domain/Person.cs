using System;
using System.Collections.Generic;

namespace Domain
{
    public class Person : Entity
    {
        public string Name { get; protected set; } //Validar
        public string PersonalDocument { get; protected set; } //Validar
        public string BirthDate { get; protected set; }
        public string Email { get; protected set; } //Validar
        public string Phone { get; protected set; } //Validar
        public DateTime? RemovedAt {get; protected set; } = null;
        
        // public list<Adress> Adresses { get; protected set;}  ---> Incluir no construtor depois

        public Person(string name, string personalDocument, string birthDate, string email, string phone, DateTime removedAt, Guid id) : base(id)
        {
            Name = name;
            PersonalDocument = personalDocument;
            BirthDate = birthDate;
            Email = email;
            Phone = phone;
            RemovedAt = removedAt;
        }
    }
}

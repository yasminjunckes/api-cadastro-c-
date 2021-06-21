using System;
using Domain.Entities;

namespace Web.Controllers.Users
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string PersonalDocument { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? RemovedAt {get; set; } = null;
        
        // public list<Adress> Adresses { get; protected set;}  ---> Incluir no construtor depois
    }
}

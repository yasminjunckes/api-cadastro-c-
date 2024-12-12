using Domain.DTOs.Address;
using System;
using System.Collections.Generic;

namespace Domain.DTOs.User
{
    public abstract class UserBaseDTO
    {
        public string Name { get; set; }
        public string PersonalDocument { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? RemovedAt { get; set; } = null;
        public List<AddressRequestDTO> Address { get; set; }
    }
}

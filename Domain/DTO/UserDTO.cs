using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class UserDTO
    {
        public Guid Id{ get; private set; }
        public IList<string> Errors{ get; set; }
        public bool IsValid { get; set; }

        public UserDTO(Guid id)
        {
            Id = id;
            IsValid = true;
        }

        public UserDTO(IList<string> errors)
        {
            Errors = errors;
            IsValid = false;
        }
    }
}
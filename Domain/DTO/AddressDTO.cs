using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class AddressDTO
    {
        public Guid Id{ get; private set; }
        public IList<string> Errors{ get; set; }
        public bool IsValid { get; set; }

        public AddressDTO(Guid id)
        {
            Id = id;
            IsValid = true;
        }

        public AddressDTO(IList<string> errors)
        {
            Errors = errors;
            IsValid = false;
        }
    }
}
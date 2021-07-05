using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class AdressDTO
    {
        public Guid Id{ get; private set; }
        public IList<string> Errors{ get; set; }
        public bool IsValid { get; set; }

        public AdressDTO(Guid id)
        {
            Id = id;
            IsValid = true;
        }

        public AdressDTO(IList<string> errors)
        {
            Errors = errors;
            IsValid = false;
        }
    }
}
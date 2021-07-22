using System;

namespace Domain.DTOs.Address
{
    public class AddressBaseDTO
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public int Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public bool Principal { get; set; }
        public Guid UserId { get; set; }
    }
}

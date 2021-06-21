using System;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUsersService
    {
        UserDTO Create(
            string name,
            string personalDocument,
            string birthDate,
            string email,
            string phone
        );

        User GetById(Guid id);

        void Modify(User user);
    }
}
using Domain.DTO;
using Domain.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IUsersService
    {
        UserDTO Create(UserRequestDTO request);

        User GetById(Guid id);

        void Modify(User user);

        IEnumerable<User> GetAll(Func<User, bool> predicate);
    }
}
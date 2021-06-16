using System;
using Domain.Entities;
using Domain.DTO;
using Domain.Interfaces;

namespace Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public UserDTO Create(
            string name,
            string personalDocument,
            string birthDate,
            string email,
            string phone
        )
        {
            var user = new User(name, personalDocument, birthDate, email, phone);
            var userValidation = user.Validate();

            if (userValidation.isValid)
            {
                _usersRepository.Add(user);
                return new UserDTO(user.Id);
            }
            return new UserDTO(userValidation.errors);
        }

        public User GetById(Guid id)
        {
            return _usersRepository.Get(id);
        }

        public void Modify(User user)
        {
            _usersRepository.Modify(user);
        }
    }
}
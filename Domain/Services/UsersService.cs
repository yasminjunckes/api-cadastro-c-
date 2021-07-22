using Domain.DTO;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IAddressesService _addressesService;
        public UsersService(IUsersRepository usersRepository, IAddressesService AddressService)
        {
            _usersRepository = usersRepository;
            _addressesService = AddressService;
        }

        public UserDTO Create(UserRequestDTO request)
        {
            Guid id = Guid.NewGuid();
            var user = new User(
                id,
                request.Name,
                request.PersonalDocument,
                request.BirthDate,
                request.Email,
                request.Phone
                );

            var userValidation = user.Validate();

            if (!userValidation.isValid)
            {
                throw new Exception(userValidation.errors.ToString());
            }

            _usersRepository.Add(user);

            if (request.Address != null)
            {
                foreach (var item in request.Address)
                {
                    _addressesService.Create(item, id);
                }
            }
            return new UserDTO(user.Id);
        }

        public User GetById(Guid id)
        {
            return _usersRepository.Get(id);
        }

        public void Modify(User user)
        {
            _usersRepository.Modify(user);
        }

        public IEnumerable<User> GetAll(Func<User, bool> predicate)
        {
            return _usersRepository.GetAll(predicate);
        }
    }
}
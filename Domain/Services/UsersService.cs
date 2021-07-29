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
            User user =  _usersRepository.Get(id);
            if (user == null || user.RemovedAt != null)
            {
                throw new Exception("Usuário não encontrado");
            }
            return user;
        }

        public void Modify(Guid id, UserRequestDTO userRequest)
        {
            User user = _usersRepository.Get(id);
            if(user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            user.Name = userRequest.Name;
            user.PersonalDocument = userRequest.PersonalDocument;
            user.Email = userRequest.Email;
            user.Phone = userRequest.Phone;
            user.BirthDate = userRequest.BirthDate;

            _usersRepository.Modify(user);
        }

        public IEnumerable<User> GetAll(Func<User, bool> predicate)
        {
            return _usersRepository.GetAll(predicate);
        }

        public void Remove(Guid id)
        {
            var user = _usersRepository.Get(id);
            if (user == null || user.RemovedAt != null)
            {
                throw new Exception("Usuário não encontrado");
            }

            user.RemovedAt = DateTime.Now;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.DTO;
using VisioConference.Repository;
using VisioConference.Repository.DAO;

namespace VisioConference.Service
{
    public class UsersService
    {
        private UserDAO repo = new UserDAO();
        internal UserDTO findByEmailAndPassword(UserDTO dto)
        {
            UserDTO user = new UserDTO();
            user = repo.findByEmailAndPassword(dto);
            return user;
        }

        public List<UserDTO> findAll()
        {
            return repo.findAll();
        }

        internal void Add(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> findAllConnected()
        {
            return repo.findAllConnected();
        }

        public void Update(UserDTO dto)
        {
            repo.Update(dto);
        }
    }
}
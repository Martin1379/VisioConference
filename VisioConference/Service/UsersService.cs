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
        public  UserDTO findByEmailAndPassword(UserDTO dto)
        {
            UserDTO user = new UserDTO();
            user = repo.findByEmailAndPassword(dto);
            return user;
        }
        public UserDTO findById(int? id)
        {
            UserDTO user = new UserDTO();
            user = repo.findById(id);
            return user;
        }
        public List<UserDTO> findAll()
        {
            return repo.findAll();
        }
        public List<UserDTO> findAll(string search)
        {
            return repo.findAll(search);
        }

        public  void Add(UserDTO userDTO)
        {
            repo.Add(userDTO);
        }

        public List<UserDTO> findAllConnected()
        {
            return repo.findAllConnected();
        }

        public void Update(UserDTO dto)
        {
            repo.Update(dto);
        }

        internal void DeleteUserDTO(int id)
        {
            repo.DeleteUserDTO(id);
        }
    }
}
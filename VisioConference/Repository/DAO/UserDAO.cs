using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.DTO;
using VisioConference.Repository.Interface;
using VisioConference.Models;
using VisioConference.Tools;
using System.IO;
using System.Web.Mvc;
using System.Windows.Forms;


namespace VisioConference.Repository.DAO
{
    public class UserDAO : IUser
    {
        public List<UserDTO> findAll()
        {
            using (MyContext context = new MyContext())
            {
                List<UserDTO> lst = new List<UserDTO>();
                var query = context.users;
                foreach (var item in query)
                {
                    lst.Add(Convertisseur.UserDTOFromUser(new UserDTO(), item));
                }
                return lst;
            }
        }

        public List<UserDTO> findAll(string search)
        {
            using (MyContext context = new MyContext())
            {
                List<UserDTO> lst = new List<UserDTO>();
                var query = from us in context.users
                            where (us.Pseudo.Contains(search) || us.Email.Contains(search))
                            select us;
                foreach (var item in query)
                {
                    lst.Add(Convertisseur.UserDTOFromUser(new UserDTO(), item));
                }
                return lst;
            }
        }

        public UserDTO findById(int? Id)
        {
            if (Id != null)
            {
                using (MyContext context = new MyContext())
                {
                    UserDTO dto = Convertisseur.UserDTOFromUser(new UserDTO(), context.users.Find(Id));
                    return dto;
                }
            }
            else
            {
                return null;
            }
        }
        public UserDTO findByEmailAndPassword(UserDTO dto)
        {
            UserDTO userDTO = new UserDTO();
            using (MyContext context = new MyContext())
            {
                List<User> lst = context.users.ToList();
                User user = context.users.Where(u => u.Email.Equals(dto.Email) && u.Password.Equals(dto.Password)).FirstOrDefault();
                if (user != null && user.Id != 0)
                {
                    userDTO = Convertisseur.UserDTOFromUser(userDTO, user);
                    return userDTO;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Add(UserDTO userDTO)
        {
            User u = Convertisseur.UserFromUserDTO(userDTO, new User());
            using (MyContext context = new MyContext())
            {
                context.users.Add(u);
                context.SaveChanges();
            }
        }

        public void DeleteUserDTO(int id)
        {
            using (MyContext context = new MyContext())
            {
                User u = context.users.Find(id);
                context.users.Remove(u);
                context.SaveChanges();
            }
        }

        public void Update(UserDTO dto)
        {
            using (MyContext context = new MyContext())
            {
                User u = context.users.Find(dto.Id); 
                u = Convertisseur.UserFromUserDTO(dto, u); 
                context.SaveChanges();
            }
        }

       
    }
}
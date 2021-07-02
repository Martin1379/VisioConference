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
                
                // <=> SELECT * FROM utilisateur
                var query = context.users;
                foreach (var item in query)
                {
                    lst.Add(Convertisseur.UserDTOFromUser(new UserDTO(), item));
                }
                return lst;
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

        public List<UserDTO> findAllConnected()
        {
            using (MyContext context = new MyContext())
            {
                List<UserDTO> lst = new List<UserDTO>();
                UserDTO dto = new UserDTO();
                //List<Utilisateur> utilisateurs = context.utilisateurs.Include(u => u.login).ToList();
                var query = context.users;
                foreach (var item in query)
                {
                    if (item.Connected == true)
                    {
                        dto = Convertisseur.UserDTOFromUser(dto, item);
                        lst.Add(dto);
                    }
                }
                return lst;
            }
        }

        internal void DeleteUserDTO(int id)
        {
            using (MyContext context = new MyContext())
            {
                User u = context.users.Find(id);
                context.users.Remove(u);
                context.SaveChanges();
            }
        }

        public UserDTO findByEmailAndPassword(UserDTO dto)
        {
            UserDTO userDTO = new UserDTO();
            using (MyContext context = new MyContext())
            {
                //à modifier, findby id actuellement
                /*User user = new User();*/
                /* var query = from u in context.users
                             where ((u.Email == dto.Email) && (u.Password == dto.Password))
                             select u;


                 user = query.FirstOrDefault();*/
                List<User> lst = context.users.ToList();
                User user = context.users.Where(u => u.Email.Equals(dto.Email) && u.Password.Equals(dto.Password)).FirstOrDefault();
                if (user != null && user.Id != 0)
                {
                    userDTO = Convertisseur.UserDTOFromUser(userDTO, user);
                    return userDTO;
                } else
                {
                    throw new NotImplementedException();
                }
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

        public UserDTO findById(int? Id)
        {
            if (Id != null) 
            { 
                using (MyContext context = new MyContext())
                {
                    UserDTO dto = Convertisseur.UserDTOFromUser(new UserDTO(),context.users.Find(Id));
                    return dto;
                }
            }
            throw new NotImplementedException();
        }
    }
}
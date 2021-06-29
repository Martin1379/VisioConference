using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.DTO;
using VisioConference.Repository.Interface;
using VisioConference.Repository.Objets;
using VisioConference.Tools;

namespace VisioConference.Repository.DAO
{
    public class UserDAO : IUser
    {
        public List<UserDTO> findAll()
        {
            using (MyContext context = new MyContext())
            {
                List<UserDTO> lst = new List<UserDTO>();
                UserDTO dto = new UserDTO();

                // <=> SELECT * FROM utilisateur
                var query = context.users;
                foreach (var item in query)
                {
                    dto = Convertisseur.UserDTOFromUser(dto, item);
                    lst.Add(dto);
                }
                return lst;
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

        public UserDTO findByUserNameAndPassword(UserDTO dto)
        {
            UserDTO userDTO = new UserDTO();
            using (MyContext context = new MyContext())
            {

                User user = new User();
                var query = from u in context.users
                            where u.Id == dto.Id
                            select u;
                user = query.FirstOrDefault();
                if (user != null && user.Id != 0)
                {
                    userDTO = Convertisseur.UserDTOFromUser(userDTO, user);
                }

                return userDTO;

            }
        }

        public void Update()
        {
            using (MyContext context = new MyContext())
            {
                context.SaveChanges();
            }
        }
    }
}
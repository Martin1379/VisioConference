using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisioConference.DTO;
using VisioConference.Models;

namespace VisioConference.Repository.Interface
{
    public interface IUser
    {
        List<UserDTO> findAll();
        List<UserDTO> findAll(string search);
        UserDTO findById(int? Id);
        UserDTO findByEmailAndPassword(UserDTO dto);
        void Add(UserDTO userDTO);
        void DeleteUserDTO(int id);
        void Update(UserDTO dto);
        
    }
}
//Camel case (méthodes): nom composés : commencer par une minuscule
//Pascal case(classes - interfaces): nom composés : commencer par une majuscule

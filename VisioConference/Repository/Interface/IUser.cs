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
        UserDTO findById(int? Id);
        List<UserDTO> findAllConnected();
        void Update(UserDTO dto);
        UserDTO findByEmailAndPassword(UserDTO dto);
    }
}
//Camel case (méthodes): nom composés : commencer par une minuscule
//Pascal case(classes - interfaces): nom composés : commencer par une majuscule

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisioConference.Repository.Objets;

namespace VisioConference.Repository.Interface
{
    public interface IUser
    {
        List<User> findAll();
        void Update();
        User findByLogin(string login);
    }
}
//Camel case (méthodes): nom composés : commencer par une minuscule
//Pascal case(classes - interfaces): nom composés : commencer par une majuscule

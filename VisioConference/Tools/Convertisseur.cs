using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.DTO;
using VisioConference.Models;

namespace VisioConference.Tools
{
    public class Convertisseur
    {
        public static UserDTO UserDTOFromUser(UserDTO dto, User model)
        {
            dto.Id = model.Id;
            dto.Pseudo = model.Pseudo;
            dto.Password = model.Password;
            dto.ResetPassewordCode = model.ResetPassewordCode;
            dto.Email = model.Email;
            dto.Photo = model.Photo;
            dto.Connected = model.Connected;
            dto.IsAdmin = model.IsAdmin;

            return dto;
        }

        public static User UserFromUserDTO(UserDTO dto, User model)
        {
            model.Id = dto.Id;
            model.Pseudo = dto.Pseudo;
            model.Password = dto.Password;
            //model.ResetPassewordCode = dto.ResetPassewordCode;
            model.ResetPassewordCode = "test";
            model.Email = dto.Email;
            model.Photo = dto.Photo;
            model.Connected = dto.Connected;
            model.IsAdmin = dto.IsAdmin;

            return model;
        }

        public static ConversationDTO ConvDTOFromConv(ConversationDTO dto, Conversation model)
        {
            dto.convID = model.convID;
            dto.message = model.message;
            dto.userFriendID = model.userFriendID;
            dto.userID = model.userID;


            return dto;
        }




        public static Conversation ConvFromConvDTO(ConversationDTO dto, Conversation model)
        {
            model.convID = dto.convID;
            model.message = dto.message;
            model.userFriendID = dto.userFriendID;
            model.userID = dto.userID;

            return model;
        }
    }

}
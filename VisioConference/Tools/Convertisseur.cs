﻿using System;
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
            model.Email = dto.Email;
            model.Photo = dto.Photo;
            model.Connected = dto.Connected;
            model.IsAdmin = dto.IsAdmin;

            return model;
        }
    }

}
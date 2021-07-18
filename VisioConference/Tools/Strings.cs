using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisioConference.DTO;

namespace VisioConference.Tools
{
    public class Strings
    {


        // récupère la premier élément d'une chaine compris entre string strStart et StrEnd
        // str chaine ="zeazea<#Remy2>Salut !<#Sara3> Ca va ? \n bien ou quoi <#Remy> bien bien \n deuxieme message"
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            //strStart : <#Sara>
            //strEnd : <#Remy>
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        private static string getMessage(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
        private static string getMessage(string strSource, string strStart)
        {
            if (strSource.Contains(strStart))
            {
                int Start;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                return strSource.Substring(Start, strSource.Length - Start);
            }

            return "";
        }
        private static string hashMessage(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(End, strSource.Length - (End));

            }

            return "";
        }

        private static string getFirstSender(string strSource)
        {
            return getBetween(strSource, "<#", ">");
        }

        private static string getSecondSender(string strSource)
        {
            try
            {
                return getBetween(strSource.Substring(getFirstSender(strSource).Length, strSource.Length - getFirstSender(strSource).Length), "<#", ">");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IEnumerable<string> afficherConv(string conversation, UserDTO userDTO, UserDTO amiDTO)
        {
            string login;
            string login2;
            string pseudo;
            string message;
            do
            {
                login = getFirstSender(conversation);
                login2 = getSecondSender(conversation);
                if (int.Parse(login) == userDTO.Id)
                    pseudo = userDTO.Pseudo;
                else
                    pseudo = amiDTO.Pseudo;

                if (login2 == "") // Si on est arrivé au dernier utilisateur
                {
                    message = "<strong>" + pseudo + ": " + "</strong>" + getMessage(conversation, "<#"+login+">");

                }
                else
                {
                    message = "<strong>" + pseudo + ": " + "</strong>" + getMessage(conversation, "<#"+login+">", "<#" + login2 + ">");
                    conversation = hashMessage(conversation, "<#" + login + ">", "<#" + login2 + ">");
                }

                yield return message;

            } while ("<#" + login2 + ">" != "<#>" || login2 == null);

            yield break;
        }


    }
}
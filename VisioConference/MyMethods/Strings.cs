using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisioConference.MyMethods
{
    public class Strings
    {
        // récupère la premier élément d'une chaine compris entre string strStart et StrEnd
        public static string getBetween(string strSource, string strStart, string strEnd)
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

        public static string getMessage(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);

                return getBetween(strSource, "<#", ">") + " :" + strSource.Substring(Start, End - Start);
            }

            return "";
        }
        public static string getMessage(string strSource, string strStart)
        {
            if (strSource.Contains(strStart))
            {
                int Start;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;

                return getBetween(strSource, "<#", ">") + " :" + strSource.Substring(Start, strSource.Length - Start);
            }

            return "";
        }
        public static string hashMessage(string strSource, string strStart, string strEnd)
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

        public static string getFirstSender(string strSource)
        {
            return "<#" + getBetween(strSource, "<#", ">") + ">";
        }

        public static string getSecondSender(string strSource)
        {
            try
            {
                return "<#" + getBetween(strSource.Substring(getFirstSender(strSource).Length, strSource.Length - getFirstSender(strSource).Length), "<#", ">") + ">";
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        public static void afficherConv (string conversation)
        {
            string login;
            string login2;
            string message;
            do
            {
                login = getFirstSender(conversation);
                login2 = getSecondSender(conversation);
                if (login2 == "<#>") // Si on est arrivé au dernier utilisateur
                {
                    message = getMessage(conversation, login);

                }
                else
                {
                    message = getMessage(conversation, login, login2);
                    conversation = hashMessage(conversation, login, login2);
                }

                Console.WriteLine(message);

            } while (login2 != "<#>");

        }


    }
}
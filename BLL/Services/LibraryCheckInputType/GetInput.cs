using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services.LibraryCheckInputType
{
    public static class GetInput
    {
        public static class GetNumber
        {
            // get int type
            public static bool GetIntType(string txtString)
            {
                int input = 0;
                try
                {
                    input = int.Parse(txtString);
                }
                catch
                {
                    return false;
                }
                return true;

            }
            // get float type
            public static bool GetFloatType(string txtString)
            {
                float input = 0;
                try
                {
                    input = float.Parse(txtString);
                }
                catch
                {
                    return false;
                }
                return true;
            }
            // get double type
            public static bool GetDoubleType(string txtString)
            {
                double input = 0;
                try
                {
                    input = double.Parse(txtString);
                }
                catch
                {
                    return false;
                }
                return true;

            }
        }


        // for string
        public static class GetString
        {
            static char[] Invalid_Char_For_Name = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '!', '@', '#', '$', '%',
            '^', '&', '*', '(', ')', '_', '+', '=', '~', '`', ',', '.', '<', '>', '?', '/', '|', '\\', '\'', '"', ':', ';', '}', ']', '{', '[' };
            static char[] Invalid_Char_For_Email = { '!', '#', '$', '%', '^', '&'
                , '*', '(', ')', '_', '+', '-', '=', 'Ơ', '}', '|'
                , ':', '"', '>', '?', '[', ']', '\\',';','\'','/'};
            static char[] Invalid_Char_For_Address = {'!', '#', '$', '%', '^', '&'
                , '*', '(', ')', '_', '+', '-', '=', 'Ơ', '}', '|'
                , ':', '"', '>', '?', '[', ']', '\\',';','\'','@','.'};
            static char[] Invalid_Char_For_ID = {  '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=', '~',
                '`', ',', '.', '<', '>', '?', '/', '|', '\\', '\'', '"', ':', ';', '}', ']', '{', '[' };
            // get valid email
            public static bool GetAddress(string txtString)
            {
                if (txtString.IndexOfAny(Invalid_Char_For_Address) >= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            // get valid name
            public static bool GetName(string? txtString)
            {
                if (txtString?.IndexOfAny(Invalid_Char_For_Name) >= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            // delete space
            public static string ReplaceSpaces(string input)
            {
                // Sử dụng biểu thức chính quy để thay thế các khoảng trắng dư thừa
                return Regex.Replace(input, @"\s+", " ");
            }
            // check email
            public static bool CheckEmail(string email)
            {
                int length = email.Length;
                int indexOfAt = email.IndexOf('@');
                int indexofDot = email.IndexOf('.');
                // Checks Email string
                if (length == 0) return false;
                if (email.Length > 100) return false;
                if (email.Contains(' ') == true) return false;
                if (email.ToCharArray().Count(c => c == '@') > 1) return false;
                if (indexOfAt == -1 || indexOfAt < 5 || indexOfAt > length - 5) return false;
                if (indexofDot == -1 || indexofDot == 1 || indexofDot == length - 1) return false;
                if (email.LastIndexOf(".") == length - 1) return false;
                if (email.IndexOfAny(Invalid_Char_For_Email) != -1) return false;
                if (email.IndexOf("..") != -1) return false;
                if (email.IndexOf(".@") != -1 || email.IndexOf("@.") != -1) return false;
                if (email.Substring(email.IndexOf('@')).IndexOf('.') == 1) return false;
                if (email.Substring(email.IndexOf('@')).ToArray().Count(x => x == '.') < 1) return false;
                return true;
            }
            // check ID
            public static bool CheckID(string txtString)
            {
                if (txtString.IndexOfAny(Invalid_Char_For_ID) >= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}

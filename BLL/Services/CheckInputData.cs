using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.LibraryCheckInputType;
using System.Windows.Media.Animation;

namespace BLL.Services
{
    public static class CheckInputData
    {
        #region AreaDTO
        public static string? CheckValidInputForArealDTO(AreaDTO? obj)
        {
            if (obj == null) return "Area cannot be null";
            if(obj.Name == null) return "Name cannot be null";
            if (!GetInput.GetString.GetName(obj.Name))
                return "Name is valid";
            else
                GetInput.GetString.ReplaceSpaces(obj.Name);
            if (obj.Name.Length == 0) return "Name cannot be null";
            return null;
        }
        #endregion


        #region CategoryDTO
        public static string? CheckValidInpurtForCategoryDTO(CategoryDTO? obj)
        {
            if (obj == null) return "Category cannot be null";
            if (obj.Name == null) return "Name cannot be null";
            if (!GetInput.GetString.GetName(obj.Name))
                return "Name is valid";
            else
                GetInput.GetString.ReplaceSpaces(obj.Name);
            if (obj.Name.Length == 0) return "Name cannot be null";
            return null;
        }
        #endregion


        #region EmployeeDTO
        public static string? CheckValidInpurtForEmployeeDTO(EmployeeDTO? obj)
        {
            if (obj == null) return "Employee cannot be null";
            if (obj.UserName == null) return "Username cannot be null";
            if (obj.UserName.Length == 0) return "Username cannot be null";
            if (obj.FullName?.Length == 0) return "Name cannot be null";
            if (obj.FullName == null) return "Name cannot be null";
            if (!GetInput.GetString.GetName(obj.FullName)) 
                return "Name is not valid";
            else 
                GetInput.GetString.ReplaceSpaces(obj.FullName);
            if (obj.Email == null) return "Email cannot be null";
            if (obj.Email.Length == 0) return "Email cannot be null";
            if (!GetInput.GetString.CheckEmail(obj.Email)) return "Email is not valid";
            if (obj.Phone == null) return "Phone cannot be null";
            if (obj.Phone.Length < 9) return "Phone is not valid";
            try
            {
                int.Parse(obj.Phone);
            }
            catch
            {
                return "Phone is not valid ";
            }
            return null;
        }
        #endregion


        #region ProductDTO
        public static string? CheckValidInpurtForProductDTO(ProductDTO? obj)
        {
            if (obj == null) return "Product cannot be null";
            if (obj.Name == null) return "Name cannot be null";
            if (!GetInput.GetString.GetName(obj.Name))
                return "Name is not valid";
            else
                GetInput.GetString.ReplaceSpaces(obj.Name);
            if (obj.Name.Length == 0) return "Name cannot be null";
            if (obj.UnitPrice < 0) return "Unit price is not valid";
            return null;
        }
        #endregion

    }
}

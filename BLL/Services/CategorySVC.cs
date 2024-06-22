using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategorySVC : ICategorySVC
    {
        private readonly ICategoryRES _categoryRES;
        public CategorySVC(ICategoryRES CategoryRES)
        {
            _categoryRES = CategoryRES;
        }

        public string Add(CategoryDTO obj)
        {
            string? msg = CheckInputData.CheckValidInpurtForCategoryDTO(obj);
            if (msg != null) return msg;    
            if (obj.Name == null)
                return "Name cannot be empty";
            if (_categoryRES.CheckByName(obj.Name.Trim()) != null)
                return "Category name already exist";
            var entity = new Category()
            {
                Name = obj.Name?.Trim(),
                Status = obj.Status,
                Description = obj.Description,
            };
            if (_categoryRES.Add(entity))
                return "Add successfully";
            return "Cannot add new category! Try later!";
        }

        public string Delete(int objID)
        {
            var entity = _categoryRES.GetByID(objID);
            if (entity == null)
                return "Category does not exist!";
            if (entity.Status == true)
            {
                entity.Status = false;
                if (_categoryRES.Update(entity))
                    return "Category status is changed to inActive. Delete again to actually delete the Category";
                else
                    return "Cannot delete or update the category";
            }
            else
                if (_categoryRES.Delete(entity))
                    return "Delete successfully";
            return "Cannot delete the Category! Try later!";
        }

        public List<CategoryDTO> GetAll()
        {
            List<CategoryDTO> entityList = new List<CategoryDTO>();
            var list = _categoryRES.GetAll();
            foreach (var item in list)
            {
                entityList.Add(new CategoryDTO()
                {
                    ID = item.CategoryID,
                    Name = item.Name?.Trim(),
                    Status = item.Status,
                    Description = item.Description,
                });
            }
            return entityList;
        }

        public List<CategoryDTO> GetByName(string name)
        {
            List<CategoryDTO> entityList = new List<CategoryDTO>();
            var list = _categoryRES.GetByName(name);
            foreach (var item in list)
            {
                entityList.Add(new CategoryDTO()
                {
                    ID = item.CategoryID,
                    Name = item.Name?.Trim(),
                    Status = item.Status,
                    Description = item.Description,
                });
            }
            return entityList;
        }

        public string Update(CategoryDTO obj)
        {
            string? msg = CheckInputData.CheckValidInpurtForCategoryDTO(obj);
            if (msg != null) return msg;
            var updateObj = _categoryRES.GetByID(obj.ID);
            if (updateObj == null)
                return "Category does not exist!";
            if (obj.Name == null)
                return "Name cannot be empty";
            if (updateObj.Name != obj.Name && _categoryRES.CheckByName(obj.Name) != null)
                return "Category name already exist";
            updateObj.Name = obj.Name;
            updateObj.Status = obj.Status;
            updateObj.Description = obj.Description;
            if (_categoryRES.Update(updateObj))
                return "Update sucessfully!";
            else
                return "Cannot update the category";
        }
    }
}

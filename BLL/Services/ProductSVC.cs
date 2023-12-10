using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DTO;

namespace BLL.Services
{
    public class ProductSVC : IProductSVC
    {
        ImageHandlerSVC _imageHandler;
        private readonly IProductRES _productRES;
        private readonly ICategoryRES _categoryRES;
        public ProductSVC(IProductRES productRES, ICategoryRES categoryRES, ImageHandlerSVC imageHandler)
        {
            _productRES = productRES;
            _categoryRES = categoryRES;
            _imageHandler = imageHandler;
        }

        public string Add(ProductDTO obj)
        {
            
            if (obj.Name == null)
                return "Name cannot be empty";
            if (obj.Name.Length == 0)
                return "Name cannot be empty";
            if (_productRES.CheckByName(obj.Name.Trim()) != null)
                return "Product Name already exist";
            string? imageSrc = obj.Image;
            var entity = new Product()
            {
                Name = obj.Name?.Trim(),
                UnitPrice = obj.UnitPrice,
                Status = obj.Status,
                Description = obj.Description,
                CategoryID = obj.CategoryID,
            };
            var p = _productRES.AddnReturn(entity);
            if (p != null)
            {
                p.Image = p.ProductID + ".jpg";
                if (_productRES.Update(p))
                {
                    if (imageSrc != null)
                        _imageHandler.CopyImage(imageSrc, p.Image);
                    return "Add successfully";
                }
                else
                    return "Add successfully but cannot save product image";
            }
            return "Cannot add new Product! Try later!";
        }

        public string Delete(int objID)
        {
            var entity = _productRES.GetByID(objID);
            if (entity == null)
                return "Product does not exist!";
            if (entity.Status == true)
            {
                entity.Status = false;
                if (_productRES.Update(entity))
                    return "Product status is changed to inActive. Delete again to actually delete the Product";
                else
                    return "Cannot delete or update the Product";
            }
            else
                if (_productRES.Delete(entity))
                return "Delete successfully";
            return "Cannot delete the Product! Try later!";
        }

        public List<ProductDTO> GetAll()
        {
            List<ProductDTO> entityList = new List<ProductDTO>();
            var list1 = _productRES.GetAll();
            var list2 = _categoryRES.GetAll();

            var list = (from p in list1
                       join c in list2
                       on p.CategoryID equals c.CategoryID
                       select new ProductDTO()
                       {
                           ID = p.ProductID,
                           Name = p.Name?.Trim(),
                           Status = p.Status,
                           UnitPrice = p.UnitPrice,
                           Image = _imageHandler.GetImageDirecory(p.Image),
                           Description = p.Description,
                           CategoryName = c.Name,
                           CategoryID = c.CategoryID,
                       }).ToList();

            return list==null? new List<ProductDTO>() : list;
        }

        public List<ProductDTO> GetByName(string name)
        {
            var list1 = _productRES.GetByName(name);
            var list2 = _categoryRES.GetAll();

            var list = (from p in list1
                        join c in list2
                        on p.CategoryID equals c.CategoryID
                        select new ProductDTO()
                        {
                            ID = p.ProductID,
                            Name = p.Name?.Trim(),
                            Status = p.Status,
                            UnitPrice = p.UnitPrice,
                            Image = _imageHandler.GetImageDirecory(p.Image),
                            Description = p.Description,
                            CategoryName = c.Name,
                            CategoryID = c.CategoryID
                        }).ToList();

            return list == null ? new List<ProductDTO>() : list;
        }

        public List<ProductDTO> GetByNameAndCategory(string name, CategoryDTO? category)
        {
            List<ProductDTO> list = new List<ProductDTO>();
            var list1 = _productRES.GetByName(name);
            if (category == null)
            {
                var list2 = _categoryRES.GetAll();
                list = (from p in list1
                            join c in list2
                            on p.CategoryID equals c.CategoryID
                            select new ProductDTO()
                            {
                                ID = p.ProductID,
                                Name = p.Name?.Trim(),
                                Status = p.Status,
                                UnitPrice = p.UnitPrice,
                                Image = _imageHandler.GetImageDirecory(p.Image),
                                Description = p.Description,
                                CategoryName = c.Name,
                                CategoryID = c.CategoryID
                            }).ToList();
            }
            else
            {
                var c = _categoryRES.GetByID(category.ID);
                if (c == null) return list;
                list = (from p in list1
                        where p.CategoryID == c.CategoryID
                        select new ProductDTO()
                        {
                            ID = p.ProductID,
                            Name = p.Name?.Trim(),
                            Status = p.Status,
                            UnitPrice = p.UnitPrice,
                            Image = _imageHandler.GetImageDirecory(p.Image),
                            Description = p.Description,
                            CategoryName = c.Name,
                            CategoryID = c.CategoryID
                        }).ToList();
            }

            return list == null ? new List<ProductDTO>() : list;
        }

        public string Update(ProductDTO obj)
        {
            if (obj.Name == null)
                return "Name cannot be empty";
            if (obj.Name.Length == 0)
                return "Name cannot be empty";
            var entity = _productRES.GetByID(obj.ID);
            if (entity == null)
                return "Product does not exist!";
            string? imageSrc = obj.Image;
            entity.Name = obj.Name;
            entity.Status = obj.Status;
            entity.Description = obj.Description;
            entity.UnitPrice = obj.UnitPrice;
            entity.Image = entity.ProductID + ".jpg";
            
            entity.CategoryID = obj.CategoryID;
            if (_productRES.Update(entity))
            {
                if (imageSrc != null)
                    _imageHandler.CopyImage(imageSrc, entity.Image);
                return "Update successfully";
            }
            else
                return "Cannot delete or update the Product";
        }

    }
}

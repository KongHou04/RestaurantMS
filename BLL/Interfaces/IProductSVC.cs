using DTO;

namespace BLL.Interfaces
{
    public interface IProductSVC
    {
        public string Add(ProductDTO obj);
        public string Update(ProductDTO obj);
        public string Delete (int objID);
        public List<ProductDTO> GetAll();
        public List<ProductDTO> GetAllValid();
        public List<ProductDTO> GetByName(string name);
        public List<ProductDTO> GetByNameAndCategory(string name, CategoryDTO? category);
    }
}

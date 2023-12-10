using System.Windows.Media.Imaging;

namespace DTO
{
    public class ProductDTO : BaseDTOModel
    {
        private int _id;
        public int ID 
        {
            get => _id; 
            set { _id=value; OnPropertyChanged(nameof(ID)); } 
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private double _unitPrice;
        public double UnitPrice
        {
            get => _unitPrice;
            set { _unitPrice = value; OnPropertyChanged(nameof(UnitPrice)); }
        }

        public string? _image;
        public string? Image
        {
            get => _image;
            set { _image = value; OnPropertyChanged(nameof(Image)); }
        }

        public BitmapImage? _imageBitMap;
        public BitmapImage? ImageBitMap
        {
            get => _imageBitMap;
            set { _imageBitMap = value; OnPropertyChanged(nameof(ImageBitMap)); }
        }

        public string? _description;
        public string? Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public bool _status;
        public bool Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        public string? _categoryName;
        public string? CategoryName
        {
            get => _categoryName;
            set { _categoryName = value; OnPropertyChanged(nameof(CategoryName)); }
        }

        public int? _categoryID;
        public int? CategoryID
        {
            get => _categoryID;
            set { _categoryID = value; OnPropertyChanged(nameof(CategoryID)); }
        }

        public ProductDTO(int id)
        {
            ID = id;
        }
        public ProductDTO() { }

    }
}

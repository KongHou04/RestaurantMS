using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GUI.ViewModels
{
    public class ProductActionViewModel : ActionViewModel<ProductDTO>
    {
        private CategoryDTO? _selectedCategory;
        public CategoryDTO? SelectedCategory
        {
            get { return _selectedCategory; }
            set 
            { 
                _selectedCategory = value;  
                OnPropertyChanged(nameof(SelectedCategory));
                if (SelectedCategory != null && Obj != null)
                    Obj.CategoryID = SelectedCategory.ID;
            }
        }

        public string Title { get; set; } = "Product Information Form";
        public ProductActionViewModel(DataViewModel? dataViewModel, ICommand? backCommand, object? updateObj = null) : base(backCommand, updateObj)
        {
            DataViewModel = dataViewModel;

            SetCommands(ExecuteClearCommand, ExecuteSetDefaultCommand);
            SetOpenImageCommands(ExecuteChooseImageCommand);
            UpdateCommand = DataViewModel?.UpdateProductCommand;
            AddCommand = DataViewModel?.AddProductCommand;


            SetDefaultCommand?.Execute(null);

        }

        private void ExecuteSetDefaultCommand(object? obj)
        {
            if (UpdateObj == null)
            {
                Obj = new ProductDTO();
                return;
            }
            if (Obj == null)
                Obj = new ProductDTO(UpdateObj.ID);
            IDString = Obj.ID.ToString();
            Obj.Name = UpdateObj.Name;
            Obj.Status = UpdateObj.Status;
            Obj.UnitPrice = UpdateObj.UnitPrice;
            Obj.Description = UpdateObj.Description;
            Obj.CategoryID = UpdateObj.CategoryID;
            Obj.Image = UpdateObj.Image;
            if (Obj.Image != null)
            {
                Obj.ImageBitMap = SetBitMap(Obj.Image);
            }
            
            //Obj.ImageBitMap = UpdateObj.ImageBitMap;

            SelectedCategory = DataViewModel?.CategoryFilterToEdit?.FirstOrDefault(c => c.ID == Obj.CategoryID);
        }
        private void ExecuteClearCommand(object? obj)
        {
            SelectedCategory = DataViewModel?.CategoryFilter?.FirstOrDefault();
            if (Obj == null) return;
            Obj.Name = string.Empty;
            Obj.Status = false;
            Obj.UnitPrice = 0;
            Obj.Description = string.Empty;
            Obj.CategoryID = SelectedCategory?.ID;

        }

        private void ExecuteChooseImageCommand(object? obj)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var imageName = openFileDialog.SafeFileName;
                // Đọc đường dẫn tệp ảnh được chọn
                var imagePath = openFileDialog.FileName;
                if (Obj != null)
                {
                    Obj.Image = imagePath;
                    Obj.ImageBitMap = SetBitMap(imagePath);
                }

            }
        }

    }
}

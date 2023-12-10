using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GUI.ViewModels
{
    public class EmployeeActionViewModel : ActionViewModel<EmployeeDTO>
    {
        private RoleDTO? _selectedRole;
        public RoleDTO? SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
                if (SelectedRole != null && Obj != null)
                    Obj.RoleID = SelectedRole.ID;
            }
        }

        public string Title { get; set; } = "Employee Information Form";

        public EmployeeActionViewModel(DataViewModel? dataViewModel, ICommand? backCommand, object? updateObj = null) : base(backCommand, updateObj)
        {
            DataViewModel = dataViewModel;

            SetCommands(ExecuteClearCommand, ExecuteSetDefaultCommand);
            SetOpenImageCommands(ExecuteChooseImageCommand);
            UpdateCommand = DataViewModel?.UpdateEmployeeCommand;
            AddCommand = DataViewModel?.AddEmployeeCommand;

            SetDefaultCommand?.Execute(null);
        }

        private void ExecuteSetDefaultCommand(object? obj)
        {
            if (UpdateObj == null)
            {
                Obj = new EmployeeDTO();
                return;
            }
            if (Obj == null)
                Obj = new EmployeeDTO(UpdateObj.ID);
            IDString = Obj.ID.ToString();
            Obj.FullName = UpdateObj.FullName;
            Obj.UserName = UpdateObj.UserName;
            Obj.Email = UpdateObj.Email;
            Obj.Phone = UpdateObj.Phone;
            Obj.BirthDate = UpdateObj.BirthDate;
            Obj.Status = UpdateObj.Status;
            Obj.RoleID = UpdateObj.RoleID;
            Obj.Avatar = UpdateObj.Avatar;
            if (Obj.Avatar != null)
            {
                Obj.AvatarBitMap = SetBitMap(Obj.Avatar);
            }


            SelectedRole = DataViewModel?.RoleFilterToEdit?.FirstOrDefault(c => c.ID == Obj.RoleID);
        }
        private void ExecuteClearCommand(object? obj)
        {
            SelectedRole = DataViewModel?.RoleFilterToEdit?.FirstOrDefault();
            if (Obj == null) return;
            Obj.FullName = string.Empty;
            Obj.UserName = string.Empty;
            Obj.Email = string.Empty;
            Obj.Phone = string.Empty;
            Obj.BirthDate = DateTime.Today.ToUniversalTime();
            Obj.Status = false;
            Obj.RoleID = SelectedRole?.ID;

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
                    Obj.Avatar = imagePath;
                    Obj.AvatarBitMap = SetBitMap(imagePath);
                }
            }
        }
    }
}

using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.ViewModels
{
    public class CategogryActionViewModel : ActionViewModel<CategoryDTO>
    {
        public string Title { get; set; } = "Category Information Form";
        public CategogryActionViewModel(DataViewModel? dataViewModel, ICommand? backCommand, object? updateObj = null) : base(backCommand, updateObj)
        {
            DataViewModel = dataViewModel;

            SetCommands(ExecuteClearCommand, ExecuteSetDefaultCommand);
            UpdateCommand = DataViewModel?.UpdateCategoryCommand;
            AddCommand = DataViewModel?.AddCategoryCommand;

            SetDefaultCommand?.Execute(null);
        }

        private void ExecuteSetDefaultCommand(object? obj)
        {
            if (UpdateObj == null)
            {
                Obj = new CategoryDTO();
                return;
            }
            if (Obj == null)
                Obj = new CategoryDTO(UpdateObj.ID);
            IDString = Obj.ID.ToString();
            Obj.Name = UpdateObj.Name;
            Obj.Status = UpdateObj.Status;
            Obj.Description = UpdateObj.Description;
        }
        private void ExecuteClearCommand(object? obj)
        {
            if (Obj == null) return;
            Obj.Name = string.Empty;
            Obj.Status = false;
            Obj.Description = string.Empty;
        }

    }
}

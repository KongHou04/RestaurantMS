using DTO;
using System.Windows.Forms;
using System.Windows.Input;

namespace GUI.ViewModels
{
    public class AreaActionViewModel : ActionViewModel<AreaDTO>
    {
        public string Title { get; set; } = "Area Information Form";
        public AreaActionViewModel(DataViewModel? dataViewModel, ICommand? backCommand, object? updateObj = null) : base(backCommand, updateObj)
        {
            DataViewModel = dataViewModel;

            SetCommands(ExecuteClearCommand, ExecuteSetDefaultCommand);
            UpdateCommand = DataViewModel?.UpdateAreaCommand;
            
            SetDefaultCommand?.Execute(null);
        }

        private void ExecuteSetDefaultCommand(object? obj)
        {
            if (UpdateObj == null)
            {
                Obj = new AreaDTO();
                return;
            }
            if (Obj == null)
                Obj = new AreaDTO(UpdateObj.ID);
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

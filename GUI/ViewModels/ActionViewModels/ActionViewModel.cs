using GUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GUI.ViewModels
{
    public class ActionViewModel <T> : BaseViewModel
    {
        // Fields - Properties
        private DataViewModel? _dataViewModel;
        public DataViewModel? DataViewModel
        {
            get { return _dataViewModel; }
            set { _dataViewModel = value; OnPropertyChanged(nameof(DataViewModel)); }
        }

        public bool IsClearBtnVisible { get; set; } = true;
        public bool IsSetDefaultBtnVisible { get; set; } = true;
        public bool IsAddBtnVisible { get; set; } = true;
        public bool IsUpdateBtnVisible { get; set; } = true;
        public bool IsDeleteBtnVisible { get; set; } = true;

        private T? _obj;
        public T? Obj
        {
            get => _obj;
            set { _obj = value; OnPropertyChanged(nameof(Obj)); }
        }

        private string idString = "Auto generate";
        public string IDString
        {
            get { return idString; }
            set { idString = value; OnPropertyChanged(IDString); }
        }

        private string image = "";
        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(Image); }
        }

        private T? _updateObj;
        public T? UpdateObj
        {
            get => _updateObj;
            set { _updateObj = value; OnPropertyChanged(nameof(UpdateObj)); }
        }


        // Commands
        public ICommand? BackCommand { get; set; }
        public ICommand? ClearCommand { get; set; }
        public ICommand? SetDefaultCommand { get; set; }
        public ICommand? AddCommand { get; set; }
        public ICommand? UpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? OpenImageCommand { get; set; }

        // Constructors
        public ActionViewModel(ICommand? backCommand, object? updateObj)
        {
            if (updateObj != null)
                UpdateObj = (T)updateObj;
            BackCommand = backCommand;
        }
        public ActionViewModel(ICommand? backCommand)
        {
            BackCommand = backCommand;
        }

        public ActionViewModel() { }


        protected void SetCommands(Action<object?> clearFunc, Action<object?> setDefaultFunc)
        {
            ClearCommand = new RelayCommand(clearFunc);
            SetDefaultCommand = new RelayCommand(setDefaultFunc);
        }
        protected void SetOpenImageCommands(Action<object?> openImageFunc)
        {
            OpenImageCommand = new RelayCommand(openImageFunc);
        }
        public void SetBtnMode(string mode)
        {
            mode = mode.ToLower();
            if (mode.Contains("clear")) IsClearBtnVisible = true;
            else IsClearBtnVisible = false;
            if (mode.Contains("setdefault")) IsSetDefaultBtnVisible = true;
            else IsSetDefaultBtnVisible = false;
            if (mode.Contains("add")) IsAddBtnVisible = true;
            else IsAddBtnVisible = false;
            if (mode.Contains("update")) IsUpdateBtnVisible = true;
            else IsUpdateBtnVisible = false;
            if (mode.Contains("delete")) IsDeleteBtnVisible = true;
            else IsDeleteBtnVisible = false;
        }
        protected BitmapImage? SetBitMap(string path)
        {
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                return bitmap;
            }
            catch
            {
                return null;
            } 
            
        }

    }
}

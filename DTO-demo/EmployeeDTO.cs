using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace DTO
{
    public class EmployeeDTO : BaseDTOModel
    {
        private int _id { get; set; }
        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(ID)); }
        }

        private string? _username { get; set; }
        public string? UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(UserName)); }
        }

        private string? _fullname { get; set; }
        public string? FullName
        {
            get { return _fullname; }
            set { _fullname = value; OnPropertyChanged(nameof(FullName)); }
        }

        private DateTime _birthday { get; set; }
        public DateTime BirthDate
        {
            get { return _birthday; }
            set { _birthday = value; OnPropertyChanged(nameof(BirthDate)); }
        }

        private string? _email { get; set; }
        public string? Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        private string? _phone { get; set; }
        public string? Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(nameof(Phone)); }
        }

        private string? _avatar { get; set; }
        public string? Avatar
        {
            get { return _avatar; }
            set { _avatar = value; OnPropertyChanged(nameof(Avatar)); }
        }

        private BitmapImage? _avatarbitmap { get; set; }
        public BitmapImage? AvatarBitMap
        {
            get { return _avatarbitmap; }
            set { _avatarbitmap = value; OnPropertyChanged(nameof(AvatarBitMap)); }
        }

        private bool _status { get; set; }
        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        private string? _rolename { get; set; }
        public string? RoleName
        {
            get { return _rolename; }
            set { _rolename = value; OnPropertyChanged(nameof(RoleName)); }
        }

        private int? _roleid { get; set; }
        public int? RoleID
        {
            get { return _roleid; }
            set { _roleid = value; OnPropertyChanged(nameof(RoleID)); }
        }

        public EmployeeDTO() { }
        public EmployeeDTO(int id)
        {
            _id = id;
        }
    }
}

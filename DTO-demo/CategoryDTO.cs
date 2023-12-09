using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CategoryDTO :BaseDTOModel
    {
        private int _id;
        public int ID
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(ID)); }
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private int _tableNumber;
        public int TableNumber
        {
            get => _tableNumber;
            set { _tableNumber = value; OnPropertyChanged(nameof(TableNumber)); }
        }

        private bool _tatus;
        public bool Status
        {
            get => _tatus;
            set { _tatus = value; OnPropertyChanged(nameof(Status)); }
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }


        public CategoryDTO(int id)
        {
            _id = id;
        }
        public CategoryDTO() { }
    }
}

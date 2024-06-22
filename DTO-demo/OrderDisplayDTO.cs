using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDisplayDTO : BaseDTOModel
    {
        private OrderDTO? _order;
        public OrderDTO? Order { get { return _order; } set { _order = value; OnPropertyChanged(nameof(_order)); } }

        private ObservableCollection<OrderDetailDTO>? _odList;
        public ObservableCollection<OrderDetailDTO>? ODList
        {
            get { return _odList; }
            set
            {
                _odList = value;
                OnPropertyChanged(nameof(ODList));
            }
        }

        private TableDTO? _table;
        public TableDTO? Table { get { return _table; } set { _table = value; OnPropertyChanged(nameof(_table)); } }

    }
}

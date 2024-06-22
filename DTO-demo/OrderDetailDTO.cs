using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDetailDTO : BaseDTOModel
    {
        private int _id { get; set; }
        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(ID)); }
        }
        private int? _productID { get; set; }
        public int? ProductID
        {
            get { return _productID; }
            set { _productID = value; OnPropertyChanged(nameof(ProductID)); }
        }

        private string? _productname { get; set; }
        public string? ProductName
        {
            get { return _productname; }
            set { _productname = value; OnPropertyChanged(nameof(ProductName)); }
        }

        private double _unitprice { get; set; }
        public double UnitPrice
        {
            get { return _unitprice; }
            set { _unitprice = value; OnPropertyChanged(nameof(UnitPrice)); }
        }

        private int _quantity { get; set; }
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        private string? _description { get; set; }
        public string? Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        private int? _orderid { get; set; }
        public int? OrderID
        {
            get { return _orderid; }
            set { _orderid = value; OnPropertyChanged(nameof(OrderID)); }
        }
    }
}

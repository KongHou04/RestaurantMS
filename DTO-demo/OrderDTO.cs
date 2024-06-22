using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDTO : BaseDTOModel
    {
        private int _id { get; set; }
        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(ID)); }
        }

        private DateTime _ordertime { get; set; }
        public DateTime OrderTime
        {
            get { return _ordertime; }
            set { _ordertime = value; OnPropertyChanged(nameof(OrderTime)); }
        }

        private double _totalamount { get; set; }
        public double TotalAmount
        {
            get { return _totalamount; }
            set { _totalamount = value; OnPropertyChanged(nameof(TotalAmount)); }
        }

        private double _incedentals { get; set; }
        public double Incedentals
        {
            get { return _incedentals; }
            set { _incedentals = value; OnPropertyChanged(nameof(Incedentals)); }
        }

        private double _discount { get; set; }
        public double Discount
        {
            get { return _discount; }
            set { _discount = value; OnPropertyChanged(nameof(Discount)); }
        }

        private double _grandtotal { get; set; }
        public double GrandTotal
        {
            get { return _grandtotal; }
            set { _grandtotal = value; OnPropertyChanged(nameof(GrandTotal)); }
        }

        private bool _status { get; set; }
        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        private bool _paymentstatus { get; set; }
        public bool PaymentStatus
        {
            get { return _paymentstatus; }
            set { _paymentstatus = value; OnPropertyChanged(nameof(PaymentStatus)); }
        }

        private string? _description { get; set; }
        public string? Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        private int? _tableID { get; set; }
        public int? TableID
        {
            get { return _tableID; }
            set { _tableID = value; OnPropertyChanged(nameof(TableID)); }
        }

        private string? _tablename { get; set; }
        public string? TableName
        {
            get { return _tablename; }
            set { _tablename = value; OnPropertyChanged(nameof(TableName)); }
        }

        private string? _employeename { get; set; }
        public string? EmployeeName
        {
            get { return _employeename; }
            set { _employeename = value; OnPropertyChanged(nameof(EmployeeName)); }
        }
    }
}

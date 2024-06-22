﻿using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class OrderRES : BaseRES, IOrderRES
    {
        public OrderRES(RMContext context) : base(context) { } 

        public bool Add(Order obj)
        {
            Order order = new Order();
            try
            {
                order.OrderTime = obj.OrderTime;
                order.Incedentals = obj.Incedentals;
                order.Discount = obj.Discount;
                order.Status = obj.Status;
                order.PaymentStatus = obj.PaymentStatus;
                order.Description = obj.Description;
                order.TableID = obj.TableID;
                order.EmployeeID = obj.EmployeeID;
                order.UpdateGrandTotal();
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Order? AddnReturn(Order obj)
        {
            Order order = new Order();
            try
            {
                order.OrderTime = obj.OrderTime.ToUniversalTime();
                order.Incedentals = obj.Incedentals;
                order.Discount = obj.Discount;
                order.Status = true;
                order.PaymentStatus = false;
                order.Description = obj.Description;
                order.TableID = obj.TableID;
                order.EmployeeID = obj.EmployeeID;
                order.TotalAmount = obj.TotalAmount;
                order.GrandTotal = obj.GrandTotal;
                _context.Orders.Add(order);
                _context.SaveChanges();
                return order;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Delete(Order obj)
        {
            var deleteObj = GetByID(obj.OrderID);
            if (deleteObj == null)
                return false;
            try
            {
                _context.Remove(deleteObj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public List<Order> GetAllUnpayOrder()
        {
            return _context.Orders.Where(o => o.Status == true && o.PaymentStatus == false).ToList();
        }

        public List<Order> GetByDate(DateTime date)
        {
            return _context.Orders.ToList().Where(o => o.OrderTime.Date == date.Date).ToList();
        }

        public Order? GetByID(int id)
        {
            return _context.Orders.ToList().FirstOrDefault(c => c.OrderID == id);
        }

        public Order? GetCurrentOrderByTable(int tableID)
        {
            return _context.Orders.LastOrDefault(c => c.TableID == tableID && c.Status == true && c.PaymentStatus == false);
        }

        public bool Update(Order obj)
        {
            var updateObj = GetByID(obj.OrderID);
            if (updateObj == null)
                return false;
            try
            {
                updateObj.OrderTime = obj.OrderTime;
                updateObj.Incedentals = obj.Incedentals;
                updateObj.Discount = obj.Discount;
                updateObj.Status = obj.Status;
                updateObj.PaymentStatus = obj.PaymentStatus;
                updateObj.Description = obj.Description;
                updateObj.TableID = obj.TableID;
                updateObj.EmployeeID = obj.EmployeeID;
                updateObj.UpdateGrandTotal();
                _context.Add(updateObj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}

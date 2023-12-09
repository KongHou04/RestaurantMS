using BLL.Interfaces;
using BLL.Services;
using DAL.Contexts;
using DAL.Interfaces;
using DAL.Repositories;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM_Project1.Helpers
{
    public class UserSection
    {
        private static EmployeeDTO? _instance;
        public static EmployeeDTO? Instance
        {
            get => _instance;
        }

        public static void SetCurrentUser(EmployeeDTO? emp)
        {
            _instance = emp;
        }

        public static void ClearUser()
        {
            _instance = null;
        }
    }
}

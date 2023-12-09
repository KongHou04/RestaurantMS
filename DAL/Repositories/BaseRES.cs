using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public abstract class BaseRES
    {
        protected RMContext _context;
        public BaseRES(RMContext context)
        {
            _context = context;
        }
    }
}

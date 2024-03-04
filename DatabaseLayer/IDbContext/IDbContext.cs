using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.IDbContext
{
    public interface IDbContext<out DBSetTable> where DBSetTable : class
    {
        public DBSetTable SetTable();       
    }
}

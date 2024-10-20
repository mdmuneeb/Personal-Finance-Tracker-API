using Microsoft.EntityFrameworkCore.Storage;
using SE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Services
{
    public class BaseService<TObject>
    {
        protected PersonalFinanceTrackerContext _context;
        public BaseService(PersonalFinanceTrackerContext context)
        {
            _context = context;   
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }

    }
}

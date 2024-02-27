using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCAppDbcontext _dbcontext;

        public IEmployeeRepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }


        public UnitOfWork(MVCAppDbcontext dbcontext)
        {
            EmployeeRepository = new EmployeeRepository(dbcontext);
            DepartmentRepository= new DepartmentRepository(dbcontext);
            _dbcontext = dbcontext;
        }

        async Task< int> IUnitOfWork.Complete()
        {
           return await _dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }

    }
}

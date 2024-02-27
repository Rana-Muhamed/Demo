using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private protected readonly MVCAppDbcontext _dbcontext;
        public GenericRepository(MVCAppDbcontext dbcontext)//Dependency injection
        {
            //dbcontext= new MVCAppDbcontext();
            _dbcontext = dbcontext;

        }
        public async Task Add(T item)

            =>await _dbcontext.Set<T>().AddAsync(item);
 
      


        public void Delete(T item)
        
           => _dbcontext.Set<T>().Remove(item);


        public async Task< T> Get(int id)

            => await _dbcontext.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>)await _dbcontext.Employees.Include(E => E.Department).ToListAsync();
            else
                return await _dbcontext.Set<T>().ToListAsync();
        }

            

        public void Update(T item)
        
          =>  _dbcontext.Set<T>().Update(item);
            
    
    }
}

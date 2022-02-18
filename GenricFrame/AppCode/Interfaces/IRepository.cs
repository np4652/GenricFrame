using GenricFrame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenricFrame.AppCode.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<Response<T>> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync(T entity);
        Task<Response> AddAsync(T entity);
        Task<Response> DeleteAsync(int id);
        Task<IReadOnlyList<T>> GetDropdownAsync(T entity);
        //Task GetByIdAsync(object id);
    }
}

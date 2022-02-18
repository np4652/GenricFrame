using GenricFrame.AppCode.DAL;
using GenricFrame.AppCode.Interfaces;
using GenricFrame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenricFrame.AppCode.Reops
{

    public interface IDemo
    {
        Task<List<AppicationUser>> GetEmployeeAsync();
    }
    public class DemoRepo : IRepository<DemoViewModel>
    {
        private readonly IDapperRepository _dapper;
        public DemoRepo(IDapperRepository dapper)
        {
            _dapper = dapper;
        }

        public Task<Response> AddAsync(DemoViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DemoViewModel>> GetAllAsync(DemoViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<Response<DemoViewModel>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DemoViewModel>> GetDropdownAsync(DemoViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppicationUser>> GetEmployeeAsync()
        {
            var employee = await _dapper.GetAllAsync<AppicationUser>("select * from Users", new Dapper.DynamicParameters { }, commandType: System.Data.CommandType.Text);
            return employee.ToList();
        }
    }
}

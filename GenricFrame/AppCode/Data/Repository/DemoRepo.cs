using GenricFrame.AppCode.DAL;
using GenricFrame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenricFrame.AppCode.Data.Repository
{

    public interface IDemo
    {
        Task<List<AppicationUser>> GetEmployeeAsync();
    }
    public class DemoRepo : IDemo
    {
        private readonly IDapperRepository _dapper;
        public DemoRepo(IDapperRepository dapper)
        {
            _dapper = dapper;
        }

        public async Task<List<AppicationUser>> GetEmployeeAsync()
        {
            var employee = await _dapper.GetAllAsync<AppicationUser>("select * from Users", new Dapper.DynamicParameters { }, commandType: System.Data.CommandType.Text);
            return employee.ToList();
        }
    }
}

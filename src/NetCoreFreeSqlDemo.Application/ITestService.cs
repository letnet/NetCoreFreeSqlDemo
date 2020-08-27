using NetCoreFreeSqlDemo.Application.Models;
using NetCoreFreeSqlDemo.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFreeSqlDemo.Application
{
    public interface ITestService : IBaseApplication
    {
        Task<TestDto> Get(string id);

        Task Insert(List<TestDto> list);
    }
}



using FreeSql;
using NetCoreFreeSqlDemo.Application.Entitys;
using NetCoreFreeSqlDemo.Application.Models;
using NetCoreFreeSqlDemo.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFreeSqlDemo.Application.Application
{
    public class TestService : ITestService
    {
        BaseRepository<Test> _testRepository { get; set; }
        //IRepository<Test> _testRepository { get; set; }
        public TestService(BaseRepository<Test> testRepository)
        {
            this._testRepository = testRepository;
        }

        //[Transactional]
        public async Task<TestDto> Get(string id)
        {
            var test = await _testRepository.Select.Where(p => p.ID == id).FirstAsync();
            return test.MapTo<TestDto>();
        }

        public async Task Insert(List<TestDto> list)
        {
            if (list == null)
                return;
            var listTest = list.MapToList<Test>();
            await _testRepository.InsertAsync(listTest);
        }
    }
}



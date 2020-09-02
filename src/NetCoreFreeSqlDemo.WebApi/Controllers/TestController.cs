using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreFreeSqlDemo.Application.Application;
using NetCoreFreeSqlDemo.Application.Models;

namespace NetCoreFreeSqlDemo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        TestService _testService { get; set; }
        public TestController(TestService testService)
        {
            this._testService = testService;
        }

        /// <summary>
        /// 获取测试信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<TestDto> GetInfo()
        {
            return await _testService.Get("68f4469d-1222-4414-bf73-6823815cdb15");             
        }
    }
}



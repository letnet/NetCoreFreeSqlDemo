using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NetCoreFreeSqlDemo.Infrastructure
{
    public class FreeSqlDb
    {
        public static IFreeSql Builder(IConfiguration configuration)
        {
            var fsql = new FreeSql.FreeSqlBuilder()
                            .UseConnectionString(FreeSql.DataType.MySql, configuration.GetConnectionString("SqlConnection"))
                            .UseAutoSyncStructure(true) //自动同步实体结构到数据库【开发环境必备】
                            //.UseMonitorCommand(cmd => Debug.WriteLine(cmd.CommandText)) //调试打印执行的sql
                            .Build();
            return fsql;
        }
    }
}

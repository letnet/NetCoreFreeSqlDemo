using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFreeSqlDemo.Application.Entitys
{

    public class Test
    {
        [Column(IsIdentity = false, IsPrimary = true)]
        public string ID { get; set; }
        public string Title { get; set; }
    }
}



using AspectCore.DynamicProxy;
using FreeSql;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFreeSqlDemo.Infrastructure
{
    /// <summary>
    /// 为工作单元提供事务一致性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionalAttribute : AbstractInterceptorAttribute
    {
        public Propagation Propagation { get; set; } = Propagation.Required;

        IsolationLevel? _IsolationLevelPriv;
        public IsolationLevel IsolationLevel { get => _IsolationLevelPriv.Value; set => _IsolationLevelPriv = value; }

        IUnitOfWork _uow;

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                var _uowManager = context.ServiceProvider.GetService(typeof(UnitOfWorkManager)) as UnitOfWorkManager;
                _uow = _uowManager.Begin(this.Propagation, this._IsolationLevelPriv);
                await next(context);
                _uow.Commit();
            }
            catch (Exception)
            {
                _uow.Rollback();
                throw;
            }
        }
    }
}



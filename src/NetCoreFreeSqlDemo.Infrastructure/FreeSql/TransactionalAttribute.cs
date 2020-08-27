using FreeSql;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Data;
using System.Threading.Tasks;

namespace NetCoreFreeSqlDemo.Infrastructure
{
    /// <summary>
    /// 为工作单元提供事务一致性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionalAttribute : DynamicProxyAttribute, IActionFilter
    {
        public Propagation Propagation { get; set; } = Propagation.Required;

        IsolationLevel? _IsolationLevelPriv;
        public IsolationLevel IsolationLevel { get => _IsolationLevelPriv.Value; set => _IsolationLevelPriv = value; }

        [DynamicProxyFromServices]
        UnitOfWorkManager _uowManager;
        IUnitOfWork _uow;

        public override Task Before(DynamicProxyBeforeArguments args) => OnBefore(_uowManager);
        public override Task After(DynamicProxyAfterArguments args) => OnAfter(args.Exception);

        //这里是为了 controller 
        public void OnActionExecuting(ActionExecutingContext context) => OnBefore(context.HttpContext.RequestServices.GetService(typeof(UnitOfWorkManager)) as UnitOfWorkManager);
        public void OnActionExecuted(ActionExecutedContext context) => OnAfter(context.Exception);

        Task OnBefore(UnitOfWorkManager uowm)
        {
            _uow = uowm.Begin(this.Propagation, this._IsolationLevelPriv);
            return Task.FromResult(false);
        }
        Task OnAfter(Exception ex)
        {
            try
            {
                if (ex == null) _uow.Commit();
                else _uow.Rollback();
            }
            finally
            {
                _uow.Dispose();
            }
            return Task.FromResult(false);
        }
    }
}



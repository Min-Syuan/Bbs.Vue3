﻿using System.Linq.Expressions;
using Furion.DependencyInjection;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Enums;
using Yi.Framework.Infrastructure.Helper;

namespace Yi.Framework.Infrastructure.Sqlsugar.Repositories
{
    public class SqlsugarRepository<T> : SimpleClient<T>, IRepository<T> ,ITransient where T : class, new()
    {
        public SqlsugarRepository(ISqlSugarClient context) : base(context)
        {
        }
        /// <summary>
        /// 注释一下，严格意义这里应该protected，但是我认为 简易程度 与 耦合程度 中是需要进行衡量的
        /// </summary>
        public ISugarQueryable<T> _DbQueryable => AsQueryable();

        protected ISqlSugarClient _Db { get { return Context; } set { } }

        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, IPagedAndSortedResultRequestDto page)
        {
            return await base.GetPageListAsync(whereExpression, new PageModel { PageIndex = page.PageNum, PageSize = page.PageSize });
        }

        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, IPagedAndSortedResultRequestDto page, Expression<Func<T, object>>? orderByExpression = null, OrderByEnum orderByType = OrderByEnum.Asc)
        {
            return await base.GetPageListAsync(whereExpression, new PageModel { PageIndex = page.PageNum, PageSize = page.PageSize }, orderByExpression, orderByType.EnumToEnum<OrderByType>());
        }

        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, IPagedAndSortedResultRequestDto page, string? orderBy, OrderByEnum orderByType = OrderByEnum.Asc)
        {
            return await _DbQueryable.Where(whereExpression).OrderByIF(orderBy is not null, orderBy + " " + orderByType.ToString().ToLower()).ToPageListAsync(page.PageNum, page.PageSize);
        }




        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, int pageNum, int pageSize)
        {
            return await base.GetPageListAsync(whereExpression, new PageModel { PageIndex = pageNum, PageSize = pageSize });
        }

        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, int pageNum, int pageSize, Expression<Func<T, object>>? orderByExpression = null, OrderByEnum orderByType = OrderByEnum.Asc)
        {
            return await base.GetPageListAsync(whereExpression, new PageModel { PageIndex = pageNum, PageSize = pageSize }, orderByExpression, orderByType.EnumToEnum<OrderByType>());
        }

        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, int pageNum, int pageSize, string? orderBy, OrderByEnum orderByType = OrderByEnum.Asc)
        {
            return await _DbQueryable.Where(whereExpression).OrderByIF(orderBy is not null, orderBy + " " + orderByType.ToString().ToLower()).ToPageListAsync(pageNum, pageSize);
        }







        public async Task<bool> UpdateIgnoreNullAsync(T updateObj)
        {
            return await _Db.Updateable(updateObj).IgnoreColumns(true).ExecuteCommandAsync() > 0;
        }

        public override async Task<bool> DeleteAsync(T deleteObj)
        {
            //逻辑删除
            if (deleteObj is ISoftDelete)
            {
                //反射赋值
                ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, deleteObj);
                return await UpdateAsync(deleteObj);

            }
            else
            {
                return await base.DeleteAsync(deleteObj);

            }
        }
        public override async Task<bool> DeleteAsync(List<T> deleteObjs)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                //反射赋值
                deleteObjs.ForEach(e => ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, e));
                return await UpdateRangeAsync(deleteObjs);
            }
            else
            {
                return await base.DeleteAsync(deleteObjs);
            }
        }
        public override async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                var entities = await GetListAsync(whereExpression);
                //反射赋值
                entities.ForEach(e => ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, e));
                return await UpdateRangeAsync(entities);
            }
            else
            {
                return await base.DeleteAsync(whereExpression);
            }
        }
        public override async Task<bool> DeleteByIdAsync(dynamic id)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                var entity = await GetByIdAsync(id);
                //反射赋值
                ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, entity);
                return await UpdateAsync(entity);
            }
            else
            {
                return await _Db.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
            }

        }
        public override async Task<bool> DeleteByIdsAsync(dynamic[] ids)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                var entities = await _DbQueryable.In(ids).ToListAsync();
                if (entities.Count == 0)
                {
                    return false;
                }
                //反射赋值
                entities.ForEach(e => ReflexHelper.SetModelValue(nameof(ISoftDelete.IsDeleted), true, e));
                return await UpdateRangeAsync(entities);
            }
            else
            {
                return await base.DeleteByIdsAsync(ids);
            }

        }

    }
}

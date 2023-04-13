﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Framework.Infrastructure.Ddd.Entities;
using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Framework.Infrastructure.Helper;

namespace Yi.Framework.Infrastructure.Ddd.Services
{

    public abstract class CrudAppService<TEntity, TEntityDto, TKey>
    : CrudAppService<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto>
    where TEntity : class, IEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
    {
    }

    public abstract class CrudAppService<TEntity, TEntityDto, TKey, TGetListInput>
        : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
    }

    public abstract class CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
    }




    public abstract class CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : CrudAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TEntity : class, IEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
    {
        protected override Task<TEntityDto> MapToGetListOutputDtoAsync(TEntity entity)
        {
            return MapToGetOutputDtoAsync(entity);
        }

    }

    public abstract class CrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : ReadOnlyAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>,
     ICrudAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TEntity : class, IEntity<TKey>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>

    {
        protected virtual Task<TEntity> MapToEntityAsync(TGetListInput getListinput)
        {
            return Task.FromResult(_mapper.Map<TEntity>(getListinput));
        }


        protected virtual Task<TEntity> MapToEntityAsync(TCreateInput createInput)
        {
            var entity = _mapper.Map<TEntity>(createInput);

            //这里判断实体的T，给id赋值

            //雪花id
            if (entity is IEntity<long> entityForlongId)
            {
                if (entityForlongId.Id is default(long))
                {
                    //使用反射，暂时先使用sqlsuga的雪花id提供
                    //ps: linshi
                    ReflexHelper.SetModelValue(nameof(IEntity<long>.Id), SnowflakeHelper.NextId, entity);
                }
            }
            if (entity is IEntity<Guid> entityForGuidId)
            {
                if (entityForGuidId.Id == Guid.Empty)
                {
                    ReflexHelper.SetModelValue(nameof(IEntity<long>.Id), new Guid(), entity);
                }
            }

            return Task.FromResult(entity);
        }
        protected virtual Task MapToEntityAsync(TUpdateInput updateInput, TEntity entity)
        {
            _mapper.Map(updateInput, entity);
            return Task.CompletedTask;
        }

        protected virtual Task<TEntity> MapToEntityAsync(TUpdateInput updateInput)
        {
            var entity = _mapper.Map<TEntity>(updateInput);
            return Task.FromResult(entity);
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<TGetOutputDto> CreateAsync(TCreateInput input)
        {
            var entity = await MapToEntityAsync(input);

            //这里还可以设置租户
            await _repository.InsertAsync(entity);

            return await MapToGetOutputDtoAsync(entity);
        }

        /// <summary>
        /// 单、多删
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<bool> DeleteAsync(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var idsValue = id.Split(',');
            if (idsValue is null || idsValue.Length == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _repository.DeleteByIdsAsync(idsValue.Select(x => (object)x!).ToArray());
        }

        ///// <summary>
        ///// 删
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //public async Task<bool> DeleteAsync(TKey id)
        //{
        //    if (id is null)
        //    {
        //        throw new ArgumentNullException(nameof(id));
        //    }
        //    return await _repository.DeleteByIdAsync(id);
        //}

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await _repository.GetByIdAsync(id);
            await MapToEntityAsync(input, entity);
            await _repository.UpdateAsync(entity);

            return await MapToGetOutputDtoAsync(entity);
        }
    }
}

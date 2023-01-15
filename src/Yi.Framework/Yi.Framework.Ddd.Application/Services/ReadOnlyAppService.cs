﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Ddd.Entities;
using Yi.Framework.Ddd.Repositories;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.Framework.Ddd.Services
{

    public abstract class ReadOnlyAppService<TEntity, TEntityDto, TKey>
    : ReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, PagedAndSortedResultRequestDto>
    where TEntity : class, IEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
    {
    }

    public abstract class ReadOnlyAppService<TEntity, TEntityDto, TKey, TGetListInput>
: ReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput>
where TEntity : class, IEntity<TKey>
where TEntityDto : IEntityDto<TKey>
    {
    }


    public abstract class ReadOnlyAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput> : ApplicationService,
      IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
        where TEntity : class, IEntity
    {
        protected IRepository<TEntity> _repository { get; }

        //Mapper
        protected virtual Task<TGetOutputDto> MapToGetOutputDtoAsync(TEntity entity)
        {
            return Task.FromResult(_mapper.Map<TEntity, TGetOutputDto>(entity));
        }
        protected virtual Task<List<TGetListOutputDto>> MapToGetListOutputDtosAsync(List<TEntity> entities)
        {
            var dtos = _mapper.Map<List<TGetListOutputDto>>(entities);

            return Task.FromResult(dtos);
        }
        protected virtual Task<TGetListOutputDto> MapToGetListOutputDtoAsync(TEntity entity)
        {
            var dto = _mapper.Map<TEntity, TGetListOutputDto>(entity);
            return Task.FromResult(dto);
        }

        /// <summary>
        /// 单查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TGetOutputDto> GetAsync(TKey id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await _repository.GetByIdAsync(id);

            return await MapToGetOutputDtoAsync(entity);
        }

        /// <summary>
        /// 全查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input)
        {

            var totalCount = await _repository.CountAsync(_ => true);

            var entities = new List<TEntity>();
            var entityDtos = new List<TGetListOutputDto>();

            if (totalCount > 0)
            {
                if (input is IPagedAndSortedResultRequestDto sortInput)
                {
                    entities = await _repository.GetPageListAsync(_ => true, sortInput);
                }
                //这里还可以追加如果是审计日志，继续拼接条件即可
                else
                {
                    entities = await _repository.GetListAsync();
                }
                entityDtos = await MapToGetListOutputDtosAsync(entities);
            }

            return new PagedResultDto<TGetListOutputDto>(
                totalCount,
                entityDtos
            );
        }
    }
}
